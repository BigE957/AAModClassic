using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Banners
{
    public class ThixxieBanner : ModTile
	{
		public override void SetStaticDefaults() 
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 6;
			TileObjectData.newTile.Width = 4;
            TileObjectData.newTile.Origin = new Point16(2, 0);
            TileObjectData.newTile.AnchorBottom = default;
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16, 16, 16 };
			TileObjectData.newTile.CoordinatePadding = 0;
			TileObjectData.addTile(Type);
			DustType = -1;
			disableSmartCursor/* tModPorter Note: Removed. Use TileID.Sets.DisableSmartCursor instead */ = true;
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Banner");
			AddMapEntry(new Color(13, 88, 130), name);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY) 
		{
			Item.NewItem(i * 16, j * 16, 16, 48, Mod.Find<ModItem>("ThixxieBanner").Type);
		}

		public override void NearbyEffects(int i, int j, bool closer) 
		{
			if (closer) 
			{
				Player player = Main.LocalPlayer;
				player.NPCBannerBuff[Mod.Find<ModNPC>("FatPixie").Type] = true;
				player.hasBanner = true;
			}
		}
	}
}
