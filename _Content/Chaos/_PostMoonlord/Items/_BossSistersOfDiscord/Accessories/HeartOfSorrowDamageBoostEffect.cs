using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Accessories
{
    public class HeartOfSorrowDamageBoostEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 1 - player.statLife / player.statLifeMax;
            player.GetDamage(DamageClass.Ranged) += 1 - player.statLife / player.statLifeMax;
        }
    }
}