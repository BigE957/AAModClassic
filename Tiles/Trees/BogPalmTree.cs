using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAMod.Tiles.Trees
{
    class BogPalmTree : ModPalmTree
    {
        public override TreePaintingSettings TreeShaderSettings => new();

        public override void SetStaticDefaults()
        {

        }

        public override int DropWood()
        {
            return AAMod.instance.Find<ModItem>("Bogwood").Type;
        }

        public override Asset<Texture2D> GetOasisTopTextures()
        {
            return ModContent.Request<Texture2D>("Tiles/Trees/BogPalmTreetops");
        }

        public override Asset<Texture2D> GetTexture()
        {
            return ModContent.Request<Texture2D>("Tiles/Trees/BogPalmTree");
        }

        public override Asset<Texture2D> GetTopTextures()
        {
            return ModContent.Request<Texture2D>("Tiles/Trees/BogPalmTreetops");
        }
    }
}
