using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Enums;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using AAModClassic.Dusts;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushChandelier_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileWaterDeath[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinateHeights = [16, 16, 16];
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.StyleLineSkip = 2;
            TileObjectData.newTile.StyleWrapLimit = 37;
            TileObjectData.newTile.DrawYOffset = -2;
            TileObjectData.newTile.Direction = TileObjectDirection.None;
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 1, 1);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.Origin = new Point16(1, 0);

			TileObjectData.newTile.WaterDeath = true;
			TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.LavaPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.addTile(Type);

            HitSound = SoundID.Dig;
            DustType = ModContent.DustType<MushDust>();

			AdjTiles = [TileID.Chandeliers];
            VanillaFallbackOnModDeletion = TileID.Chandeliers;
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(200, 150, 20), name);

			RegisterItemDrop(ModContent.ItemType<RedmushChandelier>(), 0);
        }

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

        public override void HitWire(int i, int j)
        {
            int left = i - Main.tile[i, j].TileFrameX / 18 % 3;
            int top = j - Main.tile[i, j].TileFrameY / 18 % 3;

            for (int x = left; x < left + 3; x++) {
                for (int y = top; y < top + 3; y++) {
                    if (Main.tile[x, y].TileFrameX >= 54) {
                        Main.tile[x, y].TileFrameX -= 54;
                    }
                    else {
                        Main.tile[x, y].TileFrameX += 54;
                    }
                }
            }

            //Note from EIGHT:
            // There was a small bug where wires doesn't alternate the chandeliers ON/OFF state if placed on specific parts of the tile like the top-left, middle-left or bottom-left segment
            // I slightly changed the code on "if (Wiring.running)" to fix it, so in any case you can also use this fix for the other chandeliers
            if (Wiring.running) {
                Wiring.SkipWire(left, top);
                Wiring.SkipWire(left, top + 1);
                Wiring.SkipWire(left, top + 2);
                Wiring.SkipWire(left + 1, top);
                Wiring.SkipWire(left + 2, top);
                Wiring.SkipWire(left + 1, top + 1);
                Wiring.SkipWire(left + 2, top + 2);
            }

            NetMessage.SendTileSquare(-1, left, top + 3, 3);
        }

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
			Tile tile = Main.tile[i, j];
			if (tile.TileFrameX == 0) {
				r = 1.1f;
				g = 0.8f;
				b = 0.8f;
			}
		}
    }
}