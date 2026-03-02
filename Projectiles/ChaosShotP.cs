using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using AAModClassic.Dusts;

namespace AAModClassic.Projectiles
{
    public class ChaosShotP : ModProjectile
    { 
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Beam");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.penetrate = 1;  
            Projectile.width = 30;
            Projectile.height = 30;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 600;
            Projectile.aiStyle = -1;
        }
		
		public override void AI()
		{
            for (int dust = 0; dust < 5; dust++)
            {
                int dustId = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width, Projectile.height + 5, ModContent.DustType<AkumaADust>(), Projectile.velocity.X * 0.2f,
                Projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, 0, Main.DiscoB), 1f);
                Main.dust[dustId].noGravity = true;
                int dustId3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width, Projectile.height + 5, ModContent.DustType<YamataADust>(), Projectile.velocity.X * 0.2f,
                    Projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, 0, Main.DiscoB), 1f);
                Main.dust[dustId3].noGravity = true;
            }
            
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
            
            
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 0.785f;
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            
            const int aislotHomingCooldown = 0;
            const int homingDelay = 30;
            const float desiredFlySpeedInPixelsPerFrame = 20;
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


        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<AkumaADust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, 0, Main.DiscoB), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<YamataADust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, 0, Main.DiscoB));
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<AkumaADust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, 0, Main.DiscoB), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<YamataADust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, 0, Main.DiscoB));
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<AkumaADust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, 0, Main.DiscoB), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<YamataADust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, 0, Main.DiscoB));
                Main.dust[num469].velocity *= 2f;
            }
            Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("PerfectChaosE").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
        
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(Mod.Find<ModBuff>("Moonraze").Type, 600);
            target.AddBuff(BuffID.Daybreak, 600);
        }
    }
}