using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Terraria.ObjectData;
using AAModClassic._Content._Dev.__Hardmode.Items.Accessories;

namespace AAModClassic._Content._Dev.World.Tiles
{
    public class InvokerBook_Tile : ModTile
	{
		public override void SetStaticDefaults()
		{
            Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
			TileObjectData.addTile(Type);
			RegisterItemDrop(ModContent.ItemType<InvokerBook>());
			LocalizedText modTranslation = CreateMapEntryName();
			// modTranslation.SetDefault("Aleister Book");
			AddMapEntry(Color.Gold, modTranslation);
			AnimationFrameHeight = 16;
		}

        public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.noThrow = 2;
			localPlayer.cursorItemIconEnabled = true;
			localPlayer.cursorItemIconID = ModContent.ItemType<InvokerBook>();
		}

        public override bool RightClick(int i, int j)
		{
            Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, ModContent.ItemType<InvokerBook>(), 1, false, 0, false, false);
            WorldGen.KillTile(i, j, false, false, true);
            return true;
		}
    }
}