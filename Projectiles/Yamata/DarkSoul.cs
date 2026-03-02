using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Yamata
{
    public class DarkSoul : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dark Soul");
        }

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = ProjAIStyleID.SpectreWrath;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 3;
            Projectile.timeLeft = 120 * 3;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(Mod.Find<ModBuff>("Moonraze").Type, 600);
        }

        NPC n = null;
        public override void AI()
        {
            if (n == null)
            {
                Projectile.timeLeft--;
                if (Projectile.timeLeft <= 0)
                {
                    Projectile.Kill();
                }
            }
            for (int num572 = 0; num572 < 5; num572++)
            {
                float num573 = Projectile.velocity.X * 0.2f * num572;
                float num574 = -(Projectile.velocity.Y * 0.2f) * num572;
                int num575 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.YamataDustLight>(), 0f, 0f, 100, default, 1.3f);
                Main.dust[num575].noGravity = true;
                Main.dust[num575].velocity *= 0f;
                Dust expr_178B4_cp_0 = Main.dust[num575];
                expr_178B4_cp_0.position.X -= num573;
                Dust expr_178D3_cp_0 = Main.dust[num575];
                expr_178D3_cp_0.position.Y -= num574;
            }
            const int aislotHomingCooldown = 0;
            const int homingDelay = 20;
            const float desiredFlySpeedInPixelsPerFrame = 15;
            const float amountOfFramesToLerpBy = 20; // minimum of 1, please keep in full numbers even though it's a float!

            Projectile.ai[aislotHomingCooldown]++;

            if (Projectile.ai[aislotHomingCooldown] > homingDelay)
            {
                Projectile.ai[aislotHomingCooldown] = homingDelay; 

                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    n = Main.npc[foundTarget];
                    Vector2 desiredVelocity = Projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                }
            }
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 400;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(Projectile) && (!n.wet || homingCanAimAtWetEnemies))
                {
                    float distance = Projectile.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            Projectile.Distance(Main.npc[selectedTarget].Center) > distance) 
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }
    }
}
