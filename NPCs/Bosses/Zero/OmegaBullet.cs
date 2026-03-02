using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero
{
    public class OmegaBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Omega Bullet");
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            Projectile.friendly = false;
            Projectile.hostile = true;
            AIType = ProjectileID.Bullet;
            Projectile.tileCollide = false;
        }

        int a = 0;

        public override void PostAI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) a++;
            if (a == 40)
            {
                Projectile.tileCollide = true;
                Projectile.netUpdate = true;
            }
            if (a < 40)
            {
                Projectile.tileCollide = false;
            }
        }
    }
}
