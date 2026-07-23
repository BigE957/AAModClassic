using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration.RazewoodFurniture
{
    public class RazewoodDoorClosed_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetUpDoorClosed(ModContent.ItemType<RazewoodDoor>(), true);
            TileID.Sets.OpenDoorID[Type] = ModContent.TileType<RazewoodDoorOpen_Tile>();
            DustType = ModContent.DustType<RazewoodDust>();
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<RazewoodDoor>();
        }

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }

    public class RazewoodDoorOpen_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetUpDoorOpen(ModContent.ItemType<RazewoodDoor>(), true);
            TileID.Sets.CloseDoorID[Type] = ModContent.TileType<RazewoodDoorClosed_Tile>();
            DustType = ModContent.DustType<RazewoodDust>();
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<RazewoodDoor>();
        }

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }
}