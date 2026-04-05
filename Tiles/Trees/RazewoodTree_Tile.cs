using AAModClassic.Tiles;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;


namespace AAModClassic.Tiles.Trees
{
    class RazewoodTree_Tile : ModTree
    {
        public override TreePaintingSettings TreeShaderSettings => new();

        public override void SetStaticDefaults()
        {
            GrowsOnTileId = [ModContent.TileType<InfernoGrass>(), ModContent.TileType<TorchAsh>(), ModContent.TileType<TorchMoss>()];
        }

        public override int DropWood()
        {
            return AAMod.instance.Find<ModItem>("Razewood").Type;
        }

        public override Asset<Texture2D> GetTexture()
        {
            return ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/RazewoodTree");
        }

        public override Asset<Texture2D> GetBranchTextures()
        {
            return ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/RazewoodBranches");
        }

        public override Asset<Texture2D> GetTopTextures()
        {
            return ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/RazewoodTreetop");
        }

        public override int SaplingGrowthType(ref int style)/* tModPorter Note: _Unreleased. Use ModTree.SaplingGrowthType */
        {
            style = 0;
            return AAMod.instance.Find<ModTile>("RazewoodSapling").Type;
        }
    }
}
