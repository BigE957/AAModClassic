using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Terraria.GameContent.Events;
using Terraria.Localization;
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

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(amount * 100);
    }
}
