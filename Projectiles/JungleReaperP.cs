using System;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class JungleReaperP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.penetrate = -1;                       //this is the projectile penetration
            Main.projFrames[Projectile.type] = 1;           //this is projectile frames
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("JungleReaperP");
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
            for (int num468 = 0; num468 < 5; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.JungleGrass, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 0, new Color(255, 255, 255), 2.105263f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.JungleGrass, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 0, new Color(255, 255, 255), 2.105263f);
                Main.dust[num469].velocity *= 2f;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, Color.White, true);
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 10)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3) 
                    Projectile.frame = 0; 
            }
            return false;
        }
    }
}
