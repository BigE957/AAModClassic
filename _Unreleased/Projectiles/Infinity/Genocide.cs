using AAModClassic.Dusts;
using AAModClassic._Unreleased.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Projectiles.Infinity
{
    public class Genocide : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 1000;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }

		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Antimatter");
		}

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Projectile.damage = (int)(Projectile.damage * 2);
        }

        public override void AI()
        {
            for (int num447 = 0; num447 < 4; num447++)
            {
                Vector2 vector33 = Projectile.position;
                vector33 -= Projectile.velocity * (num447 * 0.25f);
                Projectile.alpha = 255;
                int num448 = Dust.NewDust(vector33, Projectile.width, Projectile.height, ModContent.DustType<VoidDust_Unreleased>(), 0f, 0f, 200, default(Color), 1f); //Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.VoidDust>(), 0f, 0f, 200, default(Color), 1f);;
                Main.dust[num448].position = vector33;
                Main.dust[num448].scale = Main.rand.Next(70, 110) * 0.013f;
                Main.dust[num448].velocity *= 0.2f;
                Main.dust[num448].noGravity = true;
            }
            const int aislotHomingCooldown = 10;
            const int homingDelay = 10;
            const float desiredFlySpeedInPixelsPerFrame = 60;
            const float amountOfFramesToLerpBy = 20; // minimum of 1, please keep in full numbers even though it's a float!

            Projectile.ai[aislotHomingCooldown]++;
            if (Projectile.ai[aislotHomingCooldown] > homingDelay)
            {
                Projectile.ai[aislotHomingCooldown] = homingDelay; //cap this value 

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
                            Projectile.Distance(Main.npc[selectedTarget].Center) > distance) //or we are closer to this target than the already selected target
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }
    }
}
