using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Accessories
{
    public class HeartOfPassionManaRegenEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (player.statLife > player.statLifeMax * (2 / 3))
                player.manaRegenBonus += 6;
        }
    }
}