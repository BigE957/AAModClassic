using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Monarch
{
    public class SporeCloud : ModProjectile
    {
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fungus Cloud");
            Main.projFrames[Projectile.type] = 5;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 1;
            Projectile.scale = .8f;
            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
            Projectile.velocity *= 0;
            Projectile.alpha += 3;
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