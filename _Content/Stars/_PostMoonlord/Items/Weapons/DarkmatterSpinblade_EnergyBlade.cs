using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Weapons
{
    public class DarkmatterSpinblade_EnergyBlade : ModProjectile
      {
	  public override void SetStaticDefaults() 
           {
	     ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;    //The length of old position to be recorded
             ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode        
           }

        public override void SetDefaults()
         {
	    Projectile.aiStyle = -1;
            Projectile.width = 38;
            Projectile.height = 60;
            Projectile.aiStyle = ProjAIStyleID.Beam;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 240;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.alpha = 254;
            Projectile.extraUpdates = 1;
         }

        public override void AI()
        {
           Projectile.rotation = (Projectile.position.X + Projectile.position.Y / 4) * 0.0150f;
           Lighting.AddLight(Projectile.Center, (0 - Projectile.alpha) * 1f / 100f, (64 - Projectile.alpha) * 1f / 100f, (45 - Projectile.alpha) * 1f / 100f);

            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 5;
            }
            const int aislotHomingCooldown = 0;
            const int homingDelay = 10;
            const float desiredFlySpeedInPixelsPerFrame = 60;
            const float amountOfFramesToLerpBy = 20; 

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
          public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
          {
                int num580 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.DarkmatterDust>(), -Projectile.velocity.X * 0.6f, -Projectile.velocity.Y * 0.6f, 100, default, 2f);
                Main.dust[num580].noGravity = true;
                Main.dust[num580].velocity *= 1.5f;
                num580 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.DarkmatterDust>(), -Projectile.velocity.X * 0.6f, -Projectile.velocity.Y * 0.6f, 100);
                Main.dust[num580].velocity *= 1.5f;
          }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 500;

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