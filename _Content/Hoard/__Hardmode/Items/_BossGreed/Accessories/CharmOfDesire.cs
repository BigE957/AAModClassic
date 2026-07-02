using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Accessories;
using AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.Accessories;
using AAModClassic.Dusts;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.Accessories
{
    public class CharmOfDesire : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Charm of Desire");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 6, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void RegisterEquipStats()
        {
            AddEffect(new CharmOfDesireEffect(20));
        }
    }

    public class CharmOfDesireEffect(int bonusDamageCap = 20) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<CharmOfDesirePlayer>().effect = true;
            player.GetModPlayer<CharmOfDesirePlayer>().BonusDamageCap = bonusDamageCap;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(bonusDamageCap, Main.LocalPlayer.GetModPlayer<CharmOfDesirePlayer>().BonusDamage);
    }

    public class CharmOfDesirePlayer : EquipEffectAbstract
    {
        public int BonusDamageCap;
        public float BonusDamage;

        public override void PostUpdate()
        {
            if (!Main.LocalPlayer.HasBuff<CharmOfDesire_Desire>())
                BonusDamage = 0;
        }
    }

    public class CharmOfDesireItem : GlobalItem
    {
        public override bool OnPickup(Item item, Player player)
        {
            CharmOfDesirePlayer modPlayer = player.GetModPlayer<CharmOfDesirePlayer>();
            if (item.ammo == AmmoID.Coin)
            {
                if (modPlayer.effect)
                {
                    player.AddBuff(ModContent.BuffType<CharmOfDesire_Desire>(), 240);
                    if (modPlayer.BonusDamage < modPlayer.BonusDamageCap)
                    {
                        modPlayer.BonusDamage += 1;
                    }
                }
            }

            return base.OnPickup(item, player);
        }
    }
}