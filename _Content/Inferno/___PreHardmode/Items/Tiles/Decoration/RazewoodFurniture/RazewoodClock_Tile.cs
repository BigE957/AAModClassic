using AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Decoration.OuroborosWoodFurniture;
using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration.RazewoodFurniture
{
    public class RazewoodClock_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetUpClock(ModContent.ItemType<RazewoodClock>());
            DustType = ModContent.DustType<RazewoodDust>();
        }

        public override void MouseOver(int i, int j) => FurnitureUtils.MouseOver(i, j, ModContent.ItemType<RazewoodClock>());

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