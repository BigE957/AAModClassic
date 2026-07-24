using AAModClassic.Dusts;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushChandelier_Tile : ChandelierTile
    {
        public override int ItemType => ModContent.ItemType<RedmushChandelier>();

        public override Color LightColor => new(1.1f, 0.5f, 0.5f);

        public override int HitDust => ModContent.DustType<MushDust>();
    }
}
