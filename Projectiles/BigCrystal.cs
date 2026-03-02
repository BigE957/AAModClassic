using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class BigCrystal : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BoulderStaffOfEarth);
            Projectile.penetrate = -1;  
            Projectile.width = 44;
            Projectile.height = 44;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.timeLeft = 900;
            Projectile.DamageType = DamageClass.Magic;
        }

		public override void SetStaticDefaults()
		{
		// DisplayName.SetDefault("Crystal");
		}


    }
}
