using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items.Tiles.Decoration.DoomFurniture
{
    public class DoomDresser_Tile : ModTile
	{
		public override void SetStaticDefaults()
		{
            this.SetUpDresser(ModContent.ItemType<DoomDresser>());
            DustType = ModContent.DustType<Dusts.DoomDust>();
		}

        public override LocalizedText DefaultContainerName(int i, int j) => ModContent.GetModItem(ModContent.ItemType<DoomDresser>()).DisplayName;
        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;
        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
        public override void MouseOver(int i, int j) => FurnitureUtils.DresserMouseOver<DoomDresser>();
        public override void MouseOverFar(int i, int j) => FurnitureUtils.DresserMouseFar<DoomDresser>();
        public override void KillMultiTile(int i, int j, int frameX, int frameY) => Chest.DestroyChest(i, j);
        public override bool RightClick(int i, int j) => FurnitureUtils.DresserRightClick();

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Main.tile[i, j];
			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			int height = tile.TileFrameY == 36 ? 18 : 16;
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Glow").Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}
	}
}
