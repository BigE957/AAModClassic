using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class GhastSkull : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 30;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.timeLeft = 120;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.usesLocalNPCImmunity = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Color.White.R, Color.White.G, Color.White.B, 100);
        }

        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
            if (Projectile.velocity.X < 0f)
            {
                Projectile.spriteDirection = -1;
                Projectile.rotation = (float)Math.Atan2(-Projectile.velocity.Y, -Projectile.velocity.X);
            }
            else
            {
                Projectile.spriteDirection = 1;
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
            }
            if (Projectile.alpha <= 0)
            {
                for (int num107 = 0; num107 < 3; num107++)
                {
                    int num108 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 0);
                    Main.dust[num108].noGravity = true;
                    Main.dust[num108].velocity *= 0.3f;
                    Main.dust[num108].noLight = true;
                }
            }
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 55;
                Projectile.scale = 1.3f;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                    float num109 = 16f;
                    int num110 = 0;
                    while (num110 < num109)
                    {
                        Vector2 vector14 = Vector2.UnitX * 0f;
                        vector14 += -Vector2.UnitY.RotatedBy(num110 * (6.28318548f / num109)) * new Vector2(1f, 4f);
                        vector14 = vector14.RotatedBy(Projectile.velocity.ToRotation());
                        int num111 = Dust.NewDust(Projectile.Center, 0, 0, DustID.DungeonSpirit, 0f, 0f, 0);
                        Main.dust[num111].scale = 1.5f;
                        Main.dust[num111].noLight = true;
                        Main.dust[num111].noGravity = true;
                        Main.dust[num111].position = Projectile.Center + vector14;
                        Main.dust[num111].velocity = (Main.dust[num111].velocity * 4f) + (Projectile.velocity * 0.3f);
                        num110++;
                    }
                }
            }
            const int aislotHomingCooldown = 0;
            const int homingDelay = 20;
            const float desiredFlySpeedInPixelsPerFrame = 30;
            const float amountOfFramesToLerpBy = 20; // minimum of 1, please keep in full numbers even though it's a float!

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

        public override void OnKill(int timeLeft)
        {
			Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, 0, Mod.Find<ModProjectile>("GhastBoom").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
            Projectile.position = Projectile.Center;
            Projectile.width = Projectile.height = 160;
            Projectile.Center = Projectile.position;
            Projectile.maxPenetrate = -1;
            Projectile.penetrate = -1;
            Projectile.Damage();
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            SoundEngine.PlaySound(SoundID.NPCDeath39, Projectile.position);
            Vector2 position = Projectile.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 4; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, DustID.DungeonSpirit, 0f, 0f, 100);
                Main.dust[num86].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 20; num87++)
            {
                int num794 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 0);
                Main.dust[num794].velocity *= 0.1f;
                Main.dust[num794].scale = 1.3f;
                Main.dust[num794].noGravity = true;
                Main.dust[num794].velocity += Projectile.DirectionTo(Main.dust[num794].position) * (2f + (Main.rand.NextFloat() * 4f));
                num794 = Dust.NewDust(position, num84, height3, DustID.RedTorch, 0f, 0f, 100);
                Main.dust[num794].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num794].velocity *= 2f;
                Main.dust[num794].noGravity = true;
                Main.dust[num794].fadeIn = 1f;
                Main.dust[num794].color = Color.Crimson * 0.5f;
                Main.dust[num794].noLight = true;
                Main.dust[num794].velocity += Projectile.DirectionTo(Main.dust[num794].position) * 8f;
            }
            for (int num89 = 0; num89 < 20; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, DustID.DungeonSpirit, 0f, 0f, 0);
                Main.dust[num90].position = Projectile.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(Projectile.velocity.ToRotation()) * num84 / 2f);
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += Projectile.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 70; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, DustID.DungeonSpirit, 0f, 0f, 0);
                Main.dust[num92].position = Projectile.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(Projectile.velocity.ToRotation()) * num84 / 2f);
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += Projectile.DirectionTo(Main.dust[num92].position) * 3f;
            }
        }
    }
}