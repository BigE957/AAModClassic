using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Armor
{
    public class ChaosSlayerHelmetSummonerSetStatScalingEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (player.statLife <= player.statLifeMax2 * .2f)
            {
                player.GetDamage(DamageClass.Summon) += .60f;
            }
            else if (player.statLife <= player.statLifeMax2 * .4f)
            {
                player.GetDamage(DamageClass.Summon) += .45f;
            }
            else if (player.statLife <= player.statLifeMax2 * .6f)
            {
                player.GetDamage(DamageClass.Summon) += .3f;
            }
            else if (player.statLife <= player.statLifeMax2 * .8f)
            {
                player.GetDamage(DamageClass.Summon) += .15f;
            }
        }
    }
}