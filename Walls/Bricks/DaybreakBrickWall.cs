using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls.Bricks
{
    public class DaybreakBrickWall : ModWall
	{
        public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            Main.wallHouse[Type] = true;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("DaybreakWall").Type;
            AddMapEntry(new Color(40, 12, 10));
            DustType = Mod.Find<ModDust>("DaybreakIncineriteDust").Type;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Texture2D glowTex = Mod.GetTexture("Glowmasks/DaybreakBrickWall_Glow");
            BaseDrawing.DrawWallTexture(sb, glowTex, x, y, false, AAGlobalTile.GetAkumaColorDim);
        }
    }
}