using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using System;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Accessories
{
    public class NaitokurosuMireEffect : EquipmentEffectData
    {
        public const float DAMAGEBOOST = 0.09f;

        public override void DoEffect(Player player)
        {
            if (player.GetModPlayer<ZAAPlayer>().ZoneMire)
                player.GetDamage(DamageClass.Ranged) += DAMAGEBOOST;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(Math.Round(DAMAGEBOOST * 100, 0));
    }
}