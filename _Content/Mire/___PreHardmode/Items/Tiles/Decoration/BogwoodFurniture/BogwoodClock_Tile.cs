using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration.BogwoodFurniture
{
    public class BogwoodClock_Tile : ModTile
	{
        public override void SetStaticDefaults()
        {
            this.SetUpClock(ModContent.ItemType<BogwoodClock>());
            DustType = ModContent.DustType<BogwoodDust>();
        }

        public override void MouseOver(int i, int j) => FurnitureCommon.MouseOver(i, j, ModContent.ItemType<BogwoodClock>());

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

        public override bool RightClick(int x, int y) => FurnitureCommon.ClockRightClick();

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
                Main.SceneMetrics.HasClock = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }
}