using AAModClassic.Utilities;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration.RazewoodFurniture
{
    public class RazewoodDresser_Tile : ModTile
	{
		public override void SetStaticDefaults()
		{
            this.SetUpDresser(ModContent.ItemType<RazewoodDresser>());
            DustType = ModContent.DustType<Dusts.RazewoodDust>();
		}

        public override LocalizedText DefaultContainerName(int i, int j) => ModContent.GetModItem(ModContent.ItemType<RazewoodDresser>()).DisplayName;
        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;
        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
        public override void MouseOver(int i, int j) => FurnitureUtils.DresserMouseOver<RazewoodDresser>();
        public override void MouseOverFar(int i, int j) => FurnitureUtils.DresserMouseFar<RazewoodDresser>();
        public override void KillMultiTile(int i, int j, int frameX, int frameY) => Chest.DestroyChest(i, j);
        public override bool RightClick(int i, int j) => FurnitureUtils.DresserRightClick();
    }
}
