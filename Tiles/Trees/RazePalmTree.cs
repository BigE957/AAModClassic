using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAMod.Tiles.Trees
{
    class RazePalmTree : ModPalmTree
    {
        public override TreePaintingSettings TreeShaderSettings => new();

        public override void SetStaticDefaults()
        {
            GrowsOnTileId = [ModContent.TileType<Torchsand>()];
        }

        public override int DropWood()
        {
            return AAMod.instance.Find<ModItem>("Razewood").Type;
        }

        public override Asset<Texture2D> GetTexture()
        {
            
            return ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/RazePalmTree");
        }

        public override Asset<Texture2D> GetTopTextures()
        {
            return ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/RazePalmTreetops");
        }

        public override Asset<Texture2D> GetOasisTopTextures()
        {
            return ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/RazePalmTreetops");
        }

        public override int SaplingGrowthType(ref int style)/* tModPorter Note: Removed. Use ModTree.SaplingGrowthType */
        {
            style = 0;
            return AAMod.instance.Find<ModTile>("RazePalmSapling").Type;
        }
    }
}
