using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Toad
{
    public class ToadBomb : ModProjectile
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Acid"); 
            Main.projFrames[Projectile.type] = 5;   
		}

		public override void SetDefaults()
		{
			Projectile.width = 14;   
			Projectile.height = 14;  
			Projectile.aiStyle = 1;            
			Projectile.friendly = false;      
			Projectile.hostile = true;    
			Projectile.penetrate = 1;     
			Projectile.timeLeft = 600;   
			Projectile.alpha = 20;    
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;      
			AIType = ProjectileID.WoodenArrowFriendly;           
            
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Shroomed>(), 200);
        }

        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.ShroomDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.ShroomDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188));
                Main.dust[num469].velocity *= 2f;
            }
            Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("ToadBoom").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
    }
}
