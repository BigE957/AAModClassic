using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.NPCs.Friendly
{
    public class Mushman_Throwshroom : ModProjectile
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
