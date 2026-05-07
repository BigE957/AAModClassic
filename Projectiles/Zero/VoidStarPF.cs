using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;


namespace AAModClassic.Projectiles.Zero
{
    public class VoidStarPF : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Star Vortex");
        }

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 20;
            Projectile.scale = .01f;
            Projectile.alpha = 255;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
        }

        private float RingRotation = 0f;

        public override void AI()
        {
            RingRotation += 0.03f;

            if (Projectile.alpha > 80)
            {
                Projectile.friendly = false;
                Projectile.alpha -= 3;
            }
            else
            {
                Projectile.friendly = true;
                Projectile.alpha = 80;
            }

            Projectile.scale = Projectile.penetrate / 20;

            const int aislotHomingCooldown = 0;
            const int homingDelay = 30;
            const float desiredFlySpeedInPixelsPerFrame = 5;
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

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D Tex = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D Vortex = Mod.GetTexture("Projectiles/SingularityVortex1");
            Rectangle frame = new Rectangle(0, 0, Tex.Width, Tex.Height);
            BaseDrawing.DrawTexture(Main.spriteBatch, Vortex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, RingRotation, 0, 1, frame, Projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            BaseDrawing.DrawTexture(Main.spriteBatch, Tex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, -RingRotation, 0, 1, frame, Projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            return false;
        }
    }
}
