using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class ElectricitySpellP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 11;
            Projectile.friendly = true;
            Projectile.penetrate = -1;                       //this is the projectile penetration
            Main.projFrames[Projectile.type] = 4;           //this is projectile frames
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("ESP");
    }

 
        public override void AI()
        {
                                                          //this make that the projectile faces the right way
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;
            Projectile.alpha = (int)Projectile.localAI[0] * 2;
           
            if (Projectile.localAI[0] > 200f) //projectile time left before disappears
            {
                Projectile.Kill();
            }
           
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 10)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3) 
                    Projectile.frame = 0; 
            }
            return true;
        }
    }
}
