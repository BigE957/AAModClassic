using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class TrueManaShot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.LightBeam);
            Projectile.penetrate = 1;  
            Projectile.width = 18;
            Projectile.height = 18;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.timeLeft = 900;
        }
		
		public override void AI()
		{
			if (Main.rand.NextFloat() < 0.9210526f)
			{
				Dust dust;
				Vector2 position = Projectile.position;
                dust = Main.dust[Dust.NewDust(position, 0, 0, 27, 4.736842f, 0f, 46, new Color(255, 0, 100), 1.184211f)];
                dust.fadeIn = 0.9868421f;
                dust.noGravity = true;
			}
		}

		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Mana Petal");
		}


    }
}
