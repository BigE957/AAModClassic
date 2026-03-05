using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class AxisShot : ModProjectile
    {
        public override void SetDefaults()
        {
			Projectile.CloneDefaults(343);
			Projectile.light = 1f;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Axis Shot");
        }
		
        public override void AI()
        {
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] > 45f)
			{
				Projectile.ai[0] = 45f;
				Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
				if (Projectile.velocity.Y > 16f)
				{
					Projectile.velocity.Y = 16f;
				}
				Projectile.velocity.X = Projectile.velocity.X * 0.995f;
			}
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
			Projectile.alpha -= 50;
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			if (Projectile.owner == Main.myPlayer)
			{
				Projectile.localAI[0] += 1f;
				if (Projectile.localAI[0] >= 8f)
				{
					Projectile.localAI[0] = 0f;
					int num566 = 0;
					int num3;
					for (int num567 = 0; num567 < 1000; num567 = num3 + 1)
					{
						if (Main.projectile[num567].active && Main.projectile[num567].owner == Projectile.owner && Main.projectile[num567].type == ProjectileID.NorthPoleSnowflake)
						{
							num566++;
						}
						num3 = num567;
					}
					float num568 = Projectile.damage * 0.8f;
					if (num566 > 100)
					{
						float num569 = num566 - 100;
						num569 = 1f - num569 / 110f;
						num568 *= num569;
					}
					if (num566 > 100)
					{
						Projectile.localAI[0] -= 1f;
					}
					if (num566 > 120)
					{
						Projectile.localAI[0] -= 1f;
					}
					if (num566 > 140)
					{
						Projectile.localAI[0] -= 1f;
					}
					if (num566 > 150)
					{
						Projectile.localAI[0] -= 1f;
					}
					if (num566 > 160)
					{
						Projectile.localAI[0] -= 1f;
					}
					if (num566 > 165)
					{
						Projectile.localAI[0] -= 1f;
					}
					if (num566 > 170)
					{
						Projectile.localAI[0] -= 2f;
					}
					if (num566 > 175)
					{
						Projectile.localAI[0] -= 3f;
					}
					if (num566 > 180)
					{
						Projectile.localAI[0] -= 4f;
					}
					if (num566 > 185)
					{
						Projectile.localAI[0] -= 5f;
					}
					if (num566 > 190)
					{
						Projectile.localAI[0] -= 6f;
					}
					if (num566 > 195)
					{
						Projectile.localAI[0] -= 7f;
					}
					if (num568 > Projectile.damage * 0.1f)
					{
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("AxisSnow").Type, (int)num568, Projectile.knockBack * 0.55f, Projectile.owner, 0f, Main.rand.Next(3));
						return;
					}
				}
			}
        }
		
		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
			int num3;
			for (int num369 = 4; num369 < 31; num369 = num3 + 1)
			{
				float num370 = Projectile.oldVelocity.X * (30f / num369);
				float num371 = Projectile.oldVelocity.Y * (30f / num369);
				int num372 = Dust.NewDust(new Vector2(Projectile.oldPosition.X - num370, Projectile.oldPosition.Y - num371), 8, 8, DustID.DungeonSpirit, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, default, 1.2f);
				Main.dust[num372].noGravity = true;
				Dust dust = Main.dust[num372];
				dust.velocity *= 0.5f;
				num3 = num369;
			}
		}
		
		public bool stop = false;
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.immune[Projectile.owner] = 1;
			if (!stop)
			{
				Vector2 vel1 = new Vector2(-1, -1);
				vel1 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X+130, target.position.Y+130, vel1.X, vel1.Y, Mod.Find<ModProjectile>("AxisSnow").Type, Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel2 = new Vector2(1, 1);
				vel2 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X-130, target.position.Y-130, vel2.X, vel2.Y, Mod.Find<ModProjectile>("AxisSnow").Type, Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel3 = new Vector2(1, -1);
				vel3 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X-130, target.position.Y+130, vel3.X, vel3.Y, Mod.Find<ModProjectile>("AxisSnow").Type, Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel4 = new Vector2(-1, 1);
				vel4 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X+130, target.position.Y-130, vel4.X, vel4.Y, Mod.Find<ModProjectile>("AxisSnow").Type, Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel5 = new Vector2(0, -1);
				vel5 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X, target.position.Y+130, vel5.X, vel5.Y, Mod.Find<ModProjectile>("AxisSnow").Type, Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel6 = new Vector2(0, 1);
				vel6 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X, target.position.Y-130, vel6.X, vel6.Y, Mod.Find<ModProjectile>("AxisSnow").Type, Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel7 = new Vector2(1, 0);
				vel7 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X-130, target.position.Y, vel7.X, vel7.Y, Mod.Find<ModProjectile>("AxisSnow").Type, Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel8 = new Vector2(-1, 0);
				vel8 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X+130, target.position.Y, vel8.X, vel8.Y, Mod.Find<ModProjectile>("AxisSnow").Type, Projectile.damage/3, 0, Main.myPlayer);
				stop = true;
			}
		}
		
		public override Color? GetAlpha(Color newColor)
		{
			float num6 = 1f - Projectile.alpha / 255f;
			return new Color((int)(250f * num6), (int)(250f * num6), (int)(250f * num6), (int)(100f * num6));
		}
    }
}
