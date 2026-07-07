using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.UI;

namespace AAModClassic._Unofficial.Desert
{
    public class FrameableUIImage : UIElement
    {
        private Asset<Texture2D> _texture;
        public Rectangle frame = Rectangle.Empty;
        public Vector2 ImageScale = Vector2.One;
        public float Rotation;
        public bool ScaleToFit;
        public bool AllowResizingDimensions = true;
        public Color Color = Color.White;
        public Vector2 NormalizedOrigin = Vector2.Zero;
        public bool RemoveFloatingPointsFromDrawPosition;
        private Texture2D _nonReloadingTexture;

        public FrameableUIImage(Asset<Texture2D> texture)
        {
            SetImage(texture);
        }

        public FrameableUIImage(Texture2D nonReloadingTexture)
        {
            SetImage(nonReloadingTexture);
        }

        public void SetImage(Asset<Texture2D> texture)
        {
            _texture = texture;
            _nonReloadingTexture = null;
            if (AllowResizingDimensions)
            {
                Width.Set(_texture.Width(), 0f);
                Height.Set(_texture.Height(), 0f);
            }
        }

        public void SetImage(Texture2D nonReloadingTexture)
        {
            _texture = null;
            _nonReloadingTexture = nonReloadingTexture;
            if (AllowResizingDimensions)
            {
                Width.Set(_nonReloadingTexture.Width, 0f);
                Height.Set(_nonReloadingTexture.Height, 0f);
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            var dimensions = GetDimensions();
            Texture2D texture2D = null;
            if (_texture != null)
                texture2D = _texture.Value;

            if (_nonReloadingTexture != null)
                texture2D = _nonReloadingTexture;

            if (ScaleToFit)
            {
                spriteBatch.Draw(texture2D, dimensions.ToRectangle(), Color);
                return;
            }

            var size = frame == Rectangle.Empty ? texture2D.Size() : frame.Size();
            var position = dimensions.Position();// + size * (Vector2.One - ImageScale) / 2f + size * NormalizedOrigin;
            if (RemoveFloatingPointsFromDrawPosition)
                position = position.Floor();

            spriteBatch.Draw(texture2D, position, frame == Rectangle.Empty ? null : frame, Color, Rotation, size * NormalizedOrigin, ImageScale, SpriteEffects.None, 0f);
        }
    }
}
