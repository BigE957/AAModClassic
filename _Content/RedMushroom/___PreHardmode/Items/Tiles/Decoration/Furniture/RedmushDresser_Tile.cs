using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushDresser_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetUpDresser(ModContent.ItemType<RedmushDresser>());
            DustType = ModContent.DustType<MushDust>();
        }

        public override LocalizedText DefaultContainerName(int i, int j) => ModContent.GetModItem(ModContent.ItemType<RedmushDresser>()).DisplayName;
        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;
        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
        public override void MouseOver(int i, int j) => FurnitureUtils.DresserMouseOver<RedmushDresser>();
        public override void MouseOverFar(int i, int j) => FurnitureUtils.DresserMouseFar<RedmushDresser>();
        public override void KillMultiTile(int i, int j, int frameX, int frameY) => Chest.DestroyChest(i, j);
        public override bool RightClick(int i, int j) => FurnitureUtils.DresserRightClick();
    }
}