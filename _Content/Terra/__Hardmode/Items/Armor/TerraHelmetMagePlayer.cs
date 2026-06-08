using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terra.__Hardmode.Items.Armor
{
    public class TerraHelmetMagePlayer : EquipEffectAbstract
    {
        public override void PostUpdate()
        {
            if (effect && !Player.HasBuff(ModContent.BuffType<TerraHelmetMagePlayer_TerraRoseCooldown>()))
            {
                if (AAMod.ArmorAbilityKey.JustPressed)
                {
                    Player.AddBuff(ModContent.BuffType<TerraHelmetMagePlayer_TerraRoseCooldown>(), 600);
                    float playerY = Player.position.Y + Player.height;

                    Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X - 64, playerY), new Vector2(0, -10), ModContent.ProjectileType<TerraHelmetMagePlayer_TerraRose>(), (int)Player.GetDamage(DamageClass.Magic).ApplyTo(50), 4, Main.myPlayer);
                    Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X + 64, playerY), new Vector2(0, -10), ModContent.ProjectileType<TerraHelmetMagePlayer_TerraRose>(), (int)Player.GetDamage(DamageClass.Magic).ApplyTo(50), 4, Main.myPlayer);
                }
            }
        }
    }
}