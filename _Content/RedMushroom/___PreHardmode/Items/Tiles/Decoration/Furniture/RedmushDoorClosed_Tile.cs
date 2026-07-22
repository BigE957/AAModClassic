using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushDoorClosed_Tile : ModTile 
	{
        public override void SetStaticDefaults() 
		{
            Main.tileFrameImportant[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileNoAttach[Type] = true;
			Main.tileBlockLight[Type] = true;
            Main.tileLavaDeath[Type] = true;

			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			TileID.Sets.NotReallySolid[Type] = true;
			TileID.Sets.DrawsWalls[Type] = true;
			TileID.Sets.OpenDoorID[Type] = ModContent.TileType<RedmushDoorOpen_Tile>();

			TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.ClosedDoor, 0));
			TileObjectData.addTile(Type);

            HitSound = SoundID.Dig;
			DustType = ModContent.DustType<MushDust>();

			AdjTiles = [TileID.ClosedDoor];
			VanillaFallbackOnModDeletion = TileID.ClosedDoor;
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(160, 130, 20), name);

			RegisterItemDrop(ModContent.ItemType<RedmushDoor>(), 0);
        }

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

		public override void MouseOver(int i, int j) 
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = ModContent.ItemType<RedmushDoor>();
		}
    }
}