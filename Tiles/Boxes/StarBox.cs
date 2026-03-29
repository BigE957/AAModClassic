using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using AAModClassic.Base.BaseMod.Base;
using Terraria.ID;

namespace AAModClassic.Tiles.Boxes
{
    class StarBox : ModTile
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
            DustType = Mod.Find<ModDust>("RadiumDust").Type;
            AddMapEntry(new Color(200, 200, 200), name);
            RegisterItemDrop(ModContent.ItemType<AAModClassic.Items.Blocks.Boxes.StarBox>());
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (Main.dayTime)
            {
                LocalizedText name = CreateMapEntryName();
                AddMapEntry(new Color(160, 150, 0), name);
                DustType = Mod.Find<ModDust>("RadiumDust").Type;
            }
            else
            {
                LocalizedText name = CreateMapEntryName();
                AddMapEntry(new Color(0, 30, 100), name);
                DustType = ModContent.DustType<Dusts.DarkmatterDust>();
            }
            return true;
        }

        public override bool PreDraw(int x, int y, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[x, y];
            int width = 16, height = 16;
            int frameX = tile != null && tile.HasTile ? tile.TileFrameX + (Main.tileFrame[Type] * 38) : 0;
            int frameY = tile != null && tile.HasTile ? tile.TileFrameY : 0;
            Texture2D Tex = TextureAssets.Tile[Type].Value;
            if (!Main.dayTime)
            {
                Tex = Mod.GetTexture("Tiles/Boxes/StarBoxN");
            }
            BaseDrawing.DrawTileTexture(spriteBatch, Tex, x, y, width, height, frameX, frameY, false, false, false, null);
            return false;
        }

        public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = Mod.Find<ModItem>("StarBox").Type;
		}
	}
}
