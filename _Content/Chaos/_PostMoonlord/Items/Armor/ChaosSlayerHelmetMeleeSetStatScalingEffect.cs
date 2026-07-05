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
    public class ChaosSlayerHelmetMeleeSetStatScalingEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (player.statLife <= player.statLifeMax2 * .2f)
            {
                player.endurance += .06f;
                player.GetDamage(DamageClass.Melee) += .4f;
            }
            else if (player.statLife <= player.statLifeMax2 * .4f)
            {
                player.endurance += .04f;
                player.GetDamage(DamageClass.Melee) += .3f;
            }
            else if (player.statLife <= player.statLifeMax2 * .6f)
            {
                player.endurance += .02f;
                player.GetDamage(DamageClass.Melee) += .2f;
            }
            if (player.statLife <= player.statLifeMax2 * .8f)
            {
                player.endurance += .01f;
                player.GetDamage(DamageClass.Melee) += .1f;
            }
        }
    }
}