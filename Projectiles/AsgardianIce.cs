using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Projectiles
{
    public class AsgardianIce : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Icesickle");
            Main.projFrames[Projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.FrostArrow);
            Projectile.hostile = false;
            Projectile.friendly = true;
			Projectile.penetrate = 5;
		}
        public override void PostAI()
        {

            if (Projectile.frameCounter++ > 6)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 4)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 5;
            target.AddBuff(BuffID.Frostburn, 120);
        }
    }
}