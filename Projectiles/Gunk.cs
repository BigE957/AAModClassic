using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ModLoader;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.Projectiles
{
    public class Gunk : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gunk");
        }
        public override void SetDefaults()
        {
            Projectile.penetrate = 1;  
            Projectile.width = 28;
            Projectile.height = 28;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.timeLeft = 300;
            Projectile.aiStyle = -1;
            Projectile.alpha = 70;
        }

        public override void AI()
        {
            if (Projectile.ai[0]++ > 60)
            {
                Projectile.ai[0] = 0;
                Projectile.ai[1] += 1;
                if (Projectile.ai[1] > 2)
                {
                    Projectile.ai[1] = 2;
                }
            }
            Projectile.frame = (int)Projectile.ai[1];
            if (Projectile.ai[1] == 0)
            {
                Projectile.scale = 1 / 4;
            }
            else if (Projectile.ai[1] == 0)
            {
                Projectile.scale = 1 / 2;
            }
            else
            {
                Projectile.scale = 1;
            }
        }

        public override void OnKill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AcidDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 46, new Color(0, 255, 217), 1.184211f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AcidDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 46, new Color(0, 255, 217), 1.184211f);
                Main.dust[num469].velocity *= 2f;
            }
        }


        public override bool PreDraw(ref Color lightColor)
        {
            int width = TextureAssets.Projectile[Projectile.type].Value.Width;
            int height = TextureAssets.Projectile[Projectile.type].Value.Height;

            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, width, height / 3, 0, 0);

            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, 1f, Projectile.rotation, 0, 3, frame, lightColor, true);
            return false;
        }
    }
}
