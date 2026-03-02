using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class OceanWhaler : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ocean Harpoon");
        }
        

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void AI()
        {
            if (Main.player[Projectile.owner].dead)
            {
                Projectile.Kill();
                return;
            }
            if (Projectile.type != ProjectileID.ChainGuillotine)
            {
                Main.player[Projectile.owner].itemAnimation = 5;
                Main.player[Projectile.owner].itemTime = 5;
            }
            if (Projectile.alpha == 0)
            {
                if (Projectile.position.X + Projectile.width / 2 > Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2)
                {
                    Main.player[Projectile.owner].ChangeDir(1);
                }
                else
                {
                    Main.player[Projectile.owner].ChangeDir(-1);
                }
            }
            Vector2 vector14 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
            float num166 = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - vector14.X;
            float num167 = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - vector14.Y;
            float num168 = (float)Math.Sqrt(num166 * num166 + num167 * num167);
            if (Projectile.ai[0] == 0f)
            {
                if (num168 > 700f)
                {
                    Projectile.ai[0] = 1f;
                }
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
                Projectile.ai[1] += 1f;
                if (Projectile.ai[1] > 5f)
                {
                    Projectile.alpha = 0;
                }
                if (Projectile.ai[1] >= 10f)
                {
                    Projectile.ai[1] = 15f;
                    Projectile.velocity.Y = Projectile.velocity.Y + 0.3f;
                }
            }
            else if (Projectile.ai[0] == 1f)
            {
                Projectile.tileCollide = false;
                Projectile.rotation = (float)Math.Atan2(num167, num166) - 1.57f;
                float num169 = 20f;
                if (Projectile.type == ProjectileID.GolemFist)
                {
                    num169 = 30f;
                }
                if (num168 < 50f)
                {
                    Projectile.Kill();
                }
                num168 = num169 / num168;
                num166 *= num168;
                num167 *= num168;
                Projectile.velocity.X = num166;
                Projectile.velocity.Y = num167;
                if (Projectile.type == ProjectileID.GolemFist && Projectile.velocity.X < 0f)
                {
                    Projectile.spriteDirection = 1;
                }
                else if (Projectile.type == ProjectileID.GolemFist)
                {
                    Projectile.spriteDirection = -1;
                }
                if (Projectile.type == ProjectileID.BoxingGlove && Projectile.velocity.X < 0f)
                {
                    Projectile.spriteDirection = 1;
                    return;
                }
                if (Projectile.type == ProjectileID.BoxingGlove)
                {
                    Projectile.spriteDirection = -1;
                    return;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[0] = 1f;
            Projectile.netUpdate = true;
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = Mod.GetTexture("Chains/OceanWhaler_Chain");

            Vector2 position = Projectile.Center;
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float num1 = texture.Height;
            Vector2 vector24 = mountedCenter - position;
            float rotation = (float)Math.Atan2(vector24.Y, vector24.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector24.X) && float.IsNaN(vector24.Y))
                flag = false;
            while (flag)
            {
                if (vector24.Length() < num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector21 = vector24;
                    vector21.Normalize();
                    position += vector21 * num1;
                    vector24 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    color2 = Projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1.35f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
        }

    }
}
         