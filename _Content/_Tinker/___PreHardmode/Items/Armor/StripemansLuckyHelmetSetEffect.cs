using AAModClassic.Achievements;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Terraria.Localization;

namespace AAModClassic._Content._Tinker.___PreHardmode.Items.Armor
{
    public class StripemansLuckyHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<StripemansLuckyHelmetSetPlayer>().effect = true;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(EnabledDisabledTextThatIsHardcodedBecauseItIsImportant(Main.LocalPlayer.GetModPlayer<StripemansLuckyHelmetSetPlayer>().doEffectForReal));

        public string EnabledDisabledTextThatIsHardcodedBecauseItIsImportant(bool thing)
        {
            return thing ? "ACTIVATED" : "UNACTIVATED";
        }
    }

    public class StripemansLuckyHelmetSetPlayer : EquipmentEffectPlayer
    {
        public bool doEffectForReal = false;

        public override void PostUpdate()
        {
            if (effect)
            {
                LuckyArmorEquipped.Condition.Complete();
                if (AAMod.ArmorAbilityKey.JustPressed)
                    doEffectForReal = !doEffectForReal;

                Main.CurrentPlayer.GetModPlayer<ZAAPlayer>().CrasyLucky = doEffectForReal;
            }
        }
    }
}