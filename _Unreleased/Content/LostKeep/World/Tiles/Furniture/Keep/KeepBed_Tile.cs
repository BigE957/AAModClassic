using AAModClassic.Utilities;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;

public class KeepBed_Tile : ModTile
{
    public override void SetStaticDefaults()
    {
        this.SetUpBed(ModContent.ItemType<KeepBed>());
        DustType = DustID.Stone;
        VanillaFallbackOnModDeletion = TileID.Beds;
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

    public override bool RightClick(int i, int j) => FurnitureUtils.BedRightClick(i, j);

    public override void MouseOver(int i, int j) => FurnitureUtils.MouseOver(i, j, ModContent.ItemType<KeepBed>());
}
