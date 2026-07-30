using AAModClassic.Utilities;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration.BogwoodFurniture
{
    public class BogwoodDresser_Tile : ModTile
	{
		public override void SetStaticDefaults()
		{
            this.SetUpDresser(ModContent.ItemType<BogwoodDresser>());
            DustType = ModContent.DustType<Dusts.BogwoodDust>();
		}

        public override LocalizedText DefaultContainerName(int i, int j) => ModContent.GetModItem(ModContent.ItemType<BogwoodDresser>()).DisplayName;
        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;
        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
        public override void MouseOver(int i, int j) => FurnitureUtils.DresserMouseOver<BogwoodDresser>();
        public override void MouseOverFar(int i, int j) => FurnitureUtils.DresserMouseFar<BogwoodDresser>();
        public override void KillMultiTile(int i, int j, int frameX, int frameY) => Chest.DestroyChest(i, j);
        public override bool RightClick(int i, int j) => FurnitureUtils.DresserRightClick();
    }
}
