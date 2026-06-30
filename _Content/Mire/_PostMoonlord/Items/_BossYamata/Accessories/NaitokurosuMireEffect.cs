using AAModClassic._Content.Mire.Buffs;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Accessories;
using AAModClassic.Rarities;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Accessories
{
    public class NaitokurosuMireEffect : EquipmentEffectData
    {
        public const float DAMAGEBOOST = 0.09f;

        public override void DoEffect(Player player)
        {
            if (player.GetModPlayer<AAPlayer>().ZoneMire)
                player.GetDamage(DamageClass.Ranged) += DAMAGEBOOST;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(Math.Round(DAMAGEBOOST * 100, 0));
    }
}