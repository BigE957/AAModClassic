using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Trees
{
    class BogPalmTree : ModPalmTree
    {
        public override TreePaintingSettings TreeShaderSettings => new();

        public override void SetStaticDefaults()
        {
            GrowsOnTileId = [ModContent.TileType<DepthMoss>(), ModContent.TileType<Depthsand>()];
        }

        public override int DropWood()
        {
            return AAMod.instance.Find<ModItem>("Bogwood").Type;
        }

        public override Asset<Texture2D> GetOasisTopTextures()
        {
            return ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/BogPalmTreetops");
        }

        public override Asset<Texture2D> GetTexture()
        {
            return ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/BogPalmTree");
        }

        public override Asset<Texture2D> GetTopTextures()
        {
            return ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/BogPalmTreetops");
        }

        public override int SaplingGrowthType(ref int style)/* tModPorter Note: Removed. Use ModTree.SaplingGrowthType */
        {
            style = 0;
            return AAMod.instance.Find<ModTile>("BogwoodSapling").Type;
        }
    }
}
