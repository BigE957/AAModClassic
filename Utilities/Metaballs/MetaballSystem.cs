using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace AAModClassic.Utilities.Metaballs;

public class MetaballSystem : ModSystem
{
    internal static readonly List<Metaball> metaballs = [];

    private static RenderTarget2D maskTarget;

    private const int MaskPadding = 4;

    public static Asset<Texture2D> Circle { get; private set;  }

    private static RenderTarget2D GetMaskTarget()
    {
        int width = Math.Max(1, Main.screenWidth + MaskPadding);
        int height = Math.Max(1, Main.screenHeight + MaskPadding);

        if (maskTarget is null || maskTarget.IsDisposed || maskTarget.Width != width || maskTarget.Height != height)
        {
            maskTarget?.Dispose();
            maskTarget = new RenderTarget2D(Main.instance.GraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
        }

        return maskTarget;
    }

    private static RenderTarget2D sceneBackup;

    private static RenderTarget2D GetSceneBackup(GraphicsDevice device, RenderTarget2D scene)
    {
        if (sceneBackup is null || sceneBackup.IsDisposed || sceneBackup.Width != scene.Width || sceneBackup.Height != scene.Height)
        {
            sceneBackup?.Dispose();
            sceneBackup = new RenderTarget2D(device, scene.Width, scene.Height, false, scene.Format, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
        }

        return sceneBackup;
    }

    private static void CopyTexture(Texture2D source)
    {
        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Matrix.Identity);
        Main.spriteBatch.Draw(source, Vector2.Zero, Color.White);
        Main.spriteBatch.End();
    }

    public override void Load()
    {
        On_Main.CheckMonoliths += PrepareMetaballs;
        On_Main.DrawBackgroundBlackFill += DrawMetaballs_BeforeAllTiles;
        On_Main.DoDraw_Tiles_Solid += DrawMetaballs_BeforeSolidTiles;
        On_Main.DoDraw_DrawNPCsOverTiles += DrawMetaballs_NPCs;
        On_Main.DrawProjectiles += DrawMetaballs_Projectiles;
        On_Main.DrawPlayers_AfterProjectiles += DrawMetaballs_AfterPlayers;
        On_Main.DrawDust += DrawMetaballs_AfterDusts;
        On_Main.DrawInfernoRings += DrawMetaballs_AfterEverything;
    }

    public override void PostSetupContent()
    {
        Circle = ModContent.Request<Texture2D>("AAModClassic/Utilities/Metaballs/BasicCircle");
    }

    public override void Unload()
    {
        metaballs.Clear();

        Main.QueueMainThreadAction(() =>
        {
            maskTarget?.Dispose();
            maskTarget = null;
            sceneBackup?.Dispose();
            sceneBackup = null;
        });
    }

    private void PrepareMetaballs(On_Main.orig_CheckMonoliths orig)
    {
        orig();

        foreach (var metaball in metaballs)
            metaball.PreDrawCallContent();
    }

    private void DrawMetaballs_BeforeAllTiles(On_Main.orig_DrawBackgroundBlackFill orig, Main self)
    {
        Main.spriteBatch.End(out var ss);
        DrawMetaballs(DrawLayer.BeforeAllTiles);
        Main.spriteBatch.Begin(ss);
        orig(self);
    }

    private void DrawMetaballs_BeforeSolidTiles(On_Main.orig_DoDraw_Tiles_Solid orig, Main self)
    {
        DrawMetaballs(DrawLayer.BeforeSolidTiles);
        orig(self);
    }

    private void DrawMetaballs_NPCs(On_Main.orig_DoDraw_DrawNPCsOverTiles orig, Main self)
    {
        DrawMetaballs(DrawLayer.BeforeNPCs);
        orig(self);
        DrawMetaballs(DrawLayer.AfterNPCs);
    }

    private void DrawMetaballs_Projectiles(On_Main.orig_DrawProjectiles orig, Main self)
    {
        DrawMetaballs(DrawLayer.BeforeProjectiles);
        orig(self);
        DrawMetaballs(DrawLayer.AfterProjectiles);
    }

    private void DrawMetaballs_AfterPlayers(On_Main.orig_DrawPlayers_AfterProjectiles orig, Main self)
    {
        orig(self);
        DrawMetaballs(DrawLayer.AfterPlayers);
    }

    private void DrawMetaballs_AfterDusts(On_Main.orig_DrawDust orig, Main self)
    {
        orig(self);
        DrawMetaballs(DrawLayer.AfterDusts);
    }

    private void DrawMetaballs_AfterEverything(On_Main.orig_DrawInfernoRings orig, Main self)
    {
        orig(self);
        Main.spriteBatch.End(out var ss);
        DrawMetaballs(DrawLayer.AfterEverything);
        Main.spriteBatch.Begin(ss);
    }

    public override void PreUpdateEntities()
    {
        if (Main.dedServ)
            return;

        // Get a list of all metaballs currently in use.
        var activeMetaballs = metaballs.Where(m => m.ShouldRender);

        // Don't bother wasting resources if metaballs are not in use at the moment.
        if (!activeMetaballs.Any())
            return;

        foreach (var metaball in activeMetaballs)
            metaball.Update();
    }

    public override void OnWorldUnload()
    {
        foreach (Metaball metaball in metaballs)
            metaball.ClearInstances();
    }

    private static void DrawMetaballs(DrawLayer targetLayer)
    {
        if (Main.gameMenu && (!GameShaders.Misc.ContainsKey("AAModClassic:Masking") || !GameShaders.Misc.ContainsKey("AAModClassic:MaskEdge")))
            return;

        var activeMetaballs = metaballs.Where(m => m.ShouldRender && m.DrawLayers.Contains(targetLayer));

        if (!activeMetaballs.Any())
            return;

        GraphicsDevice device = Main.instance.GraphicsDevice;

        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, Main.Rasterizer, null, Main.Transform);

        foreach (var metaball in metaballs)
        {
            if (!metaball.ShouldRender)
                continue;

            for (int i = 0; i < metaball.DrawLayers.Length; i++)
            {
                if (metaball.DrawLayers[i] != targetLayer)
                    continue;

                // Don't render when there's nothing to mask.
                if (metaball.FillDrawCalls[i].Count == 0 && metaball.EdgeDrawCalls[i].Count == 0)
                    continue;

                Main.spriteBatch.End();

                RenderTarget2D mask = GetMaskTarget();

                RenderTargetBinding[] previousTargets = device.GetRenderTargets();
                Viewport previousViewport = device.Viewport;

                RenderTarget2D sceneTarget = previousTargets.Length > 0 ? previousTargets[0].RenderTarget as RenderTarget2D : null;
                RenderTarget2D backup = null;

                if (sceneTarget is not null)
                {
                    backup = GetSceneBackup(device, sceneTarget);
                    device.SetRenderTarget(backup);
                    CopyTexture(sceneTarget);
                }

                //Construct Mask
                device.SetRenderTarget(mask);
                device.Clear(Color.Black);

                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, Main.Rasterizer, null, Main.Transform);

                foreach (MetaballDrawCommand command in metaball.FillDrawCalls[i])
                    command.Draw(Main.spriteBatch, Color.White);

                Main.spriteBatch.End();

                if (previousTargets.Length == 0)
                    device.SetRenderTarget(null);
                else
                    device.SetRenderTargets(previousTargets);

                device.Viewport = previousViewport;

                //Put the scene back exactly as it was
                if (backup is not null)
                    CopyTexture(backup);

                //Apply masking shader
                MiscShaderData maskingShader = GameShaders.Misc["AAModClassic:Masking"];
                maskingShader.Shader.Parameters["screenSize"]?.SetValue(new Vector2(Main.screenWidth, Main.screenHeight));

                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.Default, Main.Rasterizer, maskingShader.Shader, Main.Transform);

                device.Textures[1] = mask;
                device.SamplerStates[1] = SamplerState.PointClamp;

                //draw texture layers
                int layerNum = 0;
                foreach (var layer in metaball.LayerTextures)
                {
                    Vector2 offset = metaball.CalculateManualOffsetForLayer(layerNum);
                    if (!metaball.FixedToScreen)
                        offset += Main.screenPosition;

                    Main.spriteBatch.Draw(layer, new Rectangle(-2, -2, Main.screenWidth + 2, Main.screenHeight + 2), new Rectangle((int)offset.X % layer.Width, (int)offset.Y % layer.Height, Main.screenWidth, Main.screenHeight), metaball.DrawLayerColor(i));

                    layerNum++;
                }

                //Reset for edge drawing
                MiscShaderData edgeShader = GameShaders.Misc["AAModClassic:MaskEdge"];
                edgeShader.Shader.Parameters["screenSize"]?.SetValue(new Vector2(Main.screenWidth, Main.screenHeight));
                edgeShader.Shader.Parameters["edgeWidth"]?.SetValue(1);

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.Default, Main.Rasterizer, edgeShader.Shader, Matrix.Identity);

