using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Armor
{
    public class ChaosSlayerHelmetRangedSetStatScalingEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (player.statLife <= player.statLifeMax2 * .2f)
            {
                player.GetDamage(DamageClass.Ranged) += .4f;
                player.GetCritChance(DamageClass.Ranged) += 7;
            }
            else if (player.statLife <= player.statLifeMax2 * .4f)
            {
                player.GetDamage(DamageClass.Ranged) += .3f;
                player.GetCritChance(DamageClass.Ranged) += 14;
            }
            else if (player.statLife <= player.statLifeMax2 * .6f)
            {
                player.GetDamage(DamageClass.Ranged) += .2f;
                player.GetCritChance(DamageClass.Ranged) += 21;
            }
            else if (player.statLife <= player.statLifeMax2 * .8f)
            {
                player.GetDamage(DamageClass.Ranged) += .1f;
                player.GetCritChance(DamageClass.Ranged) += 28;
            }
        }
    }
}