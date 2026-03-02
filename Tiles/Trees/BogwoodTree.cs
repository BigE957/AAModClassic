using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    class BogwoodTree : ModTree
    {
        public override TreePaintingSettings TreeShaderSettings => new();

        public override void SetStaticDefaults()
        {
            GrowsOnTileId = [ModContent.TileType<AbyssGrass>()];
        }

        public override int DropWood()
        {
            return AAMod.instance.Find<ModItem>("Bogwood").Type;
        }

        public override Asset<Texture2D> GetTexture()
        {
            return ModContent.Request<Texture2D>("Tiles/Trees/BogwoodTree");
        }

        public override Asset<Texture2D> GetBranchTextures()
        {
            return ModContent.Request<Texture2D>("Tiles/Trees/BogwoodBranches");
        }

        public override Asset<Texture2D> GetTopTextures()
        {
            return ModContent.Request<Texture2D>("Tiles/Trees/BogwoodTreeTop");
        }

        public override int SaplingGrowthType(ref int style)/* tModPorter Note: Removed. Use ModTree.SaplingGrowthType */
        {
            style = 0;
            return AAMod.instance.Find<ModTile>("BogwoodSapling").Type;
        }
    }
}
