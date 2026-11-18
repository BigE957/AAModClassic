using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace AAModClassic.Utilities
{
    public sealed class SceneCaptureSlot
    {
        public string Name { get; }

        public Func<bool> Needed { get; }

        public Func<Action<SpriteBatch>> Overlay { get; init; }

        public Func<SpriteBatchSnapshot, SpriteBatchSnapshot> OverlayState { get; init; }

        internal RenderTarget2D target;
        internal bool ready;

        public RenderTarget2D Target => ready ? target : null;

        internal SceneCaptureSlot(string name, Func<bool> needed)
        {
            Name = name;
            Needed = needed;
        }
    }

    [Autoload(Side = ModSide.Client)]
    public class SceneCapture : ModSystem
    {
        private static readonly List<SceneCaptureSlot> slots = [];
        private static readonly List<(SceneCaptureSlot slot, Action<SpriteBatch> overlay)> active = [];

        private static RenderTarget2D filteredScene;
        private static bool filteredValid;

        public static RenderTarget2D CurrentScene => filteredValid ? filteredScene : Main.screenTarget;

        public static SceneCaptureSlot Register(string name, Func<bool> needed, Func<Action<SpriteBatch>> overlay = null, Func<SpriteBatchSnapshot, SpriteBatchSnapshot> overlayState = null)
        {
            var slot = new SceneCaptureSlot(name, needed) { Overlay = overlay, OverlayState = overlayState };
            slots.Add(slot);
            return slot;
        }

        public override void Load() => On_FilterManager.EndCapture += OnEndCapture;

        public override void Unload()
        {
            On_FilterManager.EndCapture -= OnEndCapture;

            List<RenderTarget2D> toDispose = [filteredScene];
            foreach (var slot in slots)
            {
                toDispose.Add(slot.target);
                slot.target = null;
                slot.ready = false;
            }

            filteredScene = null;
            filteredValid = false;
            slots.Clear();
            active.Clear();

            Main.QueueMainThreadAction(() =>
            {
                foreach (var t in toDispose)
                    t?.Dispose();
            });
        }

        public override void PostDrawInterface(SpriteBatch spriteBatch) => filteredValid = false;

        private static bool AnyNeeded()
        {
            foreach (var slot in slots)
                if (slot.Needed())
                    return true;
            return false;
        }

        private static void OnEndCapture(On_FilterManager.orig_EndCapture orig, FilterManager self, RenderTarget2D finalTexture, RenderTarget2D screenTarget1, RenderTarget2D screenTarget2, Color clearColor)
        {
            filteredValid = false;
            foreach (var slot in slots)
                slot.ready = false;

            if (finalTexture != null || screenTarget1 == null || !AnyNeeded())
            {
                orig(self, finalTexture, screenTarget1, screenTarget2, clearColor);
                return;
            }

            GraphicsDevice gd = Main.instance.GraphicsDevice;
            SpriteBatch sb = Main.spriteBatch;

            EnsureTarget(gd, ref filteredScene, screenTarget1, RenderTargetUsage.PreserveContents);

            if (self.CanCapture())
            {
                orig(self, filteredScene, screenTarget1, screenTarget2, clearColor);

                CaptureSlots(sb, gd, filteredScene);

                gd.SetRenderTarget(null);
                BlitOpaque(sb, filteredScene);

                filteredValid = true;
            }
            else
            {
                orig(self, finalTexture, screenTarget1, screenTarget2, clearColor);

                RenderTargetBinding[] previousTargets = gd.GetRenderTargets();
                Viewport previousViewport = gd.Viewport;
                bool sceneIsBound = previousTargets.Length == 0 || previousTargets[0].RenderTarget == screenTarget1;

                gd.SetRenderTarget(filteredScene);
                gd.Clear(Color.Transparent);
                BlitOpaque(sb, screenTarget1);

                CaptureSlots(sb, gd, filteredScene);

                gd.SetRenderTargets(previousTargets);

                if (sceneIsBound)
                    BlitOpaque(sb, filteredScene);

                gd.Viewport = previousViewport;
            }
        }

        private static void CaptureSlots(SpriteBatch sb, GraphicsDevice gd, RenderTarget2D source)
        {
            if (source == null || source.IsDisposed)
                return;

            active.Clear();
            foreach (var slot in slots)
                if (slot.Needed())
                    active.Add((slot, slot.Overlay?.Invoke()));

            if (active.Count == 0)
                return;

            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.Identity);
            sb.End(out var baseState);

            foreach (var (slot, overlay) in active)
            {
                EnsureTarget(gd, ref slot.target, source);
                var state = slot.OverlayState != null ? slot.OverlayState(baseState) : baseState;
                RenderScene(sb, gd, slot.target, source, overlay, state);
                slot.ready = true;
            }
        }

        private static void BlitOpaque(SpriteBatch sb, Texture2D texture)
        {
            sb.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp, null, null);
            sb.Draw(texture, Vector2.Zero, Color.White);
            sb.End();
        }

        private static void EnsureTarget(GraphicsDevice gd, ref RenderTarget2D target, RenderTarget2D source, RenderTargetUsage usage = RenderTargetUsage.DiscardContents)
        {
            if (target == null || target.IsDisposed || target.Width != source.Width || target.Height != source.Height)
            {
                target?.Dispose();
                target = new RenderTarget2D(gd, source.Width, source.Height, false, source.Format, DepthFormat.None, 0, usage);
            }
        }

        private static void RenderScene(SpriteBatch sb, GraphicsDevice gd, RenderTarget2D target, RenderTarget2D source, Action<SpriteBatch> overlay, SpriteBatchSnapshot overlayState)
        {
            gd.SetRenderTarget(target);
            gd.Clear(Color.Transparent);

            var copySnap = overlayState;
            copySnap.SortMode = SpriteSortMode.Deferred;
            copySnap.BlendState = BlendState.Opaque;
            copySnap.CustomEffect = null;
            copySnap.TransformMatrix = Matrix.Identity;
            sb.Begin(copySnap);
            sb.Draw(source, Vector2.Zero, Color.White);
            sb.End();

            if (overlay != null)
            {
                sb.Begin(overlayState);
                overlay(sb);
                sb.End();
            }
        }
    }
}