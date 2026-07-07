using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.___PreHardmode.NPCs.__BossDesertDjinn
{
    public class DesertDjinn_Djinnado : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Djinnado");
		}

		public override void SetDefaults()
		{
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = ProjAIStyleID.AncientStorm;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1200;
        }

        public override void AI()
        {
            if (Projectile.localAI[0] == 1f)
            {
                Projectile.alpha += 10;
                if (Projectile.alpha >= 255)
                {
                    Projectile.Kill();
                }
            }
            else
            {
                Projectile.alpha -= 10;
                if (Projectile.alpha <= 0)
                {
                    Projectile.localAI[0] = 1f;
                }
            }
        }
    }
}
