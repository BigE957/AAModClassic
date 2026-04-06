using AAModClassic.Items.Vanity.Cerberus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;


namespace AAModClassic.Items.Dev.DevTile.Tiles
{
    public class CerberusKennel_Tile : ModTile
	{
		public override void SetStaticDefaults()
		{
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.addTile(Type);
			LocalizedText modTranslation = CreateMapEntryName();
			// modTranslation.SetDefault("Cerberus Kennel");
			AddMapEntry(Color.Gold, modTranslation);
            RegisterItemDrop(ModContent.ItemType<InvokerBag>());
		}
    }
}