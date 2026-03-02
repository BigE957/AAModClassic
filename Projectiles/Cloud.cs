using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Cloud : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Starfury);
            Projectile.penetrate = 14;  
            Projectile.width = 14;
            Projectile.height = 18;
            Projectile.melee = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("CGP");
        }


    }
}
