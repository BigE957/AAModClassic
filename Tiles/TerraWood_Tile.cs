using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class TerraWood_Tile : ModTile
    {

        public bool glow = true; 
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileMerge[Type][ModContent.TileType<TerraLeaves>()] = true;
            Main.tileMerge[Type][ModContent.TileType<TerraCrystal>()] = true;
            HitSound = SoundID.Tink;
            Main.tileLighted[Type] = true;
            DustType = DustID.Terra;
            AddMapEntry(new Color(52, 200, 0));
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.TileFrameY == 36 ? 18 : 16;
            BaseDrawing.DrawTileTexture(spriteBatch, Mod.GetTexture("Tiles/TerraWood"), i, j, true, false, false, null, AAGlobalTile.GetTerraColorDim);
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            if (!glow) return;
            Color color = BaseUtility.ColorMult(Color.LimeGreen, 1.4f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }
    }
}