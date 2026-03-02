using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Tiles.Trees
{
    class RazePalmTree : ModPalmTree
    {
        private Mod mod => AAMod.instance;

        public override int DropWood()
        {
            return mod.Find<ModItem>("Razewood").Type;
        }

        public override Texture2D GetTexture()
        {
            
            return mod.GetTexture("Tiles/Trees/RazePalmTree");
        }

        public override Texture2D GetTopTextures()
        {
            return mod.GetTexture("Tiles/Trees/RazePalmTreetops");
        }
    }
}
