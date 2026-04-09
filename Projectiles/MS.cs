using System;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class MS : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 70;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                     
            Main.projFrames[Projectile.type] = 1;           
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        
            Projectile.tileCollide = false;                 
            Projectile.ignoreWater = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Darkmatter Wave");
        }

 
        public override void AI()
        {
                                            
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;
            Projectile.alpha = (int)Projectile.localAI[0] * 2;
           
            if (Projectile.localAI[0] > 130f) 
            {
                Projectile.Kill();
            }

        }

        public override void OnKill(int timeleft)
        {
            Projectile.NewProjectile(Projectile.GetSource_Death(), (int)Projectile.position.X, (int)Projectile.position.Y, 0, 0, ModContent.ProjectileType<MSRT>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
            for (int num468 = 0; num468 < 5; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.DarkmatterDust>(), -Projectile.velocity.X * 0.6f, -Projectile.velocity.Y * 0.6f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 1.5f;
                num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.DarkmatterDust>(), -Projectile.velocity.X * 0.6f, -Projectile.velocity.Y * 0.6f, 100);
                Main.dust[num469].velocity *= 1.5f;
            }
        }

          public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
          {
                int num580 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.DarkmatterDust>(), -Projectile.velocity.X * 0.6f, -Projectile.velocity.Y * 0.6f, 100, default, 2f);
                Main.dust[num580].noGravity = true;
                Main.dust[num580].velocity *= 1.5f;
                num580 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.DarkmatterDust>(), -Projectile.velocity.X * 0.6f, -Projectile.velocity.Y * 0.6f, 100);
                Main.dust[num580].velocity *= 1.5f;
          }
        public override bool PreDraw(ref Color lightColor)
        {
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, Color.White, true);
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
