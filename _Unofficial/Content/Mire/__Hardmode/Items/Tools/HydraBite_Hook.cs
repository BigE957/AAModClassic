using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Mire.__Hardmode.Items.Tools
{
    public class HydraBite_Hook : ModProjectile
    {
        private static Asset<Texture2D> chainTexture;

        private Color GlowColor = Color.Black;

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

        public override void OnSpawn(IEntitySource source)
        {
            int color = Main.rand.Next(6);
            GlowColor = color switch
            {
                1 => Color.Orange,
                2 => Color.Purple,
                3 => Color.Blue,
                4 => Color.Yellow,
                5 => Color.Red,
                _ => Color.Green,
            };
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

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawPos = Projectile.Center - Main.screenPosition - Projectile.rotation.ToRotationVector2();
            Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos, null, lightColor, Projectile.rotation, TextureAssets.Projectile[Type].Size() * 0.5f, Projectile.scale, 0);
            Asset<Texture2D> glow = ModContent.Request<Texture2D>(Texture + "_Glow");
            Main.EntitySpriteDraw(glow.Value, drawPos, null, GlowColor, Projectile.rotation, glow.Size() * 0.5f, Projectile.scale, 0);
            return false;
        }
    }
}
