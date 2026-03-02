using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace AAModClassic.UI.Core
{
    internal sealed class UIColorImage : UIImage
    {
        private Texture2D texture;
        private Color color;
        private Rectangle? frame;
        private Vector2 size;

        public UIColorImage(Texture2D texture, Color color, Rectangle? frame = null)
            : base(texture)
        {
            this.texture = texture;
            this.color = color;
            this.frame = frame;
            size = (frame != null) ? frame.Value.Size() : texture.Size();
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public new void SetImage(Texture2D texture)
        {
            this.texture = texture;
            Width.Set(this.texture.Width, 0f);
            Height.Set(this.texture.Height, 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            spriteBatch.Draw(texture, dimensions.Position() + (size * (1f - ImageScale) / 2f), frame, color, 0f, Vector2.Zero, ImageScale, SpriteEffects.None, 0f);
        }
    }
}