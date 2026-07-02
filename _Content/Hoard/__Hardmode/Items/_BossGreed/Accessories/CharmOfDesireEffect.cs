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
    public class CharmOfDesireEffect(int bonusDamageCap = 20) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<CharmOfDesirePlayer>().effect = true;
            player.GetModPlayer<CharmOfDesirePlayer>().BonusDamageCap = bonusDamageCap;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith(bonusDamageCap, Main.LocalPlayer.GetModPlayer<CharmOfDesirePlayer>().BonusDamage);
    }

    public class CharmOfDesirePlayer : EquipmentEffectPlayer
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