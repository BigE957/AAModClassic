using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unreleased.Tiles
{
    class SoCBox : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileObsidianKill[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(Type);
            TileID.Sets.DisableSmartCursor[Type] = true;
            LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Music Box");
            DustType = Mod.Find<ModDust>("CthulhuDust").Type;
            AddMapEntry(new Color(200, 200, 200), name);
		}
        
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 48, Mod.Find<ModItem>("SoCBox").Type);
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = Mod.Find<ModItem>("SoCBox").Type;
		}
	}
}
