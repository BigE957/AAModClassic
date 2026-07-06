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
    public class TheBookOfRunesEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<TheBookOfRunesPlayer>().effect = true;
        }
    }

    public class TheBookOfRunesPlayer : EquipmentEffectPlayer
    {
        public override void PostUpdate()
        {
            float slotscanuse = Player.maxMinions - Player.slotsMinions;

            if (effect && slotscanuse > 1)
            {
                bool RuneControlEX = Player.ownedProjectileCounts[ModContent.ProjectileType<TheBookOfRunesEffect_TerraRune>()] > 1 || Player.ownedProjectileCounts[ModContent.ProjectileType<TheBookOfRunesEffect_ChaosRune>()] > 1 || Player.ownedProjectileCounts[ModContent.ProjectileType<TheBookOfRunesEffect_VoidRune>()] > 1;

                if (RuneControlEX)
                {
                    Player.ClearBuff(ModContent.BuffType<TheBookOfRunesEffect_Buff>());
                }
                if (!Player.HasBuff<TheBookOfRunesEffect_Buff>())
                {
                    Player.AddBuff(ModContent.BuffType<TheBookOfRunesEffect_Buff>(), 3600, true);
                }

                if (Player.ownedProjectileCounts[ModContent.ProjectileType<TheBookOfRunesEffect_TerraRune>()] < 1 && slotscanuse > 1f)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, -1f, ModContent.ProjectileType<TheBookOfRunesEffect_TerraRune>(), (int)(Player.GetDamage(DamageClass.Summon)).ApplyTo(1), 0, Player.whoAmI, 0f, 0f);
                }
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<TheBookOfRunesEffect_ChaosRune>()] < 1 && slotscanuse > 2f)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, -1f, ModContent.ProjectileType<TheBookOfRunesEffect_ChaosRune>(), (int)(Player.GetDamage(DamageClass.Summon)).ApplyTo(400), 4f, Player.whoAmI, 0f, 0f);
                }
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<TheBookOfRunesEffect_VoidRune>()] < 1 && slotscanuse > 3f)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, -1f, ModContent.ProjectileType<TheBookOfRunesEffect_VoidRune>(), (int)(Player.GetDamage(DamageClass.Summon)).ApplyTo(800), 2f, Player.whoAmI, 0f, 0f);
                }
            }
        }
    }
}