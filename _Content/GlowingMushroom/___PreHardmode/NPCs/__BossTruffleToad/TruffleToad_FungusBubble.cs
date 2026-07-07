using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossTruffleToad
{
    public class TruffleToad_FungusBubble : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fungus Bubble");
		}
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 0;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 300;
            Projectile.noEnchantments = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            const int homingDelay = 45;
            const float desiredFlySpeedInPixelsPerFrame = 3;
            const float amountOfFramesToLerpBy = 30;

            Projectile.ai[0]++;
            if (Projectile.ai[0] > homingDelay)
            {
                Projectile.ai[0] = homingDelay;

                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    Player target = Main.player[foundTarget];
                    Vector2 desiredVelocity = Projectile.DirectionTo(target.Center) * desiredFlySpeedInPixelsPerFrame;
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                }
            }
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 500;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                Player target = Main.player[i];
                if (target.active && (!target.wet || homingCanAimAtWetEnemies))
                {
                    float distance = Projectile.Distance(target.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                    (
                        selectedTarget == -1 || Projectile.Distance(Main.player[selectedTarget].Center) > distance)
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
            target.AddBuff(ModContent.BuffType<Shroomed_Buff>(), 180);
        }

        public override void OnKill(int timeLeft)
        {
            for (int dust = 0; dust <= 5; dust++)
            {
                int dustType = ModContent.DustType<Dusts.ShroomDust>();
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, dustType, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 30;
            height = 30;
            return true;
        }

		public override bool OnTileCollide (Vector2 oldVelocity)
		{
			Projectile.ai[0] = 1f;
			return false;
		}
    }
}