using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushDoorClosed_Tile : ModTile 
	{
        public override void SetStaticDefaults()
        {
            this.SetUpDoorClosed(ModContent.ItemType<RedmushDoor>(), true);
            TileID.Sets.OpenDoorID[Type] = ModContent.TileType<RedmushDoorOpen_Tile>();
            DustType = ModContent.DustType<MushDust>();
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<RedmushDoor>();
        }

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }

    public class RedmushDoorOpen_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetUpDoorOpen(ModContent.ItemType<RedmushDoor>(), true);
            TileID.Sets.CloseDoorID[Type] = ModContent.TileType<RedmushDoorClosed_Tile>();
            DustType = ModContent.DustType<MushDust>();
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<RedmushDoor>();
        }

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }
}