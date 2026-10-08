using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.UI.Dialogue.DisplayEffects
{
    public class AlwaysOnScreen : DisplayEffect
    {
        public override bool FadeWhenTooFar => false;
        public override float TimeToAppear => 20;
        public override bool DespawnWithAttachedNPC => false;

        public override Vector2 TextOffsetFromStart(Vector2 startPos, Vector2 textSize)
        {
            Vector2 originOffsetPosition = startPos - (textSize / 2f);

            int leftMargin = 60;
            int topMargin = 120;
            int rightMargin = 60;
            int bottomMargin = 20;

            float minX = Main.screenPosition.X + leftMargin;
            float maxX = Main.screenPosition.X + Main.screenWidth - rightMargin;
            float minY = Main.screenPosition.Y + topMargin;
            float maxY = Main.screenPosition.Y + Main.screenHeight - bottomMargin;

            if (originOffsetPosition.X < minX)
                originOffsetPosition.X = minX;
            else if (originOffsetPosition.X + textSize.X > maxX)
                originOffsetPosition.X = maxX - textSize.X;

            if (originOffsetPosition.Y < minY)
                originOffsetPosition.Y = minY;
            else if (originOffsetPosition.Y + textSize.Y > maxY)
                originOffsetPosition.Y = maxY - textSize.Y;

            return originOffsetPosition;
        }

        public override Vector2 AppearPositioning(Vector2 startPos, Vector2 goalPos, float time, DialogueCharacterData charData)
        {
            return Vector2.Lerp(goalPos - (new Vector2(-1, -1) * 24 * charData.Scale), goalPos, MathUtils.SineOutEasing(time / TimeToAppear));
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
            if (textTimer <= 30f)
                Opacity = MathHelper.Lerp(0f, 1f, MathUtils.SineOutEasing(textTimer / 30f));
            else if (switchTimer > 0)
                Opacity = 1 - MathUtils.SineInEasing(switchTimer / 30f);

            DialogueUIEffect.DrawBloom(spriteBatch, textStart + textSize * 0.5f - Main.screenPosition, textSize, Opacity);
        }
    }

}
