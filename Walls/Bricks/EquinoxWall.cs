using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Walls.Bricks
{
    public class EquinoxWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            DustType = Mod.Find<ModDust>("RadiumDust").Type;
            AddMapEntry(new Color(60, 60, 30));
            HitSound = SoundID.Tink;
            RegisterItemDrop(Mod.Find<ModItem>("EquinoxWall").Type);
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
                BaseDrawing.DrawWallTexture(spriteBatch, TextureAssets.Wall[Type].Value, x, y, true);
            }
            else
            {
                BaseDrawing.DrawWallTexture(spriteBatch, Mod.GetTexture("Walls/Bricks/DarkmatterWall"), x, y, true);
            }
            return false;
        }
    }
}