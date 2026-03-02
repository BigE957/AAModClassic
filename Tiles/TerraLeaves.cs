using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class TerraLeaves : ModTile
    {

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileMerge[Type][Mod.Find<ModTile>("TerraWood").Type] = true;
            HitSound = 21;
            Main.tileLighted[Type] = true;
            DustType = DustID.Terra;
            AddMapEntry(new Color(100, 100, 100));
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        
        Texture2D glowTex = null;

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            bool glow = true;
            if (glow && tile != null && tile.HasTile && tile.TileType == Type)
            {
                if (glowTex == null) glowTex = Mod.GetTexture("Tiles/TerraLeaves");
                BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetTerra2ColorDim);
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = Color.YellowGreen.R / 255;
            g = Color.YellowGreen.G / 255;
            b = Color.YellowGreen.B / 255;
        }
    }
}