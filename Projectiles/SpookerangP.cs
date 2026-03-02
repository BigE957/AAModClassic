using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class SpookerangP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.PossessedHatchet);
            Projectile.penetrate = 6;  
            Projectile.width = 32;
            Projectile.height = 32;
            AIType = ProjectileID.PossessedHatchet;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("SpookerangP");
        }


    }
}
