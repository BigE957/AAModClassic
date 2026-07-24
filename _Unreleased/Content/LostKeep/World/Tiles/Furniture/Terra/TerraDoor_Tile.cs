using AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;

public class TerraDoorClosed_Tile : ModTile
{
	public override void SetStaticDefaults()
	{
        this.SetUpDoorClosed(ModContent.ItemType<KeepDoor>());
        DustType = DustID.Terra;
        TileID.Sets.OpenDoorID[Type] = ModContent.TileType<TerraDoorOpen_Tile>();
    }

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

    public override void MouseOver(int i, int j)
    {
        Player localPlayer = Main.LocalPlayer;
        localPlayer.noThrow = 2;
        localPlayer.cursorItemIconEnabled = true;
        localPlayer.cursorItemIconID = ModContent.ItemType<TerraDoor>();
    }
}

public class TerraDoorOpen_Tile : ModTile
{
    public override void SetStaticDefaults()
    {
        this.SetUpDoorOpen(ModContent.ItemType<KeepDoor>());
        DustType = DustID.Terra;
        TileID.Sets.CloseDoorID[Type] = ModContent.TileType<TerraDoorClosed_Tile>();
    }

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

    public override void MouseOver(int i, int j)
    {
        Player localPlayer = Main.LocalPlayer;
        localPlayer.noThrow = 2;
        localPlayer.cursorItemIconEnabled = true;
        localPlayer.cursorItemIconID = ModContent.ItemType<TerraDoor>();
    }
}
