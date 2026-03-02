using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace AAMod.Projectiles
{
    public class FreedomShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Freedom Shot");
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 2;
        }

        public override void AI()
        {
			if (Main.rand.Next(2) == 0)
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.GreenFairy,
					Projectile.velocity.X, Projectile.velocity.Y, 200, Scale: 1f);
				dust.velocity += Projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
			if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 2)
                {
                    Projectile.frame = 0;
                }
            }
            Projectile.rotation = Projectile.velocity.ToRotation(); // projectile faces sprite right
        }
		
		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);
			for (int index1 = 0; index1 < 20; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.GreenFairy, 0.0f, 0.0f, 100, new Color(), 1f);
				Main.dust[index2].velocity *= 1.1f;
				Main.dust[index2].scale *= 0.99f;
			}
		}
    }
}
