using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terra.__Hardmode.Items.Armor
{
    public class TerraHelmetMageSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<TerraHelmetMageSetPlayer>().effect = true;
        }
    }

    public class TerraHelmetMageSetPlayer : EquipmentEffectPlayer
    {
        public override void PostUpdate()
        {
            if (effect && !Player.HasBuff(ModContent.BuffType<TerraHelmetMageSetEffect_TerraRoseCooldown>()))
            {
                if (AAMod.ArmorAbilityKey.JustPressed)
                {
                    Player.AddBuff(ModContent.BuffType<TerraHelmetMageSetEffect_TerraRoseCooldown>(), 600);
                    float playerY = Player.position.Y + Player.height;

                    Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X - 64, playerY), new Vector2(0, -10), ModContent.ProjectileType<TerraHelmetMageSetEffect_TerraRose>(), (int)Player.GetDamage(DamageClass.Magic).ApplyTo(50), 4, Main.myPlayer);
                    Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X + 64, playerY), new Vector2(0, -10), ModContent.ProjectileType<TerraHelmetMageSetEffect_TerraRose>(), (int)Player.GetDamage(DamageClass.Magic).ApplyTo(50), 4, Main.myPlayer);
                }
            }
        }
    }
}