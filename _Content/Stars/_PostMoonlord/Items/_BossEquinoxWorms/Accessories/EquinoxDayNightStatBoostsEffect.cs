using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items._BossEquinoxWorms.Accessories
{
    public class EquinoxDayNightStatBoostsEffect(bool doNight = false) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if ((!doNight && Main.dayTime) || (doNight && !Main.dayTime))
            {
                player.GetDamage(DamageClass.Generic) += 0.17f;

                player.GetCritChance(DamageClass.Generic) += 4;
                player.GetCritChance(DamageClass.Summon) -= 4;

                player.GetAttackSpeed(DamageClass.Melee) += 0.10f;

                player.GetKnockback(DamageClass.Summon).Base += 0.7f;

                player.lifeRegen += 5;
                player.statDefense += 8;
                player.pickSpeed -= 0.30f;
            }
        }

        public override string GetDescription()
        {
            string dayNight = doNight == true ? "Night" : "Day";
            dayNight = Language.GetTextValue($"{Description}." + dayNight);
            return Language.GetTextValue($"{Description}.Default").FormatWith(dayNight);
        }
    }
}