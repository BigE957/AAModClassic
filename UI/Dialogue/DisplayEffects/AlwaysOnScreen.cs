using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace AAModClassic.UI.Dialogue.DisplayEffects
{
    public class AlwaysOnScreen : DisplayEffect
    {
        Vector2 StartPosition;

        public override bool FadeWhenTooFar => false;

        public override Vector2 TextOffsetFromStart(Vector2 startPos, Vector2 textSize)
        {
            StartPosition = startPos;

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

        public override void PreDraw(SpriteBatch spriteBatch, Vector2 textTopLeft, Vector2 textSize, int textTimer, int switchTimer)
        {
            Texture2D tex = ModContent.Request<Texture2D>("AAModClassic/UI/Dialogue/Assets/DialogueArrow").Value;
            Vector2 textCenter = textTopLeft + textSize * 0.5f;
            Vector2 toStart = (StartPosition - textCenter).SafeNormalize(-Vector2.UnitY) * 64;
            spriteBatch.Draw(tex, textCenter + toStart - Main.screenPosition, null, Color.White, toStart.ToRotation(), tex.Size() * 0.5f, 1f, 0, 0);
        }
    }

}
