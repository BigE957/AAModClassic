using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Terraria.ObjectData;

namespace AAModClassic.Items.Dev.DevTile.Tiles
{
    public class InvokerBookTile : ModTile
	{
		public override void SetStaticDefaults()
		{
            Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
			TileObjectData.addTile(Type);
			ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("InvokerBook").Type;
			LocalizedText modTranslation = CreateMapEntryName(null);
			// modTranslation.SetDefault("Aleister Book");
			AddMapEntry(Color.Gold, modTranslation);
			AnimationFrameHeight = 16;
		}

        public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.noThrow = 2;
			localPlayer.cursorItemIconEnabled = true;
			localPlayer.cursorItemIconID = Mod.Find<ModItem>("InvokerBook").Type;
		}

        public override bool RightClick(int i, int j)
		{
            Item.NewItem(i * 16, j * 16, 16, 16, Mod.Find<ModItem>("InvokerBook").Type, 1, false, 0, false, false);
            WorldGen.KillTile(i, j, false, false, true);
            return true;
		}
    }
}