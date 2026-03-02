using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class SparkFury : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fury Spark");
		}

		public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.alpha = 255;
            Projectile.penetrate = 5;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.03f * Projectile.direction;
            Projectile.alpha = 255;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] > 3f)
            {
                int num15 = 100;
                if (Projectile.ai[0] > 20f)
                {
                    int num16 = 40;
                    float num17 = Projectile.ai[0] - 20f;
                    num15 = (int)(100f * (1f - num17 / num16));
                    if (num17 >= num16)
                    {
                        Projectile.Kill();
                    }
                }
                if (Projectile.ai[0] <= 10f)
                {
                    num15 = (int)Projectile.ai[0] * 10;
                }
                if (Main.rand.Next(100) < num15)
                {
                    int num18 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 150);
                    Main.dust[num18].position = (Main.dust[num18].position + Projectile.Center) / 2f;
                    Main.dust[num18].noGravity = true;
                    Main.dust[num18].velocity *= 2f;
                    Main.dust[num18].scale *= 1.2f;
                    Main.dust[num18].velocity += Projectile.velocity;
                }
            }
            if (Projectile.ai[0] >= 20f)
            {
                Projectile.velocity.Y = Projectile.velocity.Y + 0.1f;
            }
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[1] == 1)
            {
                target.AddBuff(BuffID.Daybreak, 160);
            }
            else
            {
                target.AddBuff(BuffID.OnFire, 160);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate == 0)
            {
                Projectile.Kill();
            }
            return false;
        }
    }
}
