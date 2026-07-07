using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Chaos._PostMoonlord.Items.Accessories
{
    public class HeartOfAnarchyDamageBoostEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 1 - player.statLife / player.statLifeMax;
        }
    }
}