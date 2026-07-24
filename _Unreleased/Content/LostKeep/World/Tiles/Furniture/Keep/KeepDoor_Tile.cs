using AAModClassic.Utilities;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;

public class KeepDoorClosed_Tile : ModTile
{
	public override void SetStaticDefaults()
	{
        this.SetUpDoorClosed(ModContent.ItemType<KeepDoor>());
        DustType = DustID.Stone;
		TileID.Sets.OpenDoorID[Type] = ModContent.TileType<KeepDoorOpen_Tile>();
    }

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

    public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = ModContent.ItemType<KeepDoor>();
	}
}

public class KeepDoorOpen_Tile : ModTile
{
    public override void SetStaticDefaults()
    {
        this.SetUpDoorOpen(ModContent.ItemType<KeepDoor>(), true);
        DustType = DustID.Stone;
        TileID.Sets.CloseDoorID[Type] = ModContent.TileType<KeepDoorClosed_Tile>();
    }

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

    public override void MouseOver(int i, int j)
    {
        Player localPlayer = Main.LocalPlayer;
        localPlayer.noThrow = 2;
        localPlayer.cursorItemIconEnabled = true;
        localPlayer.cursorItemIconID = ModContent.ItemType<KeepDoor>();
    }
}

