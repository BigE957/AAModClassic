using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.UI;

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
        public const string LayerName = "AAModClassic: Scene Capture";

        private static readonly List<SceneCaptureSlot> slots = [];
        private static readonly List<(SceneCaptureSlot slot, Action<SpriteBatch> overlay)> active = [];

        private static RenderTarget2D filteredScene;
        private static bool filteredValid;
        private static bool capturedThisFrame;

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

            Main.QueueMainThreadAction(() =>
            {
                foreach (var t in toDispose)
                    t?.Dispose();
            });
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            layers.Insert(0, new LegacyGameInterfaceLayer(LayerName, () =>
            {
                EnsureCaptured(Main.spriteBatch);
                return true;
            }, InterfaceScaleType.Game));
        }

        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            capturedThisFrame = false;
            filteredValid = false;
        }

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
            capturedThisFrame = false;

            if (finalTexture != null || screenTarget1 == null || !self.CanCapture() || !AnyNeeded())
            {
                orig(self, finalTexture, screenTarget1, screenTarget2, clearColor);
                return;
            }

            var gd = Main.instance.GraphicsDevice;
            EnsureTarget(gd, ref filteredScene, screenTarget1);

            orig(self, filteredScene, screenTarget1, screenTarget2, clearColor);

            gd.SetRenderTarget(null);
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp, null, null);
            Main.spriteBatch.Draw(filteredScene, Vector2.Zero, Color.White);
            Main.spriteBatch.End();

            filteredValid = true;
        }

        public static void EnsureCaptured(SpriteBatch sb)
        {
            if (capturedThisFrame)
                return;
            capturedThisFrame = true;

            active.Clear();
            foreach (var slot in slots)
            {
                slot.ready = false;
                if (slot.Needed())
                    active.Add((slot, slot.Overlay?.Invoke()));
            }

            RenderTarget2D source = CurrentScene;
            if (active.Count == 0 || source == null || source.IsDisposed)
                return;

            GraphicsDevice gd = Main.instance.GraphicsDevice;
            sb.End(out var uiState);

            RenderTargetBinding[] previousTargets = gd.GetRenderTargets();
            Viewport previousViewport = gd.Viewport;

            foreach (var (slot, overlay) in active)
            {
                EnsureTarget(gd, ref slot.target, source);
                var state = slot.OverlayState != null ? slot.OverlayState(uiState) : uiState;
                RenderScene(sb, gd, slot.target, source, overlay, state);
                slot.ready = true;
            }

            gd.SetRenderTargets(previousTargets);
            gd.Viewport = previousViewport;

            if (previousTargets.Length == 0)
            {
                var restoreSnap = uiState;
                restoreSnap.SortMode = SpriteSortMode.Deferred;
                restoreSnap.BlendState = BlendState.Opaque;
                restoreSnap.CustomEffect = null;
                restoreSnap.TransformMatrix = Matrix.Identity;
                sb.Begin(restoreSnap);
                sb.Draw(source, Vector2.Zero, Color.White);
                sb.End();
            }

            sb.Begin(uiState);
        }

        private static void EnsureTarget(GraphicsDevice gd, ref RenderTarget2D target, RenderTarget2D source)
        {
            if (target == null || target.IsDisposed || target.Width != source.Width || target.Height != source.Height)
            {
                target?.Dispose();
                target = new RenderTarget2D(gd, source.Width, source.Height, false, source.Format, DepthFormat.None);
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