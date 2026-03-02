using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace AAMod.Tiles.Trees
{
    public class Bogtus : ModCactus
	{
        public override void SetStaticDefaults()
        {
            throw new System.NotImplementedException();
        }

        public override Asset<Texture2D> GetFruitTexture()
        {
            return null;
        }

        public override Asset<Texture2D> GetTexture()
		{
			return ModContent.Request<Texture2D>("Tiles/Trees/Bogtus");
		}
    }
}