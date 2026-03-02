using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class CursedSickleProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cursed Sickle");
		}

		public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.alpha = 100;
            Projectile.light = 0.2f;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 5;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Melee;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 16;
            height = 16;
            return true;
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.CursedInferno, 180);
			target.immune[Projectile.owner] = 1;
			Projectile.Kill();
		}

		public override void AI()
        {
            Projectile.rotation += Projectile.direction * 0.8f;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 30f)
            {
                if (Projectile.ai[0] < 100f)
                {
                    Projectile.velocity *= 1.06f;
                }
                else
                {
                    Projectile.ai[0] = 200f;
                }
            }
            for (int num257 = 0; num257 < 2; num257++)
            {
                int num258 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 75, 0f, 0f, 100);
                Main.dust[num258].noGravity = true;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnKill(int timeLeft)
		{
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int num611 = 0; num611 < 30; num611++)
            {
                int num612 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 75, Projectile.velocity.X, Projectile.velocity.Y, 100, default, 1.7f);
                Main.dust[num612].noGravity = true;
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 75, Projectile.velocity.X, Projectile.velocity.Y, 100);
            }

        }
	}
}