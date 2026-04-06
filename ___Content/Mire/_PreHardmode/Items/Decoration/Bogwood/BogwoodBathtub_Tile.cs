using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Furniture.Bogwood
{
    public class BogwoodBathtub_Tile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
			TileObjectData.newTile.CoordinateHeights = new int[]{ 16, 18 };
			TileObjectData.addTile(Type);
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Bogwood Bathtub");
            AddMapEntry(new Color(12, 62, 205), name);
            DustType = ModContent.DustType<Dusts.BogwoodDust>();
            RegisterItemDrop(ModContent.ItemType<AAModClassic.Items.Blocks.BogwoodF.BogwoodBathtub>());
        }

		
		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}
	}
}