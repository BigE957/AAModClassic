using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Tools
{
    public class DragonsGrip_Hook : ModProjectile
    {
        private static Asset<Texture2D> chainTexture;

        public override void Load()
        {
            chainTexture = ModContent.Request<Texture2D>(Texture + "_Chain");
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon's Grip");
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.IlluminantHook);
        }

        public override float GrappleRange()
        {
            return 480f;
        }

        public override void NumGrappleHooks(Player player, ref int numHooks)
        {
            numHooks = 3;
        }

        public override void GrappleRetreatSpeed(Player player, ref float speed)
        {
            speed = 20f;
        }

        public override void GrapplePullSpeed(Player player, ref float speed)
        {
            speed = 8;
        }

        public override bool PreDrawExtras()
        {
            Vector2 playerCenter = Main.LocalPlayer.MountedCenter;
            Vector2 center = Projectile.Center;
            Vector2 directionToPlayer = playerCenter - Projectile.Center;
            float chainRotation = directionToPlayer.ToRotation() - MathHelper.PiOver2;
            float distanceToPlayer = directionToPlayer.Length();

            while (distanceToPlayer > 20f && !float.IsNaN(distanceToPlayer))
            {
                directionToPlayer /= distanceToPlayer; // get unit vector
                directionToPlayer *= chainTexture.Height(); // multiply by chain link length

                center += directionToPlayer; // update draw position
                directionToPlayer = playerCenter - center; // update distance
                distanceToPlayer = directionToPlayer.Length();

                Color drawColor = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16));

                // Draw chain
                Main.EntitySpriteDraw(chainTexture.Value, center - Main.screenPosition,
                    chainTexture.Value.Bounds, drawColor, chainRotation,
                    chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0);
            }
            // Stop vanilla from drawing the default chain.
            return false;
        }
    }

}
