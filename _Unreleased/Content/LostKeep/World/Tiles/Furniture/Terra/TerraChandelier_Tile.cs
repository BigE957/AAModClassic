using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;

public class TerraChandelier_Tile : ChandelierTile
{
    public override int ItemType => ModContent.ItemType<TerraChandelier>();

    public override Color LightColor => Color.White * 0.9f;

    public override int HitDust => DustID.Terra;
}
