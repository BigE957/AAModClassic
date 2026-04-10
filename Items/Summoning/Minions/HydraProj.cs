using AAModClassic.___Content.Mire.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Summoning.Minions
{
    public class HydraProj : ModProjectile
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
			Projectile.aiStyle = ProjAIStyleID.Arrow;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.alpha = 20;
			Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.minion = true;
			AIType = ProjectileID.WoodenArrowFriendly;           
            
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Venom, 600);
        }

        public override void OnKill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AcidDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AcidDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188));
                Main.dust[num469].velocity *= 2f;
            }
            int id = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<AcidBoomProj>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
            Main.projectile[id].minion = true;
            Main.projectile[id].friendly = true;
            Main.projectile[id].hostile = false;
        }
    }
}
