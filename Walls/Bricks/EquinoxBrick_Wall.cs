using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Walls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Walls.Bricks
{
    public class EquinoxBrick_Wall : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            DustType = ModContent.DustType<Dusts.RadiumDust>();
            AddMapEntry(new Color(60, 60, 30));
            HitSound = SoundID.Tink;
            RegisterItemDrop(ModContent.ItemType<EquinoxWall>());
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
                BaseDrawing.DrawWallTexture(spriteBatch, ModContent.Request<Texture2D>("AAModClassic/Walls/Bricks/DarkmatterWall").Value, x, y, true);
            }
            return false;
        }
    }
}