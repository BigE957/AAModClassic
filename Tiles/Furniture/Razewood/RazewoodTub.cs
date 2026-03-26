using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Furniture.Razewood
{
    public class RazewoodTub : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
			TileObjectData.newTile.CoordinateHeights = new int[]{ 16, 18 };
			TileObjectData.addTile(Type);
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Razewood Bathtub");
            AddMapEntry(new Color(205, 62, 12), name);
            DustType = Mod.Find<ModDust>("RazewoodDust").Type;
		}

		
		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}
	}
}