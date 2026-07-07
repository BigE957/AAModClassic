using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using System;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
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
