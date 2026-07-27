using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content._Tinker.EquipmentEffects
{
    //TODO: add class support lol
    public class HeavyCritChanceEffect(int amount, DamageClass damageClass) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<HeavyCritPlayer>().HeavyCritChance += amount;
        }

        public override string GetDescription()
        {
            const string rootPath = "Mods.AAModClassic.EquipStats";

            string extraSpaceForGeneric = " ";
            if (damageClass == DamageClass.Generic)
                extraSpaceForGeneric = "";

            string damageTypePath = Language.GetTextValue($"{rootPath}.ClassGlobalStats.{damageClass.Name}");

            return Language.GetTextValue(Description).FormatWith(Math.Abs(amount), ChatUtils.IncreaseOrDecreaseText(amount, ChatUtils.IncreaseDecreaseTextType.IncreasedDecreased), damageTypePath, extraSpaceForGeneric);
        }
    }
}