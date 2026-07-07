using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.Accessories
{
    public class RajahRabbitsSashOfVengeanceEffect : EquipmentEffectData
    {
        public const int HEALTHINTERVAL = 10;
        public const float DAMAGEBOOSTPERINTERVAL = 0.08f;

        public override void DoEffect(Player player)
        {
            DamageClass highestClass = PlayerUtils.GetHighestDamageClass(player);
            player.GetDamage(highestClass) += PlayerUtils.GetHealthIntervalAsPercent(player, HEALTHINTERVAL, DAMAGEBOOSTPERINTERVAL);
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(HEALTHINTERVAL, DAMAGEBOOSTPERINTERVAL * 100, ColorUtils.GetDamageClassColor(PlayerUtils.GetHighestDamageClass(Main.LocalPlayer)).Hex3(), Math.Round(PlayerUtils.GetHealthIntervalAsPercent(Main.LocalPlayer, HEALTHINTERVAL, DAMAGEBOOSTPERINTERVAL) * 100, 0), Language.GetOrRegister($"Mods.AAModClassic.EquipStats.ClassGlobalStats.{PlayerUtils.GetHighestDamageClass(Main.LocalPlayer).Name}"));
    }
}