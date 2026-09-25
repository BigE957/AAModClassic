using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Armor
{
    public class ChaosSlayerHelmetMageSetStatScalingEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (player.statLife <= player.statLifeMax2 * .2f)
            {
                player.manaCost *= 0;
                player.GetDamage(DamageClass.Magic) += .4f;
            }
            else if (player.statLife <= player.statLifeMax2 * .4f)
            {
                player.manaCost *= .25f;
                player.GetDamage(DamageClass.Magic) += .3f;
            }
            else if (player.statLife <= player.statLifeMax2 * .6f)
            {
                player.manaCost *= .5f;
                player.GetDamage(DamageClass.Magic) += .2f;
            }
            else if (player.statLife <= player.statLifeMax2 * .8f)
            {
                player.manaCost *= .75f;
                player.GetDamage(DamageClass.Magic) += .1f;
            }
        }
    }
}