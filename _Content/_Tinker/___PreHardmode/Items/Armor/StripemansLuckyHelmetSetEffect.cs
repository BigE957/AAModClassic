using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic.Achievements;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items.Armor
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
        public bool doEffectForReal;

        public override void ResetEffects()
        {
            doEffectForReal = false;
        }

        public override void PostUpdate()
        {
            if (effect)
            {
                LuckyArmorEquipped.Condition.Complete();
                if (AAMod.ArmorAbilityKey.JustPressed)
                    doEffectForReal = !doEffectForReal;

                Main.CurrentPlayer.GetModPlayer<AAPlayer>().CrasyLucky = doEffectForReal;
            }
        }
    }
}