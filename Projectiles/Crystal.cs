using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Crystal : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.TerraBeam);
            Projectile.penetrate = 2;  
            Projectile.width = 20;
            Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.timeLeft = 900;
            Projectile.melee = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }
		
		public override void AI()
		{
			if (Main.rand.NextFloat() < 0.9210526f)
			{
				Dust dust;
				Vector2 position = Projectile.position;
                dust = Main.dust[Dust.NewDust(position, 0, 0, DustID.Shadowflame, 4.736842f, 0f, 46, new Color(30, 30, 30), 1.184211f)];
                dust.fadeIn = 0.9868421f;
                dust.noGravity = false;
            }
		}

        public override void OnKill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Shadowflame, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 46, new Color(30, 30, 30), 1.184211f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Shadowflame, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 46, new Color(30, 30, 30), 1.184211f);
                Main.dust[num469].velocity *= 2f;
            }
        }


        public override void SetStaticDefaults()
		{
		// DisplayName.SetDefault("Crystal");
		}


    }
}
