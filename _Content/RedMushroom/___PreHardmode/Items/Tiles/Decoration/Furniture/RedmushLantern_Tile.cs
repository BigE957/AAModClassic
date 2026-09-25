using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushLantern_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetUpLantern(ModContent.ItemType<RedmushLantern>(), true);
            DustType = ModContent.DustType<MushDust>();

        }
        public override void HitWire(int i, int j) => FurnitureUtils.LightHitWire(Type, i, j, 1, 2);

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            if (tile.TileFrameX == 0)
            {
                r = 1.1f;
                g = 0.5f;
                b = 0.5f;
            }
        }
    }
}