using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Trees
{
    class RazewoodPalmTree_Tile : ModPalmTree
    {
        public override TreePaintingSettings TreeShaderSettings => new();

        public override void SetStaticDefaults()
        {
            GrowsOnTileId = [ModContent.TileType<Torchsand_Tile>()];
        }

        public override int DropWood() => AAMod.instance.Find<ModItem>("Razewood").Type;

        public override Asset<Texture2D> GetTexture() => ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/RazePalmTree");

        public override Asset<Texture2D> GetTopTextures() => ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/RazePalmTreetops");

        public override Asset<Texture2D> GetOasisTopTextures() => ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/RazePalmTreetopsOasis");

        public override int SaplingGrowthType(ref int style)/* tModPorter Note: _Unreleased. Use ModTree.SaplingGrowthType */
        {
            style = 0;
            return AAMod.instance.Find<ModTile>("RazePalmSapling").Type;
        }
    }
}
