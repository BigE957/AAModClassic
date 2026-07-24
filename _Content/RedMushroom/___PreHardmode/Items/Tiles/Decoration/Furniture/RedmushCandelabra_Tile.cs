using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushCandelabra_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetUpCandelabra(ModContent.ItemType<RedmushCandelabra>());
            DustType = ModContent.DustType<MushDust>();
        }

        public override void HitWire(int i, int j) => FurnitureUtils.LightHitWire(Type, i, j, 2, 2);

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
			Tile tile = Main.tile[i, j];
			if (tile.TileFrameX == 0) {
				r = 1f;
				g = 0.6f;
				b = 0.6f;
			}
		}
    }
}