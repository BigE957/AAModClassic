using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero
{
    class ZeroVoidStar_VoidStorm : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            // DisplayName.SetDefault("Void Storm");
        }

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 180;
            Projectile.aiStyle = -1;
            Projectile.alpha = 0;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Projectile.rotation += 0.03f;

            if (Projectile.alpha > 30)
            {
                Projectile.alpha -= 3;
            }
            else
            {
                Projectile.alpha = 30;
            }

            const int aislotHomingCooldown = 0;
            const int homingDelay = 30;
            const float desiredFlySpeedInPixelsPerFrame = 10;
            const float amountOfFramesToLerpBy = 20; // minimum of 1, please keep in full numbers even though it's a float!

            Projectile.ai[aislotHomingCooldown]++;
            if (Projectile.ai[aislotHomingCooldown] > homingDelay)
            {
                Projectile.ai[aislotHomingCooldown] = homingDelay;

                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    Player n = Main.player[foundTarget];
                    Vector2 desiredVelocity = Projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                }
            }
        }

        private int HomeOnTarget()
        {
            const float homingMaximumRangeInPixels = 400;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player n = Main.player[i];
                float distance = Projectile.Distance(n.Center);
                if (distance <= homingMaximumRangeInPixels &&
                (
                    selectedTarget == -1 || Projectile.Distance(Main.player[selectedTarget].Center) > distance)
                )
                    selectedTarget = i;
            }

            return selectedTarget;
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_Death(), (int)Projectile.Center.X, (int)Projectile.Center.Y, 0, 0, ModContent.ProjectileType<ZeroVoidStar_VoidCyclone>(), Projectile.damage / 4, 0, Main.myPlayer);
        }
    }
}
