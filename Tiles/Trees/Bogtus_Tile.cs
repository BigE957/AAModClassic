using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Trees
{
    public class Bogtus_Tile : ModCactus
	{
        public override void SetStaticDefaults()
        {
            GrowsOnTileId = [ModContent.TileType<Depthsand>()];
        }

        public override Asset<Texture2D> GetFruitTexture()
        {
            return null;
        }

        public override Asset<Texture2D> GetTexture()
		{
			return ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/Bogtus");
		}
    }
}