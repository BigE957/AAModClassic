using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Utilities
{
    public static class PlayerUtils
    {
        public static void HideAccessories(this Player player, bool hideHeadAccs = true, bool hideBodyAccs = true, bool hideLegAccs = true, bool hideShield = true)
        {
            if (hideHeadAccs)
                player.face = -1;

            if (hideBodyAccs)
            {
                player.handon = -1;
                player.handoff = -1;

                player.back = -1;
                player.front = -1;
                player.neck = -1;
            }

            if (hideLegAccs)
            {
                player.shoe = -1;
                player.waist = -1;
            }

            if (hideShield)
                player.shield = -1;
        }

        public static DamageClass GetHighestDamageClass(this Player player, bool standardizeDamage = true)
        {
            DamageClass[] damageClasses = new DamageClass[DamageClassLoader.DamageClassCount];
            for (int i = 0; i < DamageClassLoader.DamageClassCount; i++)
                damageClasses[i] = DamageClassLoader.GetDamageClass(i);

            DamageClass highestClass = null;
            foreach (DamageClass damageClass in damageClasses)
            {
                float totalDamage = player.GetTotalDamage(damageClass).ApplyTo(1);

                if (highestClass == null || (totalDamage > player.GetTotalDamage(highestClass).ApplyTo(1)) && highestClass != DamageClass.Default && highestClass != DamageClass.Generic)
                    highestClass = damageClass;
            }

            if (standardizeDamage)
                return StandardizeDamageClasses(highestClass);
            else
                return highestClass;
        }

        /// <summary>
        /// basically just "flattens" damage classes to their simplest form. ex summoner has a few copies for weird stuff like melee speed
        /// </summary>
        /// <returns></returns>
        public static DamageClass StandardizeDamageClasses(DamageClass damage)
        {
            if (damage == DamageClass.MagicSummonHybrid || damage == DamageClass.SummonMeleeSpeed)
                return DamageClass.Magic;
            else if (damage == DamageClass.MeleeNoSpeed)
                return DamageClass.Melee;
            else
                return damage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="healthInterval">one out of x. what ur putting in is x</param>
        /// <param name="damageBoost">% per interval</param>
        /// <returns></returns>
        public static float GetHealthIntervalAsPercent(Player player, float healthInterval, float damageBoost)
        {
            float lifePercent = (float)player.statLife / (float)player.statLifeMax2;
            float statInterval = (float)Math.Ceiling(lifePercent * healthInterval) / healthInterval;
            statInterval = 1 - statInterval;
            return (statInterval * 10) * damageBoost;
        }
    }
}
