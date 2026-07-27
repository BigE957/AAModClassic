using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic._Unofficial.Content._Tinker.EquipmentEffects;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Desert.___PreHardmode.Items.Accessories
{
    public class RaptorTalonEffect : EquipmentEffectData
    {
        public const int CRIT_BOOST_MAX = 8;
        public const int DISTANCE_MAX = 150;
        public const int DISTANCE_MIN = 800;

        public override void DoEffect(Player player)
        {
            player.GetModPlayer<HeavyCritPlayer>().OddChanceModifiers.Add(CritChanceOnDistance);
        }

        public int CritChanceOnDistance(NPC target, NPC.HitModifiers modifiers, Player player)
        {
            float distance = player.Center.Distance(target.Center);
            float distancePercent = Math.Clamp(1 - ((distance - DISTANCE_MAX) / (DISTANCE_MIN - DISTANCE_MAX)), 0, 1);
            float exactBonusChance = MathHelper.Lerp(0, CRIT_BOOST_MAX, distancePercent);
            int bonusChance = (int)MathF.Ceiling(exactBonusChance);

            return bonusChance;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(CRIT_BOOST_MAX);
    }
}