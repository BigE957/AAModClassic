using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard._PostMoonlord.NPCs._BossGreedA
{
    public class CovStalactitePro : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Covitite Stalactites");
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 34;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.alpha = 0;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                int changeChoice = Main.rand.Next(3);
                if (changeChoice == 0)
                {
                    Projectile.frame = 0;
                }
                if (changeChoice == 1)
                {
                    Projectile.frame = 1;
                }
                if (changeChoice == 2)
                {
                    Projectile.frame = 2;
                }
                Projectile.ai[0] = 1;
            }
        }
    }
}