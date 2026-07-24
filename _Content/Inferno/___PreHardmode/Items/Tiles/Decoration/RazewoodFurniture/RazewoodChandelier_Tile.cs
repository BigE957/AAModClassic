using AAModClassic.Dusts;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration.RazewoodFurniture
{
    public class RazewoodChandelier_Tile : ChandelierTile
    {
        public override int ItemType => ModContent.ItemType<RazewoodChandelier>();

        public override Color LightColor => Color.Orange * 0.9f;

        public override int HitDust => ModContent.DustType<RazewoodDust>();
    }
}
