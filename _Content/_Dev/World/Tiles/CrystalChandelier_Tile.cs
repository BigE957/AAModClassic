using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace AAModClassic._Content._Dev.World.Tiles
{
    public class CrystalChandelier_Tile : ChandelierTile
    {
        public override int ItemType => ItemID.CrystalChandelier;

        public override Color LightColor => Color.White * 0.9f;

        public override int HitDust => DustID.PurpleCrystalShard;
    }
}
