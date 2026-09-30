using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAModClassic.UI.Dialogue.DisplayEffects
{
    public class DialogueUIEffect : DisplayEffect
    {
        public Vector2 ChangePosition = Vector2.Zero;
        public Vector2 ChangeSize = Vector2.Zero;
        public bool SkipIntroFade = false;

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
                Opacity = MathUtils.CircOutEasing(textTimer / 30f);

            if (switchTimer > 0)
            {
                if (closing)
                    Opacity *= MathHelper.Clamp(1 - MathUtils.CircOutEasing(switchTimer / 60f), 0f, 1f);
                else
                {
                    float t = MathUtils.SineInOutEasing(MathHelper.Clamp(switchTimer / 60f, 0f, 1f));
                    drawPos += ChangePosition * t;
                    drawSize += ChangeSize * t;
                }
            }

            Texture2D tex = ModContent.Request<Texture2D>("AAModClassic/Assets/General/SmallBloom").Value;
            spriteBatch.Draw(tex, drawPos + drawSize * 0.5f, null, Color.Black * 0.6f * Opacity, 0f, tex.Size() * 0.5f, new Vector2(drawSize.X / 160f, drawSize.Y / 120f), 0, 0);
        }
    }
}
