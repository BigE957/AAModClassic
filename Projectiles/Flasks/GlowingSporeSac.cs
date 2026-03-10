using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Flasks
{
    public class GlowingSporeSac : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flask");
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.damage = 0;
            Projectile.penetrate = -1;
            Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = true;
            AIType = ProjectileID.ToxicFlask;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = Mod.GetTexture("Projectiles/Flasks/GlowingSporeSac");
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, tex.Width, tex.Height, 0, 2);
            BaseDrawing.DrawTexture(Main.spriteBatch, tex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 7, frame, lightColor, true);
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            Vector2 position = Projectile.Center;
            SoundEngine.PlaySound(SoundID.Shatter, position);

            int radius = 6;
            int FlaskDust = 113;

            for (int m = 0; m < 20; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, FlaskDust, 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / 20 * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < 20; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, FlaskDust, 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / 20 * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5)   //circle
                    {
                        WorldGen.Convert(xPosition, yPosition, 3, 1, true, true);
                    }
                }
            }
        }
    }
}