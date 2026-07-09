using AAModClassic._Content._Tinker.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Accessories;
using AAModClassic.Dusts;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Accessories
{
    public class ArcticMedallionEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (player.ZoneRain && player.ZoneSnow)
            {
                player.GetDamage(DamageClass.Generic) += 1f;
                player.GetCritChance(DamageClass.Generic) += 100;
            }
        }
    }
}