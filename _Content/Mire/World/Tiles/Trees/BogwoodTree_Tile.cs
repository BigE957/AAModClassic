using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.World.Tiles.Trees
{
    class BogwoodTree_Tile : ModTree
    {
        public override TreePaintingSettings TreeShaderSettings => new();

        public override void SetStaticDefaults()
        {
            GrowsOnTileId = [ModContent.TileType<AbyssGrass_Tile>(), ModContent.TileType<MireGrass_Tile>()];
        }

        public override int DropWood()
        {
            return AAMod.instance.Find<ModItem>("Bogwood").Type;
        }

        public override Asset<Texture2D> GetTexture()
        {
            return ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Tiles/Trees/BogwoodTree_Tile");
        }

        public override Asset<Texture2D> GetBranchTextures()
        {
            return ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Tiles/Trees/BogwoodTree_Tile_Branches");
        }

        public override Asset<Texture2D> GetTopTextures()
        {
            return ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Tiles/Trees/BogwoodTree_Tile_Top");
        }

        public override int SaplingGrowthType(ref int style)/* tModPorter Note: _Unreleased. Use ModTree.SaplingGrowthType */
        {
            style = 0;
            return AAMod.instance.Find<ModTile>("BogwoodSapling_Tile").Type;
        }
    }
}
