using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class OrderArrow : ModProjectile
	{
		public static int defense = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Order Arrow");
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(1);
			Projectile.width = 14;
			Projectile.height = 18;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			AIType = 1;
            Projectile.arrow = true;
        }

		public override void AI()
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, 107,
				Projectile.velocity.X * .5f, Projectile.velocity.Y * .5f, 200, Scale: .6f);
				dust.velocity += Projectile.velocity * 0.4f;
				dust.velocity *= 0.3f;
			}
		}

		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers)
		{
			target.defense = target.defDefense - 30;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.immune[Projectile.owner] = 1;
			target.defense = defense;
		}

        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 107, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default, .6f);
            }
        }

    }
}