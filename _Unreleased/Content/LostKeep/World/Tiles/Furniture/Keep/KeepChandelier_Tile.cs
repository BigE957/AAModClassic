using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;

public class KeepChandelier_Tile : ChandelierTile
{
    public override int ItemType => ModContent.ItemType<KeepChandelier>();

    public override Color LightColor => Color.White * 0.6f;

    public override int HitDust => DustID.Stone;
}
