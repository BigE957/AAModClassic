using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class AtlanteanTridentP : ModProjectile
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetDefaults()
		{
			Projectile.CloneDefaults(14);
			Projectile.penetrate = 1;
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
			Projectile.timeLeft = 300;
			Projectile.alpha = 150;
			Projectile.aiStyle = ProjAIStyleID.Arrow;
			AIType = ProjectileID.Bullet;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Atlantean Trident Water Sphere");
		}

		public override void AI()
		{
			Projectile.alpha = 150;
			for (int index1 = 0; index1 < 2; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Water_Space, (float)(-Projectile.velocity.X * 0.2), (float)(-Projectile.velocity.Y * 0.2), 50, new Color(), 2f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 2f;
				Main.dust[index2].scale *= 0.75f;
				int index3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Water_Space, (float)(-Projectile.velocity.X * 0.2), (float)(-Projectile.velocity.Y * 0.2), 50, new Color(), 1f);
				Main.dust[index3].velocity *= 2f;
				Main.dust[index3].scale *= 0.75f;
				Main.dust[index3].noGravity = true;
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			Projectile.Kill();
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.Kill();
			return true;
		}

		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item54, Projectile.position);
			for (int index1 = 0; index1 < 10; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Water_Space, 0.0f, 0.0f, 50, new Color(), 3.5f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 2f;
				int index3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Water_Space, 0.0f, 0.0f, 50, new Color(), 1.5f);
				Main.dust[index3].velocity *= 2f;
			}
		}
	}
}
