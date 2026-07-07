using AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.Accessories;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Accessories
{
    [AutoloadEquip(EquipType.Back, EquipType.Front)]
    public class RajahRabbitsCloakOfSupremacy : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rajah Rabbit's Cloak of Supremecy");
            /* Tooltip.SetDefault(@"'You have been deemed a worthy successor by the Champion of the Innocent'"); */
        }

        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 78;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.accessory = true;
            Item.expert = true;
            Item.defense = 10;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new MovementSpeedEffect(0.4f));
            AddEffect(new JumpStatsEffect(3.6f, 40, true));
            AddEffect<FallDamageImmunityEffect>();
            AddEffect<RajahRabbitsCloakOfSupremacyEffect>();
        }
    }
}