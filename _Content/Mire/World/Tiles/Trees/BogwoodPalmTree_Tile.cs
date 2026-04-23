using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.World.Tiles.Trees
{
    class BogwoodPalmTree_Tile : ModPalmTree
    {
        public override TreePaintingSettings TreeShaderSettings => new();

        public override void SetStaticDefaults()
        {
            GrowsOnTileId = [ModContent.TileType<DepthMoss_Tile>(), ModContent.TileType<Depthsand_Tile>()];
        }

        public override int DropWood() => AAMod.instance.Find<ModItem>("Bogwood").Type;

        public override Asset<Texture2D> GetTexture() => ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Tiles/Trees/BogwoodPalmTree_Tile");

        public override Asset<Texture2D> GetTopTextures() => ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Tiles/Trees/BogwoodPalmTree_Tile_Tops");

        public override Asset<Texture2D> GetOasisTopTextures() => ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Tiles/Trees/BogwoodPalmTree_Tile_Top_Oasis");

        public override int SaplingGrowthType(ref int style)/* tModPorter Note: _Unreleased. Use ModTree.SaplingGrowthType */
        {
            style = 0;
            return AAMod.instance.Find<ModTile>("BogwoodSapling_Tile").Type;
        }
    }
}
