using AAModClassic.Dusts;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Decoration.OuroborosWoodFurniture
{
    public class OuroborosWoodChandelier_Tile : ChandelierTile
    {
        public override int ItemType => ModContent.ItemType<OuroborosWoodChandelier>();

        public override Color LightColor => Color.Red * 0.9f;

        public override int HitDust => ModContent.DustType<DoomDust>();
    }
}
