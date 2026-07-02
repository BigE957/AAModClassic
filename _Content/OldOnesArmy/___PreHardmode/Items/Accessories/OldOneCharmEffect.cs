using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.OldOnesArmy.___PreHardmode.Items.Accessories
{
    public class OldOneCharmEffect(float amount) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (DD2Event.Ongoing)
                player.GetDamage(DamageClass.Summon) += amount;
        }
    }
}
