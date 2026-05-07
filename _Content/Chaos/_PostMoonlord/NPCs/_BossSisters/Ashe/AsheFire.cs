using AAModClassic._Content.Inferno.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs._BossSisters.Ashe
{
    internal class AsheFire : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fire Bomb");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.scale = 1.1f;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 60;
            Projectile.timeLeft = 180;
            Projectile.extraUpdates = 1;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Color.White.R, Color.White.G, Color.White.B, Projectile.alpha);
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
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
            if (Projectile.frameCounter > 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
            const int aislotHomingCooldown = 0;
            const int homingDelay = 0;
            const float desiredFlySpeedInPixelsPerFrame = 12;
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


        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 500;
            
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
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<DragonFire_Buff>(), 600);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item124);
            int id = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center - new Vector2(0, 95), new Vector2(0, 0), ModContent.ProjectileType<AsheStrike>(), Projectile.damage, 5);
            if(Projectile.ai[1] == 1)
            {
                Main.projectile[id].hostile = false;
                Main.projectile[id].friendly = true;
            }
            Projectile.active = false;
        }
    }
}