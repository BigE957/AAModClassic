using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.___Content.Acropolis._PostMoonlord.Items._BossAthenaA.Weapons
{
    public class HurricaneStone_OwlRuneCharged : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.sentry = true;
            Projectile.scale = .001f;
            Projectile.alpha = 255;
        }

        float shoot = 0;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (Projectile.ai[0] < 0f)
            {
                Projectile.ai[0] += 1f;

                Projectile.ai[1] -= Projectile.direction * 0.3926991f / 50f;
            }

            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 5;
            }
            else
            {
                Projectile.alpha = 0;
            }

            if (Projectile.scale < 1)
            {
                Projectile.scale += .019f;
            }
            else
            {
                Projectile.scale = 1;
            }

            float num633 = 700f;
            float num634 = 800f;
            float num635 = 1200f;
            float num636 = 150f;
            bool flag24 = false;
            if (Projectile.ai[0] == 2f)
            {
                Projectile.ai[1] += 1f;
                Projectile.extraUpdates = 1;
                if (Projectile.ai[1] > 40f)
                {
                    Projectile.ai[1] = 1f;
                    Projectile.ai[0] = 0f;
                    Projectile.extraUpdates = 0;
                    Projectile.numUpdates = 0;
                    Projectile.netUpdate = true;
                }
                else
                {
                    flag24 = true;
                }
            }
            if (flag24)
            {
                return;
            }
            Vector2 vector46 = Projectile.position;
            bool flag25 = false;
            if (Projectile.ai[0] != 1f)
            {
                Projectile.tileCollide = false;
            }
            if (Projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16)))
            {
                Projectile.tileCollide = false;
            }
            for (int num645 = 0; num645 < 200; num645++)
            {
                NPC nPC2 = Main.npc[num645];
                if (nPC2.CanBeChasedBy(Projectile, false))
                {
                    float num646 = Vector2.Distance(nPC2.Center, Projectile.Center);
                    if ((Vector2.Distance(Projectile.Center, vector46) > num646 && num646 < num633 || !flag25) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC2.position, nPC2.width, nPC2.height))
                    {
                        num633 = num646;
                        vector46 = nPC2.Center;
                        flag25 = true;
                    }
                }
            }
            float num647 = num634;
            if (flag25)
            {
                num647 = num635;
            }
            if (Vector2.Distance(player.Center, Projectile.Center) > num647)
            {
                Projectile.ai[0] = 1f;
                Projectile.tileCollide = false;
                Projectile.netUpdate = true;
            }
            if (flag25 && Projectile.ai[0] == 0f)
            {
                Vector2 vector47 = vector46 - Projectile.Center;
                float num648 = vector47.Length();
                vector47.Normalize();
                if (num648 > 200f)
                {
                    float scaleFactor2 = 8f;
                    vector47 *= scaleFactor2;
                    Projectile.velocity = (Projectile.velocity * 40f + vector47) / 41f;
                }
                else
                {
                    float num649 = 4f;
                    vector47 *= -num649;
                    Projectile.velocity = (Projectile.velocity * 40f + vector47) / 41f;
                }
            }
            else
            {
                bool flag26 = false;
                if (!flag26)
                {
                    flag26 = Projectile.ai[0] == 1f;
                }
                float num650 = 5f; //6
                if (flag26)
                {
                    num650 = 12f; //15
                }
                Vector2 center2 = Projectile.Center;
                Vector2 vector48 = player.Center - center2 + new Vector2(0f, -30f); //-60
                float num651 = vector48.Length();
                if (num651 > 200f && num650 < 6.5f) //200 and 8
                {
                    num650 = 6.5f; //8
                }
                if (num651 < num636 && flag26 && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                    Projectile.netUpdate = true;
                }
                if (num651 > 2000f)
                {
                    Projectile.position.X = Main.player[Projectile.owner].Center.X - Projectile.width / 2;
                    Projectile.position.Y = Main.player[Projectile.owner].Center.Y - Projectile.height / 2;
                    Projectile.netUpdate = true;
                }
                if (num651 > 70f)
                {
                    vector48.Normalize();
                    vector48 *= num650;
                    Projectile.velocity = (Projectile.velocity * 40f + vector48) / 41f;
                }
                else if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                {
                    Projectile.velocity.X = -0.2f;
                    Projectile.velocity.Y = -0.1f;
                }
            }
            shoot += 1f;
            if (shoot % 30f == 0f && shoot < 180f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int[] array4 = new int[5];
                Vector2[] array5 = new Vector2[5];
                int num838 = 0;
                float num839 = 2000f;
                for (int num840 = 0; num840 < Main.maxNPCs; num840++)
                {
                    if (Main.npc[num840].active)
                    {
                        Vector2 center9 = Main.npc[num840].Center;
                        float num841 = Vector2.Distance(center9, Projectile.Center);
                        if (num841 < num839 && Collision.CanHit(Projectile.Center, 1, 1, center9, 1, 1))
                        {
                            array4[num838] = num840;
                            array5[num838] = center9;
                            if (++num838 >= array5.Length)
                            {
                                break;
                            }
                        }
                    }
                }
                for (int num842 = 0; num842 < num838; num842++)
                {
                    Vector2 vector82 = array5[num842] - Projectile.Center;
                    float ai = Main.rand.Next(100);
                    Vector2 vector83 = Vector2.Normalize(vector82.RotatedByRandom(0.78539818525314331)) * 10f;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vector83.X, vector83.Y, ModContent.ProjectileType<HurricaneStone_OlympianStorm>(), Projectile.damage, 0f, Main.myPlayer, vector82.ToRotation(), ai);
                }
            }
            Lighting.AddLight(Projectile.Center, 0f, 0.85f, 0.9f);
            if (Projectile.alpha < 150 && shoot < 180f)
            {
                for (int num843 = 0; num843 < 1; num843++)
                {
                    float num844 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                    if (num844 < -0.5f)
                    {
                        num844 = -0.5f;
                    }
                    if (num844 > 0.5f)
                    {
                        num844 = 0.5f;
                    }
                    Vector2 value47 = new Vector2(-Projectile.width * 0.2f * Projectile.scale, 0f).RotatedBy(num844 * 6.28318548f, default).RotatedBy(Projectile.velocity.ToRotation(), default);
                    int num845 = Dust.NewDust(Projectile.Center - Vector2.One * 5f, 10, 10, DustID.Electric, -Projectile.velocity.X / 3f, -Projectile.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                    Main.dust[num845].position = Projectile.Center + value47;
                    Main.dust[num845].velocity = Vector2.Normalize(Main.dust[num845].position - Projectile.Center) * 2f;
                    Main.dust[num845].noGravity = true;
                }
                for (int num846 = 0; num846 < 1; num846++)
                {
                    float num847 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                    if (num847 < -0.5f)
                    {
                        num847 = -0.5f;
                    }
                    if (num847 > 0.5f)
                    {
                        num847 = 0.5f;
                    }
                    Vector2 value48 = new Vector2(-Projectile.width * 0.6f * Projectile.scale, 0f).RotatedBy(num847 * 6.28318548f, default).RotatedBy(Projectile.velocity.ToRotation(), default);
                    int num848 = Dust.NewDust(Projectile.Center - Vector2.One * 5f, 10, 10, DustID.Electric, -Projectile.velocity.X / 3f, -Projectile.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                    Main.dust[num848].velocity = Vector2.Zero;
                    Main.dust[num848].position = Projectile.Center + value48;
                    Main.dust[num848].noGravity = true;
                }
                return;
            }

            Projectile.velocity = Vector2.Zero;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frame += 1;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
            Texture2D tex = Mod.GetTexture("Projectiles/Athena/OwlRuneCharged");
            Rectangle SunFrame = new Rectangle(Projectile.frame, 0, tex.Width, tex.Height / 4);
            BaseDrawing.DrawTexture(Main.spriteBatch, tex, 0, Projectile.position + new Vector2(0, Projectile.gfxOffY), Projectile.width, Projectile.height, Projectile.scale, 0, Projectile.spriteDirection, 4, SunFrame, Projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            return false;
        }
    }
}