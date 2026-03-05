using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class AbyssLeaves : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileSolidTop[Type] = false;
            Main.tileMerge[Type][Mod.Find<ModTile>("AbyssWood").Type] = true;
            HitSound = SoundID.Grass;//21;
            Main.tileLighted[Type] = true;
            DustType = DustID.Blood;
            AddMapEntry(new Color(200, 0, 70));
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void PostDraw(int x, int y, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[x, y];
            if (glow && tile != null && tile.HasTile && tile.TileType == Type)
            {
                if (glowTex == null) glowTex = Mod.GetTexture("Tiles/AbyssLeaves");
                BaseDrawing.DrawTileTexture(spriteBatch, glowTex, x, y, true, false, false, null, AAGlobalTile.GetYamataColorDim);
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r  = .05f;
            g = 0f;
            b = 0f;
        }
    }
}