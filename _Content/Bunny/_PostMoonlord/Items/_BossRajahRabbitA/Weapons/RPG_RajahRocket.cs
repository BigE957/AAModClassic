using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using AAModClassic._Content.Bunny.Projectiles;
using AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.Weapons;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Weapons

{
    public class RPG_RajahRocket : Bunzooka_RajahRocket
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rajah Rocket");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            int p = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<RPG_Bunnysplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            Main.projectile[p].DamageType = DamageClass.Ranged;
            Main.projectile[p].Center = Projectile.Center;
            float spread = 12f * 0.0174f;
            double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - spread / 2;
            double deltaAngle = spread / 6;
            for (int i = 0; i < 3; i++)
            {
                double offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                int proj = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 3f) * 5, (float)(Math.Cos(offsetAngle) * 3f) * 5, ModContent.ProjectileType<RajahCarrotEX>(), Projectile.damage / 6, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Main.projectile[proj].DamageType = DamageClass.Ranged;
                 proj = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 3f) * 5, (float)(-Math.Cos(offsetAngle) * 3f) * 5, ModContent.ProjectileType<RajahCarrotEX>(), Projectile.damage / 6, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Main.projectile[proj].DamageType = DamageClass.Ranged;
            }
        }
    }
}
