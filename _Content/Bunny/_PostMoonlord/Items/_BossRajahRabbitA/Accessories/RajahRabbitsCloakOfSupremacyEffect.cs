using AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.Accessories;
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

namespace AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Accessories
{
    public class RajahRabbitsCloakOfSupremacyEffect : EquipmentEffectData
    {
        public const int HEALTHINTERVAL = 10;
        public const float DAMAGEBOOSTPERINTERVAL = 0.12f;
        public const float SPEEDBOOSTPERINTERVAL = 0.05f;

        public override void DoEffect(Player player)
        {
            DamageClass highestClass = PlayerUtils.GetHighestDamageClass(player);
            player.GetDamage(highestClass) += PlayerUtils.GetHealthIntervalAsPercent(player, HEALTHINTERVAL, DAMAGEBOOSTPERINTERVAL);
            player.moveSpeed += PlayerUtils.GetHealthIntervalAsPercent(player, HEALTHINTERVAL, SPEEDBOOSTPERINTERVAL);
            player.GetModPlayer<ZAAPlayer>().MaxMovespeedboost += PlayerUtils.GetHealthIntervalAsPercent(player, HEALTHINTERVAL, SPEEDBOOSTPERINTERVAL);
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(HEALTHINTERVAL, DAMAGEBOOSTPERINTERVAL * 100, SPEEDBOOSTPERINTERVAL * 100, ColorUtils.GetDamageClassColor(PlayerUtils.GetHighestDamageClass(Main.LocalPlayer)).Hex3(), Math.Round(PlayerUtils.GetHealthIntervalAsPercent(Main.LocalPlayer, HEALTHINTERVAL, DAMAGEBOOSTPERINTERVAL) * 100, 0), Language.GetOrRegister($"Mods.AAModClassic.EquipStats.ClassGlobalStats.{PlayerUtils.GetHighestDamageClass(Main.LocalPlayer).Name}"), PlayerUtils.GetHealthIntervalAsPercent(Main.LocalPlayer, HEALTHINTERVAL, SPEEDBOOSTPERINTERVAL) * 100);
    }
}