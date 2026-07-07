using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Weapons
{
    public class ChaosBaton_BlazeBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blaze Blast");     
            Main.projFrames[Projectile.type] = 6;     
        }

        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 102;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 6)
                {
                    Projectile.Kill();

                }
            }
            Projectile.velocity.X *= 0.00f;
            Projectile.velocity.Y *= 0.00f;

        }

        public override void OnKill(int timeLeft)
        {
            Projectile.timeLeft = 0;
        }

    }
}
