using AAModClassic.Utilities;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;

public class KeepDresser_Tile : ModTile
{
    public override void SetStaticDefaults()
    {
        this.SetUpDresser(ModContent.ItemType<KeepDresser>());
        DustType = DustID.Terra;
    }

    public override LocalizedText DefaultContainerName(int i, int j) => ModContent.GetModItem(ModContent.ItemType<KeepDresser>()).DisplayName;
    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;
    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    public override void MouseOver(int i, int j) => FurnitureUtils.DresserMouseOver<KeepDresser>();
    public override void MouseOverFar(int i, int j) => FurnitureUtils.DresserMouseFar<KeepDresser>();
    public override void KillMultiTile(int i, int j, int frameX, int frameY) => Chest.DestroyChest(i, j);
    public override bool RightClick(int i, int j) => FurnitureUtils.DresserRightClick();
}
