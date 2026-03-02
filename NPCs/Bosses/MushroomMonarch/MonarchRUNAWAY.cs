using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.MushroomMonarch
{
    public class MonarchRUNAWAY: ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mushroom Monarch");
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.damage = 24;
            Projectile.width = 74;
            Projectile.height = 80;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 900;
        }
        public override void AI()
        {
            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
                {
                    Projectile.frame = 0;
                }
            }
            Projectile.velocity.X *= 0.00f;
            Projectile.velocity.Y -= .1f;
        }
    }
}