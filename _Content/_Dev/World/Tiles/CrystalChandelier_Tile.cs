using AAModClassic.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.World.Tiles
{
    public class CrystalChandelier_Tile : ModTile
	{
        public override void SetStaticDefaults()
        {
            this.SetUpChandelier(ItemID.CrystalChandelier);
            DustType = DustID.PurpleCrystalShard;
        }

        public override void HitWire(int i, int j) => FurnitureCommon.LightHitWire(Type, i, j, 3, 3);

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            if (tile.TileFrameX < 36)
            {
                r = 0.9f;
                g = 0.9f;
                b = 0.9f;
            }
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch) => DrawingUtils.DrawSwayingMultiTile(i, j);
    }
}