                Main.spriteBatch.Draw(mask, new Rectangle(-1, -1, Main.screenWidth + 2, Main.screenHeight + 2), metaball.EdgeColor(i));

                //reset the sprite batch for the next iteration.
                Main.spriteBatch.End();

                device.Textures[1] = null;
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, Main.Rasterizer, null, Main.Transform);

                foreach (MetaballDrawCommand command in metaball.EdgeDrawCalls[i])
                    command.Draw(Main.spriteBatch, metaball.EdgeColor(i));

                metaball.FillDrawCalls[i].Clear();
                metaball.EdgeDrawCalls[i].Clear();
                metaball.PostDrawCallContent(Main.spriteBatch, i);
            }
        }

        Main.spriteBatch.End();
    }

    public static void AddMetaballFill<T>(MetaballDrawCommand command, byte layer) where T : Metaball
    {
        ModContent.GetInstance<T>().AddMetaballFill(command, layer);
    }

    public static void AddMetaballEdge<T>(MetaballDrawCommand command, byte layer) where T : Metaball
    {
        ModContent.GetInstance<T>().AddMetaballEdge(command, layer);
    }
}

public struct MetaballDrawCommand
{
    public Texture2D Texture;
    public Vector2 Position;
    public Rectangle? Frame;
    public float Rotation;
    public Vector2 Origin;
    public Vector2 Scale;
    public SpriteEffects Effect;

