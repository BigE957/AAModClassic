using AAModClassic.Assets;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.___PreHardmode.Items.Weapons
{
    class DynaskullJavelin_DynaEnergy : ModProjectile
    {
        public override string Texture => AssetDirectory.General.Nothing;
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 900;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            for (int num572 = 0; num572 < 5; num572++)
            {
                float num573 = Projectile.velocity.X * 0.2f * num572;
                float num574 = -(Projectile.velocity.Y * 0.2f) * num572;
                int num575 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.InfinityOverloadO>(), 0f, 0f, 100, default, 1.3f);
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
            const float amountOfFramesToLerpBy = 30; // minimum of 1, please keep in full numbers even though it's a float!

            Projectile.ai[aislotHomingCooldown]++;
            if (Projectile.ai[aislotHomingCooldown] > homingDelay)
            {
                Projectile.ai[aislotHomingCooldown] = homingDelay; 

                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    NPC n = Main.npc[foundTarget];
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

        public override void OnKill(int timeleft)
        {
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.InfinityOverloadO>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.InfinityOverloadO>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
        }
    }
}