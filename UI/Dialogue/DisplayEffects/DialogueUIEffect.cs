using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using static AAModClassic.UI.Dialogue.DialogueUISystem;

namespace AAModClassic.UI.Dialogue.DisplayEffects
{
    public class DialogueUIEffect : DisplayEffect
    {
        public Vector2 ChangePosition = Vector2.Zero;
        public Vector2 ChangeSize = Vector2.Zero;
        public bool SkipIntroFade = false;
        public static float SwitchProgress(int switchTimer) => MathUtils.SineInOutEasing(MathHelper.Clamp(switchTimer / 30f, 0f, 1f));

        private static readonly BlendState MultiplyBlend = new()
        {
            ColorSourceBlend = Blend.Zero,
            ColorDestinationBlend = Blend.SourceColor,
            AlphaSourceBlend = Blend.Zero,
            AlphaDestinationBlend = Blend.One
        };

        public static float LumaThreshold => 0.35f;
        public static float LumaCeiling => 0.5f;
        public static float BaseDarken => 0.33f;
        public static Vector3 ShadowTint => new(0.70f, 0.78f, 1f);
        public static float TintStrength => 0.6f;
        public static float ChromaBoost => 0.8f;

        public Action<SpriteBatch> SceneOverlay;

        public override bool FadeWhenTooFar => false;

        public override float TimeToAppear => 20;

        public override bool DespawnWithAttachedNPC => false;

        public override Vector2 TextOffsetFromStart(Vector2 startPos, Vector2 textSize) => startPos;

        public override Vector2 AppearPositioning(Vector2 startPos, Vector2 goalPos, float time, DialogueCharacterData charData)
        {
            return Vector2.Lerp(goalPos + (new Vector2(24, 24) * charData.Scale), goalPos, MathUtils.SineOutEasing(time / TimeToAppear));
        }

        public override float AppearOpacity(float goalOpacity, float time, DialogueCharacterData charData)
        {
            return MathUtils.SineOutEasing(time / TimeToAppear);
        }

        public override Vector2 AppearScale(Vector2 goalScale, float time, DialogueCharacterData charData)
        {
            return Vector2.Lerp(goalScale * 0.75f, goalScale, MathUtils.ExpOutEasing(time / TimeToAppear));
        }

        float OffsetDisappearTime(float time, float ratio) => MathHelper.Clamp((time - (ratio * TimeToDisappear / 2f)) / (TimeToDisappear / 2f), 0f, 1f);

        public override Vector2 DisappearPositioning(Vector2 startPos, float time, DialogueCharacterData charData) => Vector2.Lerp(startPos, startPos + (Vector2.UnitX * 12 * charData.Scale), MathUtils.SineOutEasing(OffsetDisappearTime(time, charData.CompletionRatio)));

        public override float DisappearOpacity(float startOpacity, float time, DialogueCharacterData charData) => 1 - MathUtils.SineOutEasing(OffsetDisappearTime(time, charData.CompletionRatio));

        public override Vector2 DisappearScale(Vector2 startScale, float time, DialogueCharacterData charData) => Vector2.Lerp(startScale, startScale * 0.75f, MathUtils.ExpOutEasing(OffsetDisappearTime(time, charData.CompletionRatio)));

        public override void PreDraw(SpriteBatch spriteBatch, Vector2 textStart, Vector2 textSize, int textTimer, int switchTimer, bool closing)
        {
            if (textTimer < 0)
                return;

            float Opacity = 1f;
            Vector2 drawPos = textStart;
            Vector2 drawSize = textSize;

            if (textTimer < 30f && !SkipIntroFade)
                Opacity = MathUtils.SineOutEasing(textTimer / 30f);

            if (switchTimer > 0)
            {
                if (closing)
                    Opacity = MathHelper.Clamp(1 - MathUtils.SineInEasing(switchTimer / 30f), 0f, 1f);
                else
                {
                    float t = SwitchProgress(switchTimer);
                    drawPos += ChangePosition * t;
                    drawSize += ChangeSize * t;
                }
            }

            DrawBloom(spriteBatch, drawPos + drawSize * 0.5f, drawSize, Opacity, SceneOverlay != null);
        }

        private static Asset<Texture2D> bloomAsset;
        private static Texture2D BloomTexture => (bloomAsset ??= ModContent.Request<Texture2D>("AAModClassic/Assets/General/SmallBloom")).Value;

        private static Effect cachedEffect;
        private static EffectParameter pPosition, pSize, pOpacity, pThreshold, pCeiling, pBaseDarken, pShadowTint, pTintStrength, pChromaBoost;

        private static void CacheParameters(Effect effect)
        {
            if (effect == cachedEffect)
                return;

            cachedEffect = effect;
            pPosition = effect.Parameters["Position"];
            pSize = effect.Parameters["Size"];
            pOpacity = effect.Parameters["Opacity"];
            pThreshold = effect.Parameters["Threshold"];
            pCeiling = effect.Parameters["Ceiling"];
            pBaseDarken = effect.Parameters["BaseDarken"];
            pShadowTint = effect.Parameters["ShadowTint"];
            pTintStrength = effect.Parameters["TintStrength"];
            pChromaBoost = effect.Parameters["ChromaBoost"];
        }

        public static void DrawBloom(SpriteBatch spriteBatch, Vector2 center, Vector2 size, float opacity, bool useOverlayScene = false)
        {
            Texture2D tex = BloomTexture;
            Vector2 scale = new(size.X / 160f, size.Y / 120f);

            Effect effect = GameShaders.Misc["AAModClassic:DialogueBloom"].Shader;
            RenderTarget2D scene = useOverlayScene ? DialogueSceneCapture.OverlayScene : DialogueSceneCapture.PlainScene;

            if (effect == null || scene == null || scene.IsDisposed)
            {
                spriteBatch.Draw(tex, center, null, Color.Black * 0.6f * opacity, 0f, tex.Size() * 0.5f, scale, 0, 0);
                return;
            }

            CacheParameters(effect);

            spriteBatch.End(out var snap);

            Vector2 quadSize = tex.Size() * scale;
            Vector2 quadMin = center - quadSize * 0.5f;
            Matrix view = snap.TransformMatrix;
            Vector2 a = Vector2.Transform(quadMin, view);
            Vector2 b = Vector2.Transform(quadMin + quadSize, view);
            Vector2 sceneSize = new(scene.Width, scene.Height);

            pPosition.SetValue(a / sceneSize);
            pSize.SetValue((b - a) / sceneSize);
            pThreshold.SetValue(LumaThreshold);
            pCeiling.SetValue(LumaCeiling);
            pBaseDarken.SetValue(BaseDarken);
            pOpacity.SetValue(opacity);
            pShadowTint.SetValue(ShadowTint);
            pTintStrength.SetValue(TintStrength);
            pChromaBoost.SetValue(ChromaBoost);

            var shaderSnap = snap;
            shaderSnap.SortMode = SpriteSortMode.Immediate;
            shaderSnap.BlendState = MultiplyBlend;
            shaderSnap.CustomEffect = effect;
            spriteBatch.Begin(shaderSnap);

            GraphicsDevice gd = Main.instance.GraphicsDevice;
            gd.Textures[1] = scene;
            gd.SamplerStates[1] = SamplerState.LinearClamp;

            spriteBatch.Draw(tex, center, null, Color.White, 0f, tex.Size() * 0.5f, scale, 0, 0);

            spriteBatch.End();
            gd.Textures[1] = null;
            spriteBatch.Begin(snap);
        }
    }
}