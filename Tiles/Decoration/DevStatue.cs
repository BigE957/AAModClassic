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
			disableSmartCursor/* tModPorter Note: Removed. Use TileID.Sets.DisableSmartCursor instead */ = true;
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
					item = Mod.Find<ModItem>("AlphakipStatue").Type;
					break;
				case 1:
					item = Mod.Find<ModItem>("LizStatue").Type;
					break;
				case 2:
					item = Mod.Find<ModItem>("HallamStatue").Type;
					break;
				case 3:
					item = Mod.Find<ModItem>("FazerStatue").Type;
					break;
                case 4:
                    item = Mod.Find<ModItem>("DallinStatue").Type;
                    break;
                case 5:
                    item = Mod.Find<ModItem>("AvesStatue").Type;
                    break;
                case 6:
                    item = Mod.Find<ModItem>("GroxStatue").Type;
                    break;
                case 7:
                    item = Mod.Find<ModItem>("MoonStatue").Type;
                    break;
                case 8:
                    item = Mod.Find<ModItem>("SauceStatue").Type;
                    break;
                case 9:
                    item = Mod.Find<ModItem>("KyuuStatue").Type;
                    break;
                case 10:
                    item = Mod.Find<ModItem>("BegStatue").Type;
                    break;
                case 11:
                    item = Mod.Find<ModItem>("FargoStatue").Type;
                    break;
                case 12:
                    item = Mod.Find<ModItem>("TailsStatue").Type;
                    break;
                case 13:
                    item = Mod.Find<ModItem>("CharlieStatue").Type;
                    break;
                case 14:
                    item = Mod.Find<ModItem>("FerretStatue").Type;
                    break;
                case 15:
                    item = Mod.Find<ModItem>("LCSStatue").Type;
                    break;
                case 16:
                    item = Mod.Find<ModItem>("EnderStatue").Type;
                    break;
            }
			if (item > 0)
			{
				Item.NewItem(i * 16, j * 16, 36, 36, item);
			}
		}
	}
}