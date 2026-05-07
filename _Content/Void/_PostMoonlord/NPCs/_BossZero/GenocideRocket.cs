using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.NPCs._BossZero
{
    public class GenocideRocket : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Genocide Rocket");

            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 240;
            Projectile.tileCollide = true;
            Projectile.aiStyle = 0;
        }


        public override void AI()
        {
            if (Projectile.timeLeft > 0)
            {
                Projectile.timeLeft--;
            }
            if (Projectile.timeLeft == 0)
            {
                Projectile.Kill();
            }

            Projectile.frameCounter++;
            if (Projectile.frameCounter > 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 2)
                {
                    Projectile.frame = 0;
                }
            }
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            const int aislotHomingCooldown = 0;
            const int homingDelay = 0;
            const float desiredFlySpeedInPixelsPerFrame = 10;
            const float amountOfFramesToLerpBy = 20; // minimum of 1, please keep in full numbers even though it's a float!

            Projectile.ai[aislotHomingCooldown]++;
            if (Projectile.ai[aislotHomingCooldown] > homingDelay)
            {
                Projectile.ai[aislotHomingCooldown] = homingDelay; 

                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    Player target = Main.player[foundTarget];
                    Vector2 desiredVelocity = Projectile.DirectionTo(target.Center) * desiredFlySpeedInPixelsPerFrame;
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                }
            }
            for (int num230 = 0; num230 < 2; num230++)
            {
                float num231 = 0f;
                float num232 = 0f;
                if (num230 == 1)
                {
                    num231 = Projectile.velocity.X * 0.5f;
                    num232 = Projectile.velocity.Y * 0.5f;
                }
                if (Projectile.localAI[1] > 9f)
                {
                    if (Main.rand.NextBool(2))
                    {
                        int num233 = Dust.NewDust(new Vector2(Projectile.position.X + 3f + num231, Projectile.position.Y + 3f + num232) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, ModContent.DustType<Dusts.VoidDust>(), 0f, 0f, 100, default, 1f);
                        Main.dust[num233].scale *= 1.4f + Main.rand.Next(10) * 0.1f;
                        Main.dust[num233].velocity *= 0.2f;
                        Main.dust[num233].noGravity = true;
                    }
                    if (Main.rand.NextBool(2))
                    {
                        int num234 = Dust.NewDust(new Vector2(Projectile.position.X + 3f + num231, Projectile.position.Y + 3f + num232) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, ModContent.DustType<Dusts.VoidDust>(), 0f, 0f, 100, default, 0.5f);
                        Main.dust[num234].fadeIn = 0.5f + Main.rand.Next(5) * 0.1f;
                        Main.dust[num234].velocity *= 0.05f;
                    }
                }
            }
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
                            selectedTarget == -1 || //there is no selected target
                            Projectile.Distance(Main.npc[selectedTarget].Center) > distance) 
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                for (int m = 0; m < 6; m++)
                {
                    int dustID = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.VoidDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100);
                    Main.dust[dustID].noGravity = true;
                    Main.dust[dustID].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
                }
                SoundEngine.PlaySound(SoundID.NPCDeath3, Projectile.Center);
            }

            SoundEngine.PlaySound(SoundID.Item14);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<GenocideBoom>(), Projectile.damage, 1, Projectile.owner);
        }


    }
}
