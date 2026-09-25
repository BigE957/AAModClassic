using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.UI.Dialogue.DisplayEffects
{
    public class BossText : DisplayEffect
    {
        public override bool FadeWhenTooFar => false;

        public override float TimeToAppear => 20;

        public override bool DespawnWithAttachedNPC => false;

        public override Vector2 TextOffsetFromStart(Vector2 startPos, Vector2 textSize)
        {
            Vector2 playerPos = Main.LocalPlayer.Center;
            Vector2 halfSize = textSize * 0.5f;
            Vector2 newPos = playerPos - halfSize + (Vector2.UnitY * (textSize.Y + 54));
            return newPos;
        }

        float OffsetAppearTime(float time, float ratio) => MathHelper.Clamp((time - (ratio * TimeToAppear / 6f)) / (TimeToAppear / 6f), 0f, 1f);

        public override Vector2 AppearPositioning(Vector2 startPos, Vector2 goalPos, float time, DialogueCharacterData charData)
        {
            Vector2 toBoss = (goalPos - startPos).SafeNormalize(-Vector2.UnitX);
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

        public override void PreDraw(SpriteBatch spriteBatch, Vector2 textStart, Vector2 textSize, int textTimer, int switchTimer)
        {
            if (textTimer < 0)
                return;

            float Opacity = 1f;
            if (textTimer < 30f)
                Opacity = MathHelper.Lerp(0f, 1f, MathUtils.CircOutEasing(textTimer / 30f));

            if (switchTimer > 0)
                Opacity *= 1 - MathUtils.CircOutEasing(switchTimer / 60f);

            Texture2D tex = ModContent.Request<Texture2D>("AAModClassic/Assets/General/SmallBloom").Value;
            spriteBatch.Draw(tex, textStart + textSize * 0.5f - Main.screenPosition, null, Color.Black * 0.6f * Opacity, 0f, tex.Size() * 0.5f, new Vector2(textSize.X / 160f, textSize.Y / 120f), 0, 0);
        }
    }
}
