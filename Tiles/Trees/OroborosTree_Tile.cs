using AAModClassic.Tiles.Ore;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Trees
{
    class OroborosTree_Tile : ModTree
    {
        public override void SetStaticDefaults()
        {
            GrowsOnTileId = [ModContent.TileType<ApocalyptiteOre_Tile>(), ModContent.TileType<DoomGrass_Tile>(), ModContent.TileType<Doomstone_Tile>(), ModContent.TileType<DoomstoneB_Tile>()];
        }

        public override TreePaintingSettings TreeShaderSettings => new();

        public override int DropWood()
        {
            return AAMod.instance.Find<ModItem>("OroborosWood").Type;
        }

        public override Asset<Texture2D> GetTexture()
        {
            return ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/OroborosTree");
        }

        public override Asset<Texture2D> GetBranchTextures()
        {
            return ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/OroborosBranches");
        }

        public override Asset<Texture2D> GetTopTextures()
        {
            return ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/OroborosTreeTop");
        }

        public override int SaplingGrowthType(ref int style)/* tModPorter Note: _Unreleased. Use ModTree.SaplingGrowthType */
        {
            style = 0;
            return AAMod.instance.Find<ModTile>("OroborosSapling").Type;
        }
    }
}
