using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.NPCs._BossEquinox
{
    public class NightclawerScythe : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Nightclawer Scythe");
		}

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
			Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            if(Projectile.localAI[1] ++ == 5f)
            {
                SpawnDust();
            }
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), .37f, .8f, .89f);

            if(Main.rand.NextBool(10))
            {
                int dustId = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.NightcrawlerDust>(), Projectile.velocity.X,
                    Projectile.velocity.Y, 100, new Color(), 2f);
                Main.dust[dustId].noGravity = true;
            }

            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                Projectile.rotation = Projectile.ai[0];
                Projectile.spriteDirection = -(int)Projectile.ai[1];
            }
            if (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) < 20)
            {
                Projectile.velocity *= 1.05f;
            }
            if (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) < 40)
            {
                Projectile.velocity *= 1.012f;
            }
            if (Projectile.velocity.X < 0f)
            {
                Projectile.direction = -1;
            }
            else
            {
                Projectile.direction = 1;
            }
            Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.025f * Projectile.direction;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(95, 205, 228, 200);
        }

        public void SpawnDust()
        {
            Vector2 position = Projectile.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.NightcrawlerDust>(), 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 7; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.NightcrawlerDust>(), 0, 0, 100, new Color(), 2f);
                Main.dust[num88].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.NightcrawlerDust>(), 0, 0, 100, new Color(), 2f);
                Main.dust[num88].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * 8f;
            }
        }
    }
}