using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.Accessories;
using AAModClassic._Removed.Content._Tinker.__Hardmode.Items.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRetriever.Accessories
{
    public class StormClawEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<StormClawPlayer>().effect = true;
        }
    }

    public class StormClawPlayer : EquipmentEffectPlayer
    {
        public override float UseTimeMultiplier(Item item)
        {
            if (!effect)
                return 1f;

            // Only speed up manual-swing weapons
            if (item.autoReuse || item.damage <= -1)
                return 1f;

            float multiplier = 2f;

            // Keep useTime >= 1 tick
            multiplier = Math.Min(multiplier, item.useTime);

            // Keep useAnimation >= 2 ticks
            multiplier = Math.Min(multiplier, item.useAnimation / 2f);

            // Never let it go below 1
            multiplier = Math.Max(multiplier, 1f);

            return multiplier;
        }
    }
}