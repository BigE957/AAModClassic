using AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Accessories;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Globals;
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

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Accessories
{
    public class TaiyangBaoleiEnduranceEffect : EquipmentEffectData
    {
        public const float ENDURANCEBOOST = 0.06f;

        public override void DoEffect(Player player)
        {
            if (Main.dayTime)
                player.endurance += ENDURANCEBOOST;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(Math.Round(ENDURANCEBOOST * 100, 0));
    }
}