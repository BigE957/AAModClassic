using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire.___PreHardmode.NPCs.__BossHydra
{
    public class HydraHead_HydraBomb : ModProjectile
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hydra Bomb");     
            Main.projFrames[Projectile.type] = 5;     
		}

		public override void SetDefaults()
		{
			Projectile.width = 14;               
			Projectile.height = 14;              
			Projectile.aiStyle = ProjAIStyleID.Arrow;             
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
            target.AddBuff(BuffID.Poisoned, 600);
        }

        public override void OnKill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AcidDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AcidDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, -Projectile.oldVelocity * 0.2f, 704, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, -Projectile.oldVelocity * 0.2f, 705, 1f);
            if (Projectile.owner == Main.myPlayer)
            {
                int num319 = Main.rand.Next(20, 31);
                for (int num320 = 0; num320 < num319; num320++)
                {
                    Vector2 value21 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                    value21.Normalize();
                    value21 *= Main.rand.Next(10, 201) * 0.01f;
                    int a = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, value21.X, value21.Y, ModContent.ProjectileType<HydraHead_HydraMist>(), Projectile.damage, 1f, Projectile.owner, 0f, Main.rand.Next(-45, 1));
                    Main.projectile[a].localAI[0] = Main.rand.Next(3);
                }
            }
        }
    }
}
