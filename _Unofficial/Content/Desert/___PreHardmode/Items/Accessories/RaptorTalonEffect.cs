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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Desert.___PreHardmode.Items.Accessories
{
    public class RaptorTalonEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<HeavyCritPlayer>().OddChanceModifiers.Add(CritChanceOnDistance);
        }

        public int CritChanceOnDistance(NPC target, NPC.HitModifiers modifiers, Player player)
        {
            const int MAX_DISTANCE = 150;
            const int MIN_DISTANCE = 800;
            
            float distance = player.Center.Distance(target.Center);
            float distancePercent = Math.Clamp(1 - ((distance - MAX_DISTANCE) / (MIN_DISTANCE - MAX_DISTANCE)), 0, 1);
            distancePercent = MathF.Round(distancePercent, 1);
            int bonusChance = (int)MathHelper.Lerp(0, 10, distancePercent);

            return bonusChance;
        }
    }
}