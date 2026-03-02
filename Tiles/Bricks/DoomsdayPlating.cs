using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles.Bricks
{
    public class DoomsdayPlating : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            //true for block to emit light
            HitSound = 21;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("DoomsdayPlating").Type;   
            DustType = Mod.Find<ModDust>("DoomDust").Type;
            AddMapEntry(new Color(70, 50, 50
                ));
			MinPick = 225;
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            Color color = BaseUtility.ColorMult(AAPlayer.ZeroColor, 0.7f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (tile != null && tile.HasTile && tile.TileType == Type)
            {
                Texture2D glowTex = Mod.GetTexture("Glowmasks/ApocalyptiteTile_Glow");
                BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetZeroColorDim);
            }
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}