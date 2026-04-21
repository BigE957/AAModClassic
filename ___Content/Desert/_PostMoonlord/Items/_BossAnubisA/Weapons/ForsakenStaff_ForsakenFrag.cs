using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Anubis.Forsaken
{
	public class ForsakenStaff_ForsakenFrag : ModProjectile
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
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 3;
			Projectile.timeLeft = 600;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
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
			Projectile.velocity.Y += Projectile.ai[0];
			if (Main.rand.NextBool(2)) {
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Dusts.JudgementDust>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity) {
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0) {
				Projectile.Kill();
			}
			else {
				if (Projectile.velocity.X != oldVelocity.X) {
					Projectile.velocity.X = -oldVelocity.X;
				}
				if (Projectile.velocity.Y != oldVelocity.Y) {
					Projectile.velocity.Y = -oldVelocity.Y;
				}
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			}
			return false;
		}

		public override void OnKill(int timeLeft) {
			for (int k = 0; k < 5; k++) {
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Dusts.JudgementDust>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
			}
			SoundEngine.PlaySound(SoundID.Item25, Projectile.position);
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0) {
				Projectile.Kill();
			}
		}
	}
}