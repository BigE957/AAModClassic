using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushCandle_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileWaterDeath[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinateHeights = [16];
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.Height = 1;
			TileObjectData.newTile.StyleLineSkip = 2;
            TileObjectData.newTile.StyleWrapLimit = 2;
            TileObjectData.newTile.DrawYOffset = 2;

			TileObjectData.newTile.WaterDeath = true;
			TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.LavaPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.addTile(Type);

            HitSound = SoundID.Dig;
            DustType = ModContent.DustType<MushDust>();

			AdjTiles = [TileID.Candles];
            VanillaFallbackOnModDeletion = TileID.Candles;
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(200, 150, 20), name);

			RegisterItemDrop(ModContent.ItemType<RedmushCandle>(), 0);
        }

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

        public override void HitWire(int i, int j)
        {
            if (Main.tile[i, j].TileFrameX >= 18) {
                Main.tile[i, j].TileFrameX -= 18;
            }
            else {
                Main.tile[i, j].TileFrameX += 18;
            }
        }

        public override bool RightClick(int i, int j)
        {
             if (Main.tile[i, j].TileFrameX >= 18) {
                Main.tile[i, j].TileFrameX -= 18;
            }
            else {
                Main.tile[i, j].TileFrameX += 18;
            }
            return true;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;

            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<RedmushCandle>();

            player.noThrow = 2;
        }

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
			Tile tile = Main.tile[i, j];
			if (tile.TileFrameX == 0) {
				r = 0.8f;
				g = 0.5f;
				b = 0.5f;
			}
		}
    }
}