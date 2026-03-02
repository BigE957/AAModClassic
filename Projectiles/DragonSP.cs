using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class DragonSP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.penetrate = -1;                       //this is the projectile penetration
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
        }

		public override void SetStaticDefaults()
		{
		  // DisplayName.SetDefault("DSP");
		}

 
        public override void AI()
        {
                                                          //this make that the projectile faces the right way
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;
            Projectile.alpha = (int)Projectile.localAI[0] * 2;
           
            if (Projectile.localAI[0] > 130f) //projectile time left before disappears
            {
                Projectile.Kill();
            }
           
        }

        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 6, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 0, new Color(50, 200, 0), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 6, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 0, new Color(50, 200, 0), 1f);
                Main.dust[num469].velocity *= 2f;
            }
        }
    }
}
