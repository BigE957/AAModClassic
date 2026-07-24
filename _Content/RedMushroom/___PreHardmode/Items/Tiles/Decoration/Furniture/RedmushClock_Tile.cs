using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushClock_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetUpClock(ModContent.ItemType<RedmushClock>());
            DustType = ModContent.DustType<MushDust>();
        }

        public override void MouseOver(int i, int j) => FurnitureUtils.MouseOver(i, j, ModContent.ItemType<RedmushClock>());

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

        public override bool RightClick(int x, int y) => FurnitureUtils.ClockRightClick();

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
                Main.SceneMetrics.HasClock = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }
}