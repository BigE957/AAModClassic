using AAModClassic.Utilities;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;

public class TerraClock_Tile : ModTile
{
	public override void SetStaticDefaults()
	{
        this.SetUpClock(ModContent.ItemType<TerraClock>());
        DustType = DustID.Terra;
	}

    public override void MouseOver(int i, int j) => FurnitureCommon.MouseOver(i, j, ModContent.ItemType<TerraClock>());

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

    public override bool RightClick(int x, int y) => FurnitureCommon.ClockRightClick();

    public override void NearbyEffects(int i, int j, bool closer)
    {
        if (closer)
            Main.SceneMetrics.HasClock = true;
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
}
