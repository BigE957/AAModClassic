using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items.Tiles.Decoration
{
    public class DaybreakBrickWall_Wall : ModWall
	{
        public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            Main.wallHouse[Type] = true;
            RegisterItemDrop(ModContent.ItemType<DaybreakBrickWall>());
            AddMapEntry(new Color(40, 12, 10));
            DustType = ModContent.DustType<Dusts.DaybreakIncineriteDust>();
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Texture2D glowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            BaseDrawing.DrawWallTexture(sb, glowTex, x, y, false, AAGlobalTile.GetAkumaColorDim);
        }
    }
}