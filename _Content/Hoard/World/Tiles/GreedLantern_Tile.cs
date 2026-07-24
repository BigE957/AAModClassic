using AAModClassic.Utilities;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard.World.Tiles
{
    public class GreedLantern_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetUpLantern(ModContent.ItemType<GreedLantern>(), true);
            DustType = DustID.Gold;

        }
        public override void HitWire(int i, int j) => FurnitureUtils.LightHitWire(Type, i, j, 1, 2);

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0.6f;
            g = 0.4f;
            b = 0.0f;
        }
    }
}