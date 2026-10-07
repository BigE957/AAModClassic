using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Graphics.Effects;
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

            Texture2D tex = ModContent.Request<Texture2D>("AAModClassic/Assets/General/SmallBloom").Value;
            Vector2 center = drawPos + drawSize * 0.5f;
            Vector2 scale = new(drawSize.X / 160f, drawSize.Y / 120f);

            Effect effect = Filters.Scene["AAModClassic:DialogueBloom"].GetShader().Shader;
            RenderTarget2D screen = Main.screenTarget;

            if (effect == null || screen == null || screen.IsDisposed)
            {
                spriteBatch.Draw(tex, center, null, Color.Black * 0.6f * Opacity, 0f, tex.Size() * 0.5f, scale, 0, 0);
                return;
            }

            spriteBatch.End(out var snap);

            // World plus the portrait, so the shader sees what is really on screen.
            RenderTarget2D scene = DialogueSceneCapture.Capture(spriteBatch, screen, SceneOverlay, snap);

            Vector2 quadSize = tex.Size() * scale;
            Vector2 quadMin = center - quadSize * 0.5f;
            Matrix view = snap.TransformMatrix;
            Vector2 a = Vector2.Transform(quadMin, view);
            Vector2 b = Vector2.Transform(quadMin + quadSize, view);
            Vector2 sceneSize = new(scene.Width, scene.Height);

            effect.Parameters["Position"].SetValue(a / sceneSize);
            effect.Parameters["Size"].SetValue((b - a) / sceneSize);
            effect.Parameters["Threshold"].SetValue(LumaThreshold);
            effect.Parameters["Ceiling"].SetValue(LumaCeiling);
            effect.Parameters["BaseDarken"].SetValue(BaseDarken);
            effect.Parameters["Opacity"].SetValue(Opacity);
            effect.Parameters["ShadowTint"].SetValue(ShadowTint);
            effect.Parameters["TintStrength"].SetValue(TintStrength);
            effect.Parameters["ChromaBoost"].SetValue(ChromaBoost);

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
