using AAModClassic._Content._EX._PostMoonlord.Items.Accessories;
using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Accessories
{
    public class APageOfTheRuneBookEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<APageOfTheRuneBookPlayer>().effect = true;
        }
    }

    public class APageOfTheRuneBookPlayer : EquipmentEffectPlayer
    {
        public override void PostUpdate()
        {
            float slotscanuse = Player.maxMinions - Player.slotsMinions;

            if (effect && slotscanuse > 1)
            {
                bool RuneControl = Player.ownedProjectileCounts[ModContent.ProjectileType<APageOfTheRuneBookEffect_BunnyRune>()] > 1 || Player.ownedProjectileCounts[ModContent.ProjectileType<APageOfTheRuneBookEffect_DiscordRune>()] > 1 || Player.ownedProjectileCounts[ModContent.ProjectileType<APageOfTheRuneBookEffect_EnergyRune>()] > 1;

                if (RuneControl)
                {
                    Player.ClearBuff(ModContent.BuffType<APageOfTheRuneBookEffect_Buff>());
                }
                if (!Player.HasBuff<APageOfTheRuneBookEffect_Buff>())
                {
                    Player.AddBuff(ModContent.BuffType<APageOfTheRuneBookEffect_Buff>(), 3600, true);
                }

                if (Player.ownedProjectileCounts[ModContent.ProjectileType<APageOfTheRuneBookEffect_BunnyRune>()] < 1 && slotscanuse > 1f)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, -1f, ModContent.ProjectileType<APageOfTheRuneBookEffect_BunnyRune>(), (int)(Player.GetDamage(DamageClass.Summon)).ApplyTo(1), 0, Player.whoAmI, 0f, 0f);
                }
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<APageOfTheRuneBookEffect_DiscordRune>()] < 1 && slotscanuse > 2f)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, -1f, ModContent.ProjectileType<APageOfTheRuneBookEffect_DiscordRune>(), (int)(Player.GetDamage(DamageClass.Summon)).ApplyTo(50), 4f, Player.whoAmI, 0f, 0f);
                }
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<APageOfTheRuneBookEffect_EnergyRune>()] < 1 && slotscanuse > 3f)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, -1f, ModContent.ProjectileType<APageOfTheRuneBookEffect_EnergyRune>(), (int)(Player.GetDamage(DamageClass.Summon)).ApplyTo(100), 2f, Player.whoAmI, 0f, 0f);
                }
            }
        }
    }
}