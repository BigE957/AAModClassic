using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class Toxifang : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(14);
			Projectile.penetrate = 1;
			Projectile.width = 10;
			Projectile.height = 12;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
			Projectile.timeLeft = 300;
			Projectile.aiStyle = ProjAIStyleID.Arrow;
			Projectile.alpha = 0;
			AIType = ProjectileID.Bullet;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Toxifang");
		}

		public override void AI()
		{
			Projectile.alpha = 0;
			for (int index1 = 0; index1 < 2; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BlueMoss, (float)(-Projectile.velocity.X * 0.2), (float)(-Projectile.velocity.Y * 0.2), 50, new Color(), 1f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 1.5f;
				Main.dust[index2].scale *= 0.75f;
				int index3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BlueMoss, (float)(-Projectile.velocity.X * 0.2), (float)(-Projectile.velocity.Y * 0.2), 50, new Color(), 1f);
				Main.dust[index3].velocity *= 1.5f;
				Main.dust[index3].scale *= 0.75f;
				Main.dust[index3].noGravity = true;
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<Buffs.HydraToxin_Buff>(), 600);
			Projectile.Kill();
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.Kill();
			return true;
		}

		public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int index1 = 0; index1 < 5; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BlueMoss, 0.0f, 0.0f, 50, new Color(), 2.5f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 2f;
				int index3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BlueMoss, 0.0f, 0.0f, 50, new Color(), 1.25f);
				Main.dust[index3].velocity *= 2f;
			}
		}
	}
}
