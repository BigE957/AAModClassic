using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Trees
{
    public class Razetus : ModCactus
	{
        public override void SetStaticDefaults()
        {
            GrowsOnTileId = [ModContent.TileType<Torchsand>()];
        }

        public override Asset<Texture2D> GetTexture()
		{
			return ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/Razetus");
		}

        public override Asset<Texture2D> GetFruitTexture()
        {
            return null;
        }
    }
}