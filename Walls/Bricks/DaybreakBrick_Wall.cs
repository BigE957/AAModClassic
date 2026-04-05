using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Walls.Bricks
{
    public class DaybreakBrick_Wall : ModWall
	{
        public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            Main.wallHouse[Type] = true;
            RegisterItemDrop(ModContent.ItemType<DaybreakWall>());
            AddMapEntry(new Color(40, 12, 10));
            DustType = ModContent.DustType<DaybreakIncineriteDust>();
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Texture2D glowTex = Mod.GetTexture("Glowmasks/DaybreakBrickWall_Glow");
            BaseDrawing.DrawWallTexture(sb, glowTex, x, y, false, AAGlobalTile.GetAkumaColorDim);
        }
    }
}