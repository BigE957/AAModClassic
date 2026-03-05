using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Crafters
{
    public class EvilAltar : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(Type);
			DustType = DustID.WoodFurniture;
			TileID.Sets.DisableSmartCursor[Type] = true;
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Evil Altar");
            DustType = ModContent.DustType<InfinityOverloadP>();
            AddMapEntry(new Color(120, 0, 160), name);
            AdjTiles = new int[] { TileID.DemonAltar };
        }

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			int item = 0;
			switch (frameX / 54)
			{
				case 0:
					item = Mod.Find<ModItem>("CorruptAltar").Type;
					break;
				case 1:
					item = Mod.Find<ModItem>("CrimsonAltar").Type;
					break;
            }
			if (item > 0)
			{
				Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 48, 48, item);
			}
		}
	}
}