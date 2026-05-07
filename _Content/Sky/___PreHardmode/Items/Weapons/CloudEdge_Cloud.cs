using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Sky.___PreHardmode.Items.Weapons
{
    public class CloudEdge_Cloud : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Starfury);
            Projectile.penetrate = 14;  
            Projectile.width = 14;
            Projectile.height = 18;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cloud");
        }


    }
}
