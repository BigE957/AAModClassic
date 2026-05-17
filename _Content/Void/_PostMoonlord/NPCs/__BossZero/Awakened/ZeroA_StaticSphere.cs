using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero.Awakened
{
    public class ZeroA_StaticSphere : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 320;
            Projectile.tileCollide = true;
            Projectile.aiStyle = 0;
            Projectile.damage = 22;
            Projectile.damage = 100;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Oblivion;
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
            const float amountOfFramesToLerpBy = 30; // minimum of 1, please keep in full numbers even though it's a float!

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
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 500;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player target = Main.player[i];
                if (target.active && (!target.wet || homingCanAimAtWetEnemies))
                {
                    float distance = Projectile.Distance(target.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            Projectile.Distance(Main.player[selectedTarget].Center) > distance) 
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Glitch"), Projectile.Center);
            float spread = 12f * 0.0174f;
            double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - spread / 2;
            double Angle = spread / 3f;
            double offsetAngle;
            int i;
            if (Projectile.owner == Main.myPlayer)
            {
                for (i = 0; i < 3; i++)
                {
                    offsetAngle = startAngle + Angle * (i + i * i) / 2f + 32f * i;
                    Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 2f), (float)(Math.Cos(offsetAngle) * 6f), ModContent.ProjectileType<ZeroA_StaticShock>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                    Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 2f), (float)(-Math.Cos(offsetAngle) * 6f), ModContent.ProjectileType<ZeroA_StaticShock>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                }
            }

        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 4)
                    Projectile.frame = 0;
            }
            return true;
        }
    }
}