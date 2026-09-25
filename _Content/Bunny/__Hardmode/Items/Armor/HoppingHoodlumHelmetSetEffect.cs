using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny.__Hardmode.Items.Armor
{
    public class HoppingHoodlumHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (player.statLife <= player.statLifeMax2 * .5f)
            {
                player.moveSpeed += .5f;
                player.GetDamage(DamageClass.Summon) += .5f;
                player.GetDamage(DamageClass.Melee) += .5f;
            }
        }
    }
}