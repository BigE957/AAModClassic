using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Throwshroom : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Shuriken);
            Projectile.penetrate = -1;  
            Projectile.width = 20;
            Projectile.height = 22;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.timeLeft = 150;
            
        }
    }
}
