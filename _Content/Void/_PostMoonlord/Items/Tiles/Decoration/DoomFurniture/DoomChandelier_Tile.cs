using AAModClassic.Dusts;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items.Tiles.Decoration.DoomFurniture
{
    public class DoomChandelier_Tile : ChandelierTile
    {
        public override int ItemType => ModContent.ItemType<DoomChandelier>();

        public override Color LightColor => new Color(1.5f, 0.3f, 0.3f);

        public override int HitDust => ModContent.DustType<DoomDust>();
    }
}
