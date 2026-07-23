using AAModClassic.Utilities;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;

public class KeepClock_Tile : ModTile
{
    public override void SetStaticDefaults()
    {
        this.SetUpClock(ModContent.ItemType<KeepClock>());
        DustType = DustID.Stone;
    }

    public override void MouseOver(int i, int j) => FurnitureCommon.MouseOver(i, j, ModContent.ItemType<KeepClock>());

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

    public override bool RightClick(int x, int y) => FurnitureCommon.ClockRightClick();

    public override void NearbyEffects(int i, int j, bool closer)
    {
        if (closer)
            Main.SceneMetrics.HasClock = true;
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
}
