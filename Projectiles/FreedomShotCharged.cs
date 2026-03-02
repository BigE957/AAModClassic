using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace AAMod.Projectiles
{
    public class FreedomShotCharged : ModProjectile
    {
        private bool firstHit = true;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Freedom Charged Shot");
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
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, 74,
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

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (firstHit && Projectile.owner == Main.myPlayer)
            {
                Projectile.NewProjectile(Projectile.Center, new Vector2(0, 0), Mod.Find<ModProjectile>("FreedomBall").Type, Projectile.damage, 0f, Projectile.owner);
                firstHit = false;
            }
        }

        public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);
			int p = Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("DummyExplosionTerra").Type, Projectile.damage, 0, Main.myPlayer);
			Main.projectile[p].magic = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
			Main.projectile[p].DamageType = DamageClass.Ranged;
			for (int index1 = 0; index1 < 30; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 74, 0.0f, 0.0f, 100, new Color(), 1f);
				Main.dust[index2].velocity *= 1.1f;
				Main.dust[index2].scale *= 0.99f;
			}
		}
    }
}
