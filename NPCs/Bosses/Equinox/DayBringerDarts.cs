using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Equinox
{
    public class DayBringerDarts : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("DayBringer Darts");
		}

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.hostile = true;
            Projectile.scale = 1f;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
			Projectile.timeLeft = 180;
        }	
        public override void AI()
        {
			Projectile.rotation = Projectile.velocity.ToRotation() + 1.5707f;
            
            if (Projectile.timeLeft == 0)
            {
                Projectile.Kill();
            }

            int dustId = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, new Color(250, 244, 171), 2f);
            Main.dust[dustId].noGravity = true;
            int dustId3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, new Color(250, 244, 171), 2f);
            Main.dust[dustId3].noGravity = true;
        }

        public override void OnKill(int timeLeft)
        {
            int id = Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, 0f, 0f, 612, Projectile.damage, 10f, Projectile.owner, 0f, 0.85f + Main.rand.NextFloat() * 1.15f);
            Main.projectile[id].hostile = true;
            Main.projectile[id].friendly = false;
        }
    }
}