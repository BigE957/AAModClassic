using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class PoniumStaff_PonyShot : ModProjectile
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Pony Shot");
        }

        bool NoScythes = false;

        public override void SetDefaults()
        {
            Projectile.width = 5;
            Projectile.height = 5;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
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
            const int homingDelay = 30;
            const float desiredFlySpeedInPixelsPerFrame = 20;
            const float amountOfFramesToLerpBy = 10; // minimum of 1, please keep in full numbers even though it's a float!
            for (int num468 = 0; num468 < 20; num468++)
            {
                float Eggroll = Math.Abs(Main.GameUpdateCount) / 8f;
                float Pie = 1f * (float)Math.Sin(Eggroll);
                int num469 = Dust.NewDust(Projectile.Center, 0, 0, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 0, Main.DiscoColor, 1f);
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
                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<PoniumStaff_Ponysplosion>(), Projectile.damage, 0, Projectile.owner, 0f, 0f);
            }
        }
    }
}