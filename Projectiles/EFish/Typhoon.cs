using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.EFish
{
    public class Typhoon : ModProjectile
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ultiblade Typhoon");

            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Typhoon);
        }
		
		
    }
}