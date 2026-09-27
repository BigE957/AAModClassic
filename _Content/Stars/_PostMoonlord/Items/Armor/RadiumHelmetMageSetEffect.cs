using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    public class RadiumHelmetMageSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<StarHelmetMagePlayer>().setBonus = true;
            player.GetModPlayer<StarHelmetMagePlayer>().sunSiphon = true;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(Main.LocalPlayer.GetDamage(DamageClass.Magic).ApplyTo(100), Main.LocalPlayer.GetCritChance(DamageClass.Magic));
    }
}