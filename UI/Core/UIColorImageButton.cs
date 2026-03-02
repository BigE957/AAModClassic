using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace AAModClassic.UI.Core
{
    internal sealed class UIColorImageButton : UIImageButton
    {
        private Texture2D texture;
        private Color color;

        public UIColorImageButton(Texture2D texture, Color color)
            : base(texture)
        {
            this.texture = texture;
            this.color = color;
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
            spriteBatch.Draw(texture, dimensions.Position(), color);
        }
    }
}