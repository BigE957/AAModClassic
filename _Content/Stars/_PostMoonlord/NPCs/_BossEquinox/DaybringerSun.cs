using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.NPCs._BossEquinox
{
    public class DaybringerSun : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Daybringer Sun");
		}

        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.hostile = true;
            Projectile.scale = 1f;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
			Projectile.timeLeft = 400;
        }

        public override void AI()
        {
            if (Projectile.timeLeft <= 0)
            {
                Projectile.Kill();
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
            
            const int aislotHomingCooldown = 0;
            const int homingDelay = 15;
            const float desiredFlySpeedInPixelsPerFrame = 12f;
            const float amountOfFramesToLerpBy = 30; // minimum of 1, please keep in full numbers even though it's a float!

            Projectile.ai[aislotHomingCooldown]++;
            if (Projectile.ai[aislotHomingCooldown] > homingDelay)
            {
                Projectile.ai[aislotHomingCooldown] = homingDelay;

                int foundTarget = HomeOnTarget();
                if(Projectile.ai[1] == 0)
                {
                    if (foundTarget != -1)
                    {
                        Player target = Main.player[foundTarget];
                        Vector2 desiredVelocity = Projectile.DirectionTo(target.Center) * desiredFlySpeedInPixelsPerFrame;
                        Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                    }
                }
                else if(Projectile.ai[1] == 1)
                {
                    if (foundTarget != -1)
                    {
                        NPC n = Main.npc[foundTarget];
                        Vector2 desiredVelocity = Projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
                        Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                    }
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(250, 244, 171, 200);
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 3000;
            
            int selectedTarget = -1;

            if(Projectile.ai[1] == 0)
            {
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
            }
            else if(Projectile.ai[1] == 1)
            {
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
            }
            

            return selectedTarget;
        }

        public override void OnKill(int timeLeft)
        {
            for(int i = 0; i < 16; i++)
            {
                Vector2 shoot = new Vector2((float)Math.Sin(i * 0.125f * (float)Math.PI), (float)Math.Cos(i * 0.125f * (float)Math.PI));
                shoot *= 12f;
                int ball = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, shoot.X, shoot.Y, ProjectileID.Fireball, Projectile.damage, 5, Main.myPlayer);
                Main.projectile[ball].timeLeft = 1500;
                Main.projectile[ball].tileCollide = false;
            }
            Projectile.active = false;
        }
    }
}