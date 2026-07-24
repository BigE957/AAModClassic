using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushCandle_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetUpCandle(ModContent.ItemType<RedmushCandle>(), true);
            base.DustType = ModContent.DustType<MushDust>();
        }

        public override bool RightClick(int i, int j)
        {
            FurnitureUtils.RightClickBreak(i, j);
            return true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

        public override void HitWire(int i, int j) => FurnitureUtils.LightHitWire(Type, i, j, 1, 1);

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<RedmushCandle>();
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
			Tile tile = Main.tile[i, j];
			if (tile.TileFrameX == 0) {
				r = 0.8f;
				g = 0.5f;
				b = 0.5f;
			}
		}
    }
}