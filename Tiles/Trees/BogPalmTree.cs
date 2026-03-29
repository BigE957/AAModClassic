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

        public override int DropWood() => AAMod.instance.Find<ModItem>("Bogwood").Type;

        public override Asset<Texture2D> GetTexture() => ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/BogPalmTree");

        public override Asset<Texture2D> GetTopTextures() => ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/BogPalmTreetops");

        public override Asset<Texture2D> GetOasisTopTextures() => ModContent.Request<Texture2D>("AAModClassic/Tiles/Trees/BogPalmTreetopsOasis");

        public override int SaplingGrowthType(ref int style)/* tModPorter Note: Removed. Use ModTree.SaplingGrowthType */
        {
            style = 0;
            return AAMod.instance.Find<ModTile>("BogwoodSapling").Type;
        }
    }
}
