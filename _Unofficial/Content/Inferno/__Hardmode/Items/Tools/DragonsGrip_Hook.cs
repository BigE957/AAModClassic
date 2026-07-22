using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Inferno.__Hardmode.Items.Tools
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
            DrawingUtils.DrawGrapplingHookChain(Projectile, chainTexture);
            return false;
        }
    }

}
