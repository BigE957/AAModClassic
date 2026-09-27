using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using System;
using Terraria.Localization;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Accessories
{
    public class TaiyangBaoleiEnduranceEffect : EquipmentEffectData
    {
        public const float ENDURANCEBOOST = 0.06f;

        public override void DoEffect(Player player)
        {
            if (Main.dayTime)
                player.endurance += ENDURANCEBOOST;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(Math.Round(ENDURANCEBOOST * 100, 0));
    }
}