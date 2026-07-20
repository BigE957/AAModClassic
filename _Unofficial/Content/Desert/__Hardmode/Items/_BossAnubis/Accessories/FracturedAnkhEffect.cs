using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.Accessories;
using AAModClassic.Dusts;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Desert.__Hardmode.Items._BossAnubis.Accessories
{
    public class FracturedAnkhEffect : EquipmentEffectData
    {
        public readonly int[] AnkhBuffs =
        {
            BuffID.Bleeding,
            BuffID.BrokenArmor,
            BuffID.Confused,
            BuffID.Cursed,
            BuffID.Darkness,
            BuffID.Poisoned,
            BuffID.Silenced,
            BuffID.Slow,
            BuffID.Stoned,
            BuffID.Weak
        };

        public override void DoEffect(Player player)
        {
            foreach (int buffID in AnkhBuffs)
            {
                player.AddBuff(buffID, 2);
            }
        }
    }
}