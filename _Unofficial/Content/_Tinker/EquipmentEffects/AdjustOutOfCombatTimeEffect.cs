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
    /// <summary>
    /// keep in ind ur adding seconds
    /// </summary>
    /// <param name="amount"></param>
    public class AdjustOutOfCombatTimeEffect(int amount) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<OutOfCombatPlayer>().OutOfCombatThresholdModifier += amount;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(Math.Abs(amount), ChatUtils.IncreaseOrDecreaseText(-amount, ChatUtils.IncreaseDecreaseTextType.FasterSlower));
    }
}