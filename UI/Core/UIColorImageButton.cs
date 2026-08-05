using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace AAModClassic.UI.Core
{
    public sealed class UIColorImageButton : UIImageButton
    {
        private Asset<Texture2D> texture;
        private Color color;

        public UIColorImageButton(Asset<Texture2D> texture, Color color)
            : base(texture)
        {
            this.texture = texture;
            this.color = color;
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public new void SetImage(Asset<Texture2D> texture)
        {
            this.texture = texture;
            Width.Set(this.texture.Width(), 0f);
            Height.Set(this.texture.Height(), 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            spriteBatch.Draw(texture.Value, dimensions.Position(), color);
        }
    }
}