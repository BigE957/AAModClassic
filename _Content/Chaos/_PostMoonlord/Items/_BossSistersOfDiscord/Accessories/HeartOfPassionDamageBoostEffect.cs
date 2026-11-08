using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Accessories
{
    public class HeartOfPassionDamageBoostEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetDamage(DamageClass.Magic) += 1 - player.statLife / player.statLifeMax;
            player.GetDamage(DamageClass.Summon) += 1 - player.statLife / player.statLifeMax;
        }
    }
}