using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Anubis.Forsaken
{
	public class ForsakenSand : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults() 
        {
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 120;
		}

		public override void AI() 
        {
            if (Projectile.frameCounter++ > 4)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
			if (Projectile.timeLeft >= 119) Projectile.ai[0] += 0.1f;
			if (Projectile.timeLeft >= 115) Projectile.friendly = false;
			if (Projectile.timeLeft <= 114) Projectile.friendly = true;
			Projectile.velocity.Y += Projectile.ai[0];
			if (Main.rand.NextBool(2)) 
            {
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<ForsakenDust>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity) {
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0) 
            {
				Projectile.Kill();
			}
			else 
            {
				Projectile.ai[0] += 0.1f;
				if (Projectile.velocity.X != oldVelocity.X) {
					Projectile.velocity.X = -oldVelocity.X;
				}
				if (Projectile.velocity.Y != oldVelocity.Y) {
					Projectile.velocity.Y = -oldVelocity.Y;
				}
				Projectile.velocity *= 0.9f;
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			}
			return false;
		}

		public override void OnKill(int timeLeft) 
        {
			for (int k = 0; k < 5; k++) 
            {
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Sandnado, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
			}
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position, Vector2.Zero, ModContent.ProjectileType<ForsakenBoom>(), Projectile.damage, Projectile.knockBack * 2, Main.myPlayer);
			SoundEngine.PlaySound(SoundID.Item25, Projectile.position);
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Projectile.ai[0] += 0.1f;
			Projectile.Kill();
		}
	}
}