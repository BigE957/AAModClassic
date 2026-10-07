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
            Vector2 playerPos = Main.LocalPlayer.Center;
            Vector2 halfSize = textSize * 0.5f;
            Vector2 newPos = startPos - halfSize + (Vector2.UnitY * -(textSize.Y + 36));
            Vector2 screenPos = newPos.ToScreenPosition();

            Vector2 boundTopLeftScreen = new((Main.screenWidth / 2f) - (Main.screenWidth / 2.5f), (Main.screenHeight / 2f) - (Main.screenHeight / 2.5f));

            if (screenPos.X < boundTopLeftScreen.X)
                newPos.X = playerPos.X - (Main.screenWidth / 2.5f);
            if (screenPos.Y < boundTopLeftScreen.Y)
                newPos.Y = playerPos.Y - (Main.screenHeight / 2.5f);

            if (newPos.X > playerPos.X + (Main.screenWidth / 2.5f) - textSize.X)
                newPos.X = playerPos.X + (Main.screenWidth / 2.5f) - textSize.X;
            if (newPos.Y > playerPos.Y + (Main.screenHeight / 2.5f) - textSize.Y)
                newPos.Y = playerPos.Y + (Main.screenHeight / 2.5f) - textSize.Y;

            return newPos;
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
