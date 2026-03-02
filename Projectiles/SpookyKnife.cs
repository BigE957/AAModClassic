using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
	public class SpookyKnife : ModProjectile
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
			Projectile.alpha = 0;
			Projectile.aiStyle = 1;
			AIType = 14;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fireball");
			Projectile.light = 0.33f;
		}

		public override void AI()
		{
			Projectile.alpha = 0;
			if (Main.rand.Next(3) == 0)
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, 6,
					Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
				dust.velocity += Projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
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
			SoundEngine.PlaySound(SoundID.NPCHit7, Projectile.position);
			for (int h = 0; h < 3; h++)
			{
				Vector2 vel = new Vector2(0, -1);
				float rand = Main.rand.NextFloat() * 6.3f;
				vel = vel.RotatedBy(rand);
				vel *= 4f;
				int type = Main.rand.Next(326,328);
				int proj = Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, vel.X, vel.Y, type, Projectile.damage/2, 0, Main.myPlayer);
				Main.projectile[proj].hostile = false;
				Main.projectile[proj].friendly = true;
			}
		}
	}
}
