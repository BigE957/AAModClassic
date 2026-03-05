using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class ChaosScythe : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Scythe");
        }

        bool NoScythes = false;

        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 90;
            Projectile.aiStyle = -1;
            Projectile.alpha = 255;
            Projectile.damage = 1;
        }

        public override void AI()
        {
            const int aislotHomingCooldown = 0;
            const int homingDelay = 0;
            const float desiredFlySpeedInPixelsPerFrame = 20;
            const float amountOfFramesToLerpBy = 10; // minimum of 1, please keep in full numbers even though it's a float!
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, 0, 0, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 0, AAColor.Jevil, 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].alpha = 20;
            }
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
            if (Projectile.timeLeft < 10)
            {
                NoScythes = true;
            }
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 1000;

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
            if (!NoScythes)
            {
                SoundEngine.PlaySound(SoundID.Item71, Projectile.position);
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X + 250, Projectile.Center.Y, -7, 0, Mod.Find<ModProjectile>("ChaosScytheP").Type, 250, 1, Projectile.owner, 0f, 0f);
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X - 250, Projectile.Center.Y, 7, 0, Mod.Find<ModProjectile>("ChaosScytheP").Type, 250, 1, Projectile.owner, 0f, 0f);
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y + 250, 0, -7, Mod.Find<ModProjectile>("ChaosScytheP").Type, 250, 1, Projectile.owner, 0f, 0f);
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y - 250, 0, 7, Mod.Find<ModProjectile>("ChaosScytheP").Type, 250, 1, Projectile.owner, 0f, 0f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.CursedInferno, 300);
        }
    }
}