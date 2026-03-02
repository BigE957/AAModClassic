using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles.Trophy
{
    public class WKGTrophy : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 36;
            TileObjectData.addTile(Type);
            DustType = DustID.WoodFurniture;
			disableSmartCursor/* tModPorter Note: Removed. Use TileID.Sets.DisableSmartCursor instead */ = true;
			AddMapEntry(new Color(120, 85, 60));
		}

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
            Item.NewItem(i * 16, j * 16, 48, 48, Mod.Find<ModItem>("WKGTrophy").Type);
        }
	}
}