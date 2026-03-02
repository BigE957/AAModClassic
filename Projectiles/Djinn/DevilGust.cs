using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Djinn
{
    public class DevilGust : ModProjectile
    {
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Desert Gust");
            Main.projFrames[Projectile.type] = 5;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -11;
            Projectile.extraUpdates = 1;
            Projectile.scale = 1.1f;
            Projectile.penetrate = -1;
            Projectile.minion = true;
        }

        public override void AI()
        {
            Projectile.velocity *= 0.98f;
            Projectile.alpha += 2;
            if (Projectile.alpha > 255)
            {
                Projectile.Kill();
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 4) 
                    Projectile.frame = 0; 
            }
            return true;
        }
    }
}