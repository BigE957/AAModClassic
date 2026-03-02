
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls.Bricks
{
    public class EquinoxWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            DustType = Mod.Find<ModDust>("RadiumDust").Type;
            AddMapEntry(new Color(60, 60, 30));
            HitSound = 21;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("EquinoxWall").Type;
            Main.wallHouse[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override bool PreDraw(int x, int y, SpriteBatch spriteBatch)
        {
            if (Main.dayTime)
            {
                BaseDrawing.DrawWallTexture(spriteBatch, Main.wallTexture[Type], x, y, true);
            }
            else
            {
                BaseDrawing.DrawWallTexture(spriteBatch, Mod.GetTexture("Walls/Bricks/DarkmatterWall"), x, y, true);
            }
            return false;
        }
    }
}