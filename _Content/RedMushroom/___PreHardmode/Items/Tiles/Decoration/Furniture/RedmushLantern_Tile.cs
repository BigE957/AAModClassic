using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushLantern_Tile : ModTile 
    {
        public override void SetStaticDefaults() 
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileWaterDeath[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinateHeights = [16, 16];
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.StyleLineSkip = 2;
            TileObjectData.newTile.DrawYOffset = -2;

			TileObjectData.newTile.WaterDeath = true;
			TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.LavaPlacement = LiquidPlacement.NotAllowed;
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom | AnchorType.Platform, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(Type);

            HitSound = SoundID.Dig;
            DustType = ModContent.DustType<MushDust>();

			AdjTiles = [TileID.HangingLanterns];
			VanillaFallbackOnModDeletion = TileID.HangingLanterns;
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(200, 150, 20), name);

			RegisterItemDrop(ModContent.ItemType<RedmushLantern>(), 0);
        }

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

        public override void HitWire(int i, int j) 
        {
            int left = i - Main.tile[i, j].TileFrameX / 18 % 1;
            int top = j - Main.tile[i, j].TileFrameY / 18 % 2;

            for (int x = left; x < left + 1; x++) 
            {
                for (int y = top; y < top + 2; y++) 
                {
                    if (Main.tile[x, y].TileFrameX >= 18)
                        Main.tile[x, y].TileFrameX -= 18;
                    else
                        Main.tile[x, y].TileFrameX += 18;
                }
            }

            if (Wiring.running) 
            {
                Wiring.SkipWire(left, top);
                Wiring.SkipWire(left, top + 1);
            }

            NetMessage.SendTileSquare(-1, left, top + 1, 2);
        }

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) 
        {
			Tile tile = Main.tile[i, j];
			if (tile.TileFrameX == 0) 
            {
				r = 0.9f;
				g = 0.5f;
				b = 0.5f;
			}
		}
    }
}