using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.BossStandard
{
    public class ShenDoragonATrophy_Tile : ModTile
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
			TileID.Sets.DisableSmartCursor[Type] = true;
			AddMapEntry(new Color(120, 85, 60));
		}
	}
}