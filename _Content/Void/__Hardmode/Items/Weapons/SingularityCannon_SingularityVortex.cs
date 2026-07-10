using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;

namespace AAModClassic._Content.Void.__Hardmode.Items.Weapons
{
    public class SingularityCannon_SingularityVortex : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Singularity Vortex");
        }

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 50;
            Projectile.alpha = 130;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
        }

        private float RingRotation = 0f;

        public override void AI()
        {
            RingRotation += 0.03f;

            if (Projectile.alpha > 80)
            {
                Projectile.alpha -= 3;
            }
            else
            {
                Projectile.alpha = 80;
            }
            

            if (Projectile.timeLeft < 60)
            {
                Projectile.scale -= .1f;
                if (Projectile.scale <= 0f)
                {
                    Projectile.active = false;
                }
            }
            else
            {
                if (Projectile.penetrate > 0)
                {
                    Projectile.scale = Projectile.penetrate / 50;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D Tex = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D Vortex = ModContent.Request<Texture2D>(Texture + "Back").Value;
            Rectangle frame = new Rectangle(0, 0, Tex.Width, Tex.Height);
            BaseDrawing.DrawTexture(Main.spriteBatch, Vortex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, RingRotation, 0, 1, frame, Projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            BaseDrawing.DrawTexture(Main.spriteBatch, Tex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, -RingRotation, 0, 1, frame, Projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            return false;
        }
    }
}
