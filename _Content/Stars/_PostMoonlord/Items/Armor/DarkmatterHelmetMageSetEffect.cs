using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    public class DarkmatterHelmetMageSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<StarHelmetMagePlayer>().setBonus = true;
            player.GetModPlayer<StarHelmetMagePlayer>().sunSiphon = false;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(Main.LocalPlayer.GetDamage(DamageClass.Magic).ApplyTo(100), Main.LocalPlayer.GetCritChance(DamageClass.Magic));
    }
}