using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Particles;

public enum DrawLayer
{
    BeforeAllTiles,
    BeforeSolidTiles,
    BeforeNPCs,
    AfterNPCs,
    BeforeProjectiles,
    AfterProjectiles,
    AfterPlayers,
    AfterDusts,
    AfterEverything,
}

[Autoload(Side = ModSide.Client)]
public class ParticleSystem : ModSystem
{
    internal static readonly Dictionary<DrawLayer, List<Particle>> alphablendParticles = [];
    internal static readonly Dictionary<DrawLayer, List<Particle>> additiveParticles = [];
    private static readonly List<Particle> particleInstances = [];

    private const int MaxParticles = 500;
    private static int activeCount = 0;
    private static int spawnCounter = 0;

    public override void Load()
    {
        alphablendParticles.Clear();
        additiveParticles.Clear();
        particleInstances.Clear();

        for (int i = 0; i <= (int)DrawLayer.AfterEverything; i++)
            alphablendParticles.Add((DrawLayer)i, []);
        for (int i = 0; i <= (int)DrawLayer.AfterEverything; i++)
            additiveParticles.Add((DrawLayer)i, []);

        On_Main.DrawBackgroundBlackFill += DrawParticles_BeforeAllTiles;
        On_Main.DoDraw_Tiles_Solid += DrawParticles_BeforeSolidTiles;
        On_Main.DoDraw_DrawNPCsOverTiles += DrawParticles_NPCs;
        On_Main.DrawProjectiles += DrawParticles_Projectiles;
        On_Main.DrawPlayers_AfterProjectiles += DrawParticles_AfterPlayers;
        On_Main.DrawDust += DrawParticles_AfterDusts;
        On_Main.DrawInfernoRings += DrawParticles_AfterEverything;

        foreach (Type type in Mod.Code.GetTypes())
        {
            if (type.IsAbstract || !type.IsSubclassOf(typeof(Particle)))
                continue;

            Particle instance = (Particle)RuntimeHelpers.GetUninitializedObject(type);
            instance.Load();

            particleInstances.Add(instance);
        }
    }

    public override void Unload()
    {
        alphablendParticles.Clear();
        additiveParticles.Clear();

        foreach (Particle instance in particleInstances)
            instance.Unload();

        particleInstances.Clear();
    }

    private void DrawParticles_BeforeAllTiles(On_Main.orig_DrawBackgroundBlackFill orig, Main self)
    {
        Main.spriteBatch.End(out var ss);
        DrawParticles(DrawLayer.BeforeAllTiles);
        Main.spriteBatch.Begin(ss);
        orig(self);
    }

    private void DrawParticles_BeforeSolidTiles(On_Main.orig_DoDraw_Tiles_Solid orig, Main self)
    {
        DrawParticles(DrawLayer.BeforeSolidTiles);
        orig(self);
    }

    private void DrawParticles_NPCs(On_Main.orig_DoDraw_DrawNPCsOverTiles orig, Main self)
    {
        DrawParticles(DrawLayer.BeforeNPCs);
        orig(self);
        DrawParticles(DrawLayer.AfterNPCs);
    }

    private void DrawParticles_Projectiles(On_Main.orig_DrawProjectiles orig, Main self)
    {
        DrawParticles(DrawLayer.BeforeProjectiles);
        orig(self);
        DrawParticles(DrawLayer.AfterProjectiles);
    }

    private void DrawParticles_AfterPlayers(On_Main.orig_DrawPlayers_AfterProjectiles orig, Main self)
    {
        orig(self);
        DrawParticles(DrawLayer.AfterPlayers);
    }

    private void DrawParticles_AfterDusts(On_Main.orig_DrawDust orig, Main self)
    {
        orig(self);
        DrawParticles(DrawLayer.AfterDusts);
    }

