using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ModLoader;

namespace AAModClassic._Content._Tinker._PostMoonlord.Items.Accessories
{
    public class EquinoxEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.17f;

            player.GetCritChance(DamageClass.Generic) += 5;
            player.GetCritChance(DamageClass.Summon) -= 5;

            player.GetAttackSpeed(DamageClass.Melee) += 0.10f;

            player.GetKnockback(DamageClass.Summon).Base += 0.75f;

            player.lifeRegen += 6;
            player.statDefense += 9;
            player.pickSpeed -= 0.35f;
        }
    }
}