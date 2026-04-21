using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Anubis
{
    class DesertStaff_DesertBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Desert Blast");
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 20;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, (255 - Projectile.alpha) * 1f / 255f, (255 - Projectile.alpha) * 0.7f / 255f, (255 - Projectile.alpha) * 0f / 255f);
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            for (int num339 = 0; num339 < 16; num339++)
            {
                Dust dust1;
                Vector2 position = Projectile.position;
                dust1 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.JudgementDust>(), 0, 0, 0, Color.White, 1f)];
                dust1.noGravity = true;
            }
        }

        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            for (int num506 = 0; num506 < 15; num506++)
            {
                Dust dust1;
                Vector2 position = Projectile.position;
                dust1 = Main.dust[Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.JudgementDust>(), 0, 0, 0, Color.White, 1f)];
                dust1.noGravity = true;
            }
			for (int h = 0; h < 3; h++)
			{
				Vector2 vel = new Vector2(0, -1);
				float rand = Main.rand.NextFloat() * 6.283f;
				vel = vel.RotatedBy(rand);
				vel *= 8f;
				Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, vel.X, vel.Y, ModContent.ProjectileType<DesertStaff_FragBlast>(), Projectile.damage, 0, Main.myPlayer);
			}
        }
    }
}