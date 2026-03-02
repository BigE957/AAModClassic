using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Tiles.Trees
{
    class BogPalmTree : ModPalmTree
    {
        private Mod mod => AAMod.instance;

        public override int DropWood()
        {
            return mod.Find<ModItem>("Bogwood").Type;
        }

        public override Texture2D GetTexture()
        {

            return mod.GetTexture("Tiles/Trees/BogPalmTree");
        }

        public override Texture2D GetTopTextures()
        {
            return mod.GetTexture("Tiles/Trees/BogPalmTreetops");
        }
    }
}
