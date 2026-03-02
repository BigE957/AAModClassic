using Terraria.ModLoader;

namespace AAMod.Projectiles.Zero
{
    public class OmegaVolleyF : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = 1;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.light = 0.3f;
            Projectile.alpha = 255;
            Projectile.extraUpdates = 7;
            Projectile.scale = 1.18f;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
        }

		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Omega Shot");
		}
    }
}
