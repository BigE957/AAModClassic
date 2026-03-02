using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class CloudEdgeP : ModProjectile
    {
        public override string Texture => "AAMod/Projectiles/Cloud";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Starfury);
            Projectile.penetrate = 14;  
            Projectile.width = 14;
            Projectile.height = 18;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("CGP");
        }


    }
}
