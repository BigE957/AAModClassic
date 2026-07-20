using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushPlatform_Tile : ModTile 
	{
        public override void SetStaticDefaults() 
		{
            Main.tileFrameImportant[Type] = true;
			Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileNoAttach[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = true;

			TileID.Sets.Platforms[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;

			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinateHeights = [16];
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleMultiplier = 27;
			TileObjectData.newTile.StyleWrapLimit = 27;
			TileObjectData.newTile.UsesCustomCanPlace = false;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addTile(Type);

            HitSound = SoundID.Dig;
			DustType = ModContent.DustType<MushDust>();

			AdjTiles = [TileID.Platforms];
			VanillaFallbackOnModDeletion = TileID.Platforms;
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);

            AddMapEntry(new Color(160, 130, 20));

			RegisterItemDrop(ModContent.ItemType<RedmushPlatform>(), 0);
        }

		public override void PostSetDefaults() => Main.tileNoSunLight[Type] = false;
    }
}