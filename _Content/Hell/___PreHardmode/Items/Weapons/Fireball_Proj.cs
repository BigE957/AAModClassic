using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hell.___PreHardmode.Items.Weapons
{
	public class Fireball_Proj : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(14);
			Projectile.penetrate = 1;
			Projectile.width = 16;
			Projectile.height = 16;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
			Projectile.timeLeft = 300;
			Projectile.alpha = 10;
			Projectile.aiStyle = ProjAIStyleID.Arrow;
			AIType = ProjectileID.Bullet;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fireball");
		}

		public override void AI()
		{
			Projectile.alpha = 10;
			for (int index1 = 0; index1 < 3; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, (float)(-Projectile.velocity.X * 0.2), (float)(-Projectile.velocity.Y * 0.2), 100, new Color(), 2f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 2f;
				Main.dust[index2].scale *= 0.8f;
				int index3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, (float)(-Projectile.velocity.X * 0.2), (float)(-Projectile.velocity.Y * 0.2), 100, new Color(), 1f);
				Main.dust[index3].velocity *= 2f;
				Main.dust[index3].scale *= 0.8f;
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.OnFire, 300);
			Projectile.Kill();
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.Kill();
			return true;
		}

		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<Fireball_Explosion>(), (int)(Projectile.damage * 1.2f), Projectile.knockBack, Projectile.owner, -10f, 0f);
			for (int index1 = 0; index1 < 30; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0.0f, 0.0f, 100, new Color(), 1.5f);
				Main.dust[index2].velocity *= 1.4f;
			}
			for (int index1 = 0; index1 < 20; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0.0f, 0.0f, 100, new Color(), 3.5f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 7f;
				int index3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0.0f, 0.0f, 100, new Color(), 1.5f);
				Main.dust[index3].velocity *= 3f;
			}
			if(!Main.dedServ)
				for (int index1 = 0; index1 < 2; ++index1)
				{
					float num2 = 0.4f;
					if (index1 == 1)
					num2 = 0.8f;
					int index2 = Gore.NewGore(Projectile.GetSource_Death(), new Vector2(Projectile.position.X, Projectile.position.Y), new Vector2(), Main.rand.Next(61, 64), 1f);
					Main.gore[index2].velocity *= num2;
					++Main.gore[index2].velocity.X;
					++Main.gore[index2].velocity.Y;
					int index3 = Gore.NewGore(Projectile.GetSource_Death(), new Vector2(Projectile.position.X, Projectile.position.Y), new Vector2(), Main.rand.Next(61, 64), 1f);
					Main.gore[index3].velocity *= num2;
					--Main.gore[index3].velocity.X;
					++Main.gore[index3].velocity.Y;
					int index4 = Gore.NewGore(Projectile.GetSource_Death(), new Vector2(Projectile.position.X, Projectile.position.Y), new Vector2(), Main.rand.Next(61, 64), 1f);
					Main.gore[index4].velocity *= num2;
					++Main.gore[index4].velocity.X;
					--Main.gore[index4].velocity.Y;
					int index5 = Gore.NewGore(Projectile.GetSource_Death(), new Vector2(Projectile.position.X, Projectile.position.Y), new Vector2(), Main.rand.Next(61, 64), 1f);
					Main.gore[index5].velocity *= num2;
					--Main.gore[index5].velocity.X;
					--Main.gore[index5].velocity.Y;
				}
		}
	}
}