    private void DrawParticles_AfterEverything(On_Main.orig_DrawInfernoRings orig, Main self)
    {
        orig(self);
        Main.spriteBatch.End(out var ss);
        DrawParticles(DrawLayer.AfterEverything);
        Main.spriteBatch.Begin(ss);
    }

    public override void PostUpdateDusts()
    {
        if (Main.dedServ) return;
        UpdateLayer(alphablendParticles);
        UpdateLayer(additiveParticles);
    }

    private static void UpdateLayer(Dictionary<DrawLayer, List<Particle>> dict)
    {
        foreach (List<Particle> layer in dict.Values)
        {
            foreach (Particle p in layer)
                p.Update();

            layer.RemoveAll(p => {
                if (!p.Active || p.Time >= p.Lifetime)
                {
                    activeCount--;
                    p.OnKill(false);
                    return true;
                }
                return false;
            });
        }
    }

    public override void OnWorldUnload()
    {
        foreach (List<Particle> p in alphablendParticles.Values)
            p.Clear();
        foreach (List<Particle> p in additiveParticles.Values)
            p.Clear();
        activeCount = 0;
        spawnCounter = 0;
    }

    private static void DrawParticles(DrawLayer targetLayer)
    {
        if (alphablendParticles[targetLayer].Count > 0)
        {
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);

            foreach (Particle p in alphablendParticles[targetLayer])
                p.Draw(Main.spriteBatch);

            Main.spriteBatch.End();
        }

        if (additiveParticles[targetLayer].Count > 0)
        {
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);

            foreach (Particle p in additiveParticles[targetLayer])
                p.Draw(Main.spriteBatch);

            Main.spriteBatch.End();
        }
    }

    public static void SpawnParticle(Particle p)
    {
        if (Main.dedServ)
            return;

        if (activeCount >= MaxParticles && !TryEvictOldest())
            return;

        p.SpawnOrder = spawnCounter++;
        (p.Additive ? additiveParticles : alphablendParticles)[p.Layer].Add(p);
        activeCount++;
        p.OnSpawn();
    }

    public static void SpawnParticle(Particle p, DrawLayer layer)
    {
        p.Layer = layer;
        SpawnParticle(p);
    }

    public static void RemoveParticle(Particle p)
    {
        if (p.Additive)
        {
            foreach (var v in additiveParticles.Values)
                foreach (Particle particle in v)
                    if (particle == p)
                        particle.Active = false;
        }
        else
        {
            foreach (var v in alphablendParticles.Values)
                foreach (Particle particle in v)
                    if (particle == p)
                        particle.Active = false;
        }
    }

    private static bool TryEvictOldest()
    {
        Particle oldest = null;
        List<Particle> oldestList = null;

        foreach (var dict in new[] { alphablendParticles, additiveParticles })
        {
            foreach (List<Particle> list in dict.Values)
            {
                foreach (Particle p in list)
                {
                    if (!p.Important && (oldest == null || p.SpawnOrder < oldest.SpawnOrder))
                    {
                        oldest = p;
                        oldestList = list;
                    }
                }
            }
        }

        if (oldest == null) return false;

        oldest.OnKill(true);
        oldestList.Remove(oldest);
        activeCount--;
        return true;
    }
}

public abstract class Particle
{
    public bool Active { get; set; } = true;
    public int Time { get; set; } = 0;
    public int Lifetime { get; set; } = 60;
    public float LifeRatio => Time / (float)Lifetime;
    public Vector2 Position;
    public Vector2 Velocity;
    public float Rotation;
    public Vector2 Scale;
    public Color Color = Color.White;
    public DrawLayer Layer { get; set; }
    public virtual bool Additive => false;
    public virtual bool Important => false;

    internal int SpawnOrder;

    public virtual void Load() { }
    public virtual void Unload() { }

    public virtual void OnSpawn() { }
    public virtual void Update()
    {
        Position += Velocity;
        Time++;
    }
    public virtual void OnKill(bool wasEvicted) { }
    public abstract void Draw(SpriteBatch spritebatch);
}