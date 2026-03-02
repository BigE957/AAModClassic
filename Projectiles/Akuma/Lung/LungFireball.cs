using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma.Lung
{
    public class LungFireball : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fireball");
		}

		public override void SetDefaults()
		{
			Projectile.width = 10; 
			Projectile.height = 10; 
			Projectile.aiStyle = 1;   
			Projectile.friendly = true; 
			Projectile.hostile = false; 
			Projectile.DamageType = DamageClass.Ranged;   
			Projectile.penetrate = 1;  
			Projectile.timeLeft = 600;  
			Projectile.alpha = 50; 
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			AIType = ProjectileID.WoodenArrowFriendly;           
            
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 100);
        }

        public override void OnKill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(191, 86, 188), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(191, 86, 188));
                Main.dust[num469].velocity *= 2f;
            }
        }
    }
}
