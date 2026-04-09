using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.FeudalFungus
{
    public class FungusCloud : ModProjectile
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
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 1;
            Projectile.scale = 1.1f;
            Projectile.aiStyle = -1;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            if(Projectile.ai[1] == 1f)
            {
                return AAColor.Glow;
            }
            return base.GetAlpha(lightColor);
        }

        public override void AI()
        {
            if(Projectile.ai[1] == 1f)
            {
                Projectile.velocity *= 0.98f;
                Projectile.alpha += 2;
                if (Projectile.alpha > 255)
                {
                    Projectile.Kill();
                }
            }
            else
            {
                if(Projectile.timeLeft < 120)
                {
                    Projectile.alpha += 2;
                    if (Projectile.alpha > 255)
                    {
                        Projectile.Kill();
                    }
                }
                if(Projectile.ai[0] ++ < 50)
                {
                    Projectile.alpha -= 5;
                }
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