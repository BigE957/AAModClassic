using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class BurningGelP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Burning Gel");
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(261);
			Projectile.aiStyle = ProjAIStyleID.GroundProjectile;
			AIType = ProjectileID.BoulderStaffOfEarth;
			Projectile.width = 20;
			Projectile.height = 18;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 300;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.Kill();
			return true;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.OnFire, 600);
			target.immune[Projectile.owner] = 1;
			Projectile.Kill();
		}

		public override void AI()
		{
			Projectile.alpha = 0;
			if (Main.rand.Next(2) == 0)
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Torch,
				Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
				dust.velocity += Projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
		}

		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.NPCDeath22, Projectile.position);
			if (Projectile.ai[0] > 7f)
			{
				float num296 = 1f;
				int num297 = 6;
				for (int num298 = 0; num298 < 50; num298++)
				{
					int num299 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num297, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
					if (num297 == 235 && Main.rand.NextBool(3))
					{
						Main.dust[num299].noGravity = true;
						Main.dust[num299].scale *= 3f;
						Dust DD = Main.dust[num299];
						DD.velocity.X *= 2f;
						Dust DDD = Main.dust[num299];
						DDD.velocity.Y *= 2f;
					}
					else
					{
						Main.dust[num299].scale *= 1.5f;
					}
					Dust DDDD = Main.dust[num299];
					DDDD.velocity.X *= 1.2f;
					Dust DDDDD = Main.dust[num299];
					DDDDD.velocity.Y *= 1.2f;
					Main.dust[num299].scale *= num296;
					if (num297 == 75)
					{
						Main.dust[num299].velocity += Projectile.velocity;
						if (!Main.dust[num299].noGravity)
						{
							Main.dust[num299].velocity *= 0.5f;
						}
					}
				}
			}
		}
	}
}