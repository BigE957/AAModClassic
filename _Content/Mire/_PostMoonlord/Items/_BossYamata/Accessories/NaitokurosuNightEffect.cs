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
    public class NaitokurosuNightEffect(float increase) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (!Main.dayTime)
                player.moveSpeed += increase;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(Math.Round(increase * 100, 0));
    }
}