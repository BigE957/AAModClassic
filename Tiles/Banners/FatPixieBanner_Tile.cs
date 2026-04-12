using AAModClassic.___Content.Hallow._Hardmode.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Banners
{
    public class FatPixieBanner_Tile : ModTile
	{
		public override void SetStaticDefaults() 
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Origin = new Point16(1, 1);
            TileObjectData.newTile.AnchorBottom = default;
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.CoordinatePadding = 0;			
            TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(Type);
			DustType = -1;
			TileID.Sets.DisableSmartCursor[Type] = true;
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Banner");
			AddMapEntry(new Color(13, 88, 130), name);
		}

		public override void NearbyEffects(int i, int j, bool closer) 
		{
			if (closer)
			{
				Player player = Main.LocalPlayer;
				player.HasNPCBannerBuff(ModContent.NPCType<FatPixie>());
			}
		}
	}
}
