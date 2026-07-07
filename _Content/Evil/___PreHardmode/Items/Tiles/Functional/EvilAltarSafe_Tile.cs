using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.Evil.___PreHardmode.Items.Tiles.Functional
{
    public class EvilAltarSafe_Tile : ModTile
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
            DustType = ModContent.DustType<Dusts.InfinityOverloadP>();
            AddMapEntry(new Color(120, 0, 160), name);
            AdjTiles = new int[] { TileID.DemonAltar };
        }
	}
}