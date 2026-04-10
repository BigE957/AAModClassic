using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Trees
{
    class MushroomTree_Tile : ModTree
    {
        public override TreePaintingSettings TreeShaderSettings => new();

        public override void SetStaticDefaults()
        {
            GrowsOnTileId = [ModContent.TileType<Mycelium_Tile>()];
        }

        public override int DropWood()
        {
            return ItemID.Mushroom;
        }

        public override Asset<Texture2D> GetTexture()
        {
            return ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/MushroomTree_Tile");
        }

        public override Asset<Texture2D> GetBranchTextures()
        {
            return ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/MushroomTree_Tile_Branches");
        }

        public override Asset<Texture2D> GetTopTextures()
        {
            return ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/MushroomTree_Tile_Top");
        }

        public override int SaplingGrowthType(ref int style)/* tModPorter Note: _Unreleased. Use ModTree.SaplingGrowthType */
        {
            style = 0;
            return TileID.MushroomPlants; //TODO: This was formerly trying to find "MushroomTree"...
        }
    }
}
