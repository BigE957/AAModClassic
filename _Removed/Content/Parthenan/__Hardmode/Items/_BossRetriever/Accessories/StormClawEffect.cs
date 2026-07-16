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
            if (!player.HeldItem.autoReuse && player.HeldItem.damage > -1 && !Main.SettingsEnabled_AutoReuseAllItems)
            {
                player.GetAttackSpeed(DamageClass.Generic) *= 2;
            }
        }
    }
}