    public MetaballDrawCommand(Texture2D tex, Vector2 pos, Rectangle? frame, float rot, Vector2 orig, Vector2 scale, SpriteEffects eff)
    {
        Texture = tex;
        Position = pos;
        Frame = frame;
        Rotation = rot;
        Origin = orig;
        Scale = scale;
        Effect = eff;
    }

    public MetaballDrawCommand(Texture2D tex, Vector2 pos, Rectangle? frame, float rot, Vector2 orig, float scale, SpriteEffects eff)
    {
        Texture = tex;
        Position = pos;
        Frame = frame;
        Rotation = rot;
        Origin = orig;
        Scale = scale * Vector2.One;
        Effect = eff;
    }

    public MetaballDrawCommand(Projectile projectile)
    {
        Texture = TextureAssets.Projectile[projectile.type].Value;
        Position = projectile.Center - Main.screenPosition;
        Frame = null;
        Rotation = projectile.rotation;
        Origin = Texture.Size() * 0.5f;
        Scale = projectile.scale * Vector2.One;
        Effect = 0;
    }

    public readonly void Draw(SpriteBatch sb, Color color)
    {
        sb.Draw(Texture, Position, Frame, color, Rotation, Origin, Scale, Effect, 0);
    }
}

public abstract class Metaball : ModType
{
    protected override void Register()
    {
        ModTypeLookup<Metaball>.Register(this);

        MetaballSystem.metaballs.Add(this);

        if (Main.dedServ)
            return;

        FillDrawCalls = new List<MetaballDrawCommand>[DrawLayers.Length];

        for (int i = 0; i < DrawLayers.Length; i++)
        {
            FillDrawCalls[i] = [];
            FillDrawCalls[i].EnsureCapacity(300);
        }

        EdgeDrawCalls = new List<MetaballDrawCommand>[DrawLayers.Length];
        for (int i = 0; i < DrawLayers.Length; i++)
            EdgeDrawCalls[i] = [];
    }

    internal List<MetaballDrawCommand>[] FillDrawCalls = [];
    internal List<MetaballDrawCommand>[] EdgeDrawCalls = [];

    public abstract bool ShouldRender
    {
        get;
    }

    // This uses a Texture2D rather than an Asset to allow for the usage of render targets when working with metaballs.
    public abstract IEnumerable<Texture2D> LayerTextures
    {
        get;
    }

    public virtual Vector2 CalculateManualOffsetForLayer(int layerIndex) => Vector2.Zero;

    public abstract Color EdgeColor(int drawLayer);

    /// <summary>
    /// Whether the layer overlay contents from <see cref="Layers"/> should be fixed to the screen.<br></br>
    /// When true, the texture will be statically drawn to the screen with no respect for world position.
    /// </summary>
    public virtual bool FixedToScreen => false;

    public abstract DrawLayer[] DrawLayers { get; }

    public virtual Color DrawLayerColor(int layer) => Color.White;

    internal void AddMetaballFill(MetaballDrawCommand command, int layer) => FillDrawCalls[layer].Add(command);
    internal void AddMetaballEdge(MetaballDrawCommand command, int layer) => EdgeDrawCalls[layer].Add(command);

    internal virtual void Update() { }

    internal virtual void PreDrawCallContent() { }

    internal virtual void PostDrawCallContent(SpriteBatch spritebatch, int layer) { }

    internal virtual void ClearInstances() { }
}