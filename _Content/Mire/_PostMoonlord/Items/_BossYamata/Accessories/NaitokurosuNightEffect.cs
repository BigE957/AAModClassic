using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using System;
using Terraria.Localization;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Accessories
{
    public class NaitokurosuNightEffect(float increase) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (!Main.dayTime)
                player.moveSpeed += increase;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(Math.Round(increase * 100, 0));
    }
}