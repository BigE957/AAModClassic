using System;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Anubis.Forsaken
{
    class Razorsand : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 54;
            Projectile.height = 54;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            if (Projectile.ai[1] > 0f)
            {
                int num611 = (int)Projectile.ai[1] - 1;
                if (num611 < 255)
                {
                    Projectile.localAI[0] += 1f;
                    if (Projectile.localAI[0] > 10f)
                    {
                        int num612 = 6;
                        for (int num613 = 0; num613 < num612; num613++)
                        {
                            Vector2 vector43 = Vector2.Normalize(Projectile.velocity) * new Vector2(Projectile.width / 2f, Projectile.height) * 0.75f;
                            vector43 = vector43.RotatedBy((num613 - (num612 / 2 - 1)) * 3.1415926535897931 / (float)num612, default) + Projectile.Center;
                            Vector2 value15 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * Main.rand.Next(3, 8);
                            int num614 = Dust.NewDust(vector43 + value15, 0, 0, ModContent.DustType<SandDust>(), value15.X * 2f, value15.Y * 2f, 100, default, 1.4f);
                            Main.dust[num614].noGravity = true;
                            Main.dust[num614].noLight = true;
                            Main.dust[num614].velocity /= 4f;
                            Main.dust[num614].velocity -= Projectile.velocity;
                        }
                        if (Projectile.alpha <= 0)
                        {
                            Projectile.alpha = 0;
                        }
                        else
                        {
                            Projectile.alpha -= 5;
                        }
                        Projectile.rotation += Projectile.velocity.X * 0.1f;
                        Projectile.frame = (int)(Projectile.localAI[0] / 3f) % 3;
                    }
                    Vector2 value16 = Main.player[num611].Center - Projectile.Center;
                    float num615 = 4f;
                    num615 += Projectile.localAI[0] / 20f;
                    Projectile.velocity = Vector2.Normalize(value16) * num615;
                    if (value16.Length() < 50f)
                    {
                        Projectile.Kill();
                    }
                }
            }
            else
            {
                float num616 = 0.209439516f;
                float num617 = 4f;
                float num618 = (float)(Math.Cos(num616 * Projectile.ai[0]) - 0.5) * num617;
                Projectile.velocity.Y = Projectile.velocity.Y - num618;
                Projectile.ai[0] += 1f;
                num618 = (float)(Math.Cos(num616 * Projectile.ai[0]) - 0.5) * num617;
                Projectile.velocity.Y = Projectile.velocity.Y + num618;
                Projectile.localAI[0] += 1f;
                if (Projectile.localAI[0] > 10f)
                {
                    Projectile.alpha -= 5;
                    if (Projectile.alpha < 100)
                    {
                        Projectile.alpha = 100;
                    }
                    Projectile.rotation += Projectile.velocity.X * 0.1f;
                    Projectile.frame = (int)(Projectile.localAI[0] / 3f) % 3;
                }
            }
            if (Projectile.wet)
            {
                Projectile.position.Y = Projectile.position.Y - 16f;
                Projectile.Kill();
                return;
            }
        }

        public override void OnKill(int timeleft)
        {

            Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, 0f, 0f, 658, 40, 0f, Main.myPlayer, 0f, 0f);
        }
    }
}