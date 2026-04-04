using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Decoration
{
	public class DevStatue : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Origin = new Point16(1, 2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
            TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(Type);
			DustType = DustID.Grass;
			TileID.Sets.DisableSmartCursor[Type] = true;
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Statue");
			AddMapEntry(new Color(120, 120, 120), name);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			int item = 0;
			switch (frameX / 36)
			{
				case 0:
					item = ItemID.AlphabetStatueE;
					break;
				case 2:
					item = ModContent.ItemType<HallamStatue>();
					break;
				case 3:
					item = ModContent.ItemType<FazerStatue>();
					break;
                case 4:
                    item = ModContent.ItemType<DallinStatue>();
                    break;
                case 5:
                    item = ModContent.ItemType<AvesStatue>();
                    break;
                case 6:
                    item = ModContent.ItemType<GroxStatue>();
                    break;
                case 7:
                    item = ModContent.ItemType<MoonStatue>();
                    break;
                case 8:
                    item = ModContent.ItemType<SauceStatue>();
                    break;
                case 9:
                    item = ModContent.ItemType<KyuuStatue>();
                    break;
                case 10:
                    item = ModContent.ItemType<BegStatue>();
                    break;
                case 11:
                    item = ModContent.ItemType<FargoStatue>();
                    break;
                case 12:
                    item = ModContent.ItemType<TailsStatue>();
                    break;
                case 13:
                    item = ModContent.ItemType<CharlieStatue>();
                    break;
                case 14:
                    item = ItemID.AlphabetStatueL;
                    break;
                case 15:
                    item = ModContent.ItemType<LCSStatue>();
                    break;
                case 16:
                    item = ModContent.ItemType<EnderStatue>();
                    break;
                default:
                    item = ItemID.GargoyleStatue;
                    break;
            }
			if (item > 0)
			{
				Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 36, 36, item);
			}
		}
	}
}