using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class Spark3 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spark");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
        }

        public override void AI()
        {
            Projectile.timeLeft --;
            if (Projectile.timeLeft <= 0)
            {
                Projectile.Kill();
            }
            Lighting.AddLight(Projectile.Center, (255 - Projectile.alpha) * 0.9f / 255f, (255 - Projectile.alpha) * .8f / 255f, 0);
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 1f;
                SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
            }

            int dustType = DustID.Torch;
            if (Main.rand.Next(3) == 0)
            {
                for (int m = 0; m < 3; m++)
                {
                    int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.White, 1.6f);
                    Main.dust[dustID].velocity = -Projectile.velocity * 0.5f;
                    Main.dust[dustID].noLight = false;
                    Main.dust[dustID].noGravity = true;
                }
                int dustID2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID2].velocity = -Projectile.velocity * 0.5f;
                Main.dust[dustID2].noLight = false;
                Main.dust[dustID2].noGravity = true;
            }

            if (Projectile.frameCounter++ > 6)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Color.White.R, Color.White.G, Color.White.B, 120);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 300);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item89);

            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<SparkBoom3>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0f);
        }
    }
}