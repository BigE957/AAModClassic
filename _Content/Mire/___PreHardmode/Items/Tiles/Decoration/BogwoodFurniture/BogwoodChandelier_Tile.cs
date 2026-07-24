using AAModClassic.Dusts;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration.BogwoodFurniture
{
    public class BogwoodChandelier_Tile : ChandelierTile
    {
        public override int ItemType => ModContent.ItemType<BogwoodChandelier>();

        public override Color LightColor => new Color(0.2f, 0.9f, 0.2f);

        public override int HitDust => ModContent.DustType<BogwoodDust>();
    }
}
