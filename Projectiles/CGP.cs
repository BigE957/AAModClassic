using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class CGP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.penetrate = 1;
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 900;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, (255 - Projectile.alpha) * 0.1f / 255f, (255 - Projectile.alpha) * 0.5f / 255f, (255 - Projectile.alpha) * 0f / 255f);
            if (Main.rand.NextFloat() < 1f)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 107, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
            Projectile.rotation += Projectile.direction * 0.4f;
            Projectile.spriteDirection = Projectile.direction;
        }

        public override void SetStaticDefaults()
        {
          // DisplayName.SetDefault("CGP");
        }


    }
}
