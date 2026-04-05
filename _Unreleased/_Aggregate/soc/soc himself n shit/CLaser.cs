using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Utilities;
using System;
using Terraria;
using Terraria.ModLoader;
using AAModClassic;

namespace AAModClassic._Unreleased.NPCs.Bosses.SoC
{
    public class CLaser : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reality Laser");
        }

        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.aiStyle = ProjAIStyleID.ThickLaser;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
        }
        
        public override void AI()
        {
            Vector2? vector69 = null;
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }
            if (Main.npc[(int)Projectile.ai[1]].active)
            {
                Vector2 value29 = new Vector2(27f, 59f);
                Vector2 value30 = Utils.Vector2FromElipse(Main.npc[(int)Projectile.ai[1]].localAI[0].ToRotationVector2(), value29 * Main.npc[(int)Projectile.ai[1]].localAI[1]);
                Projectile.position = Main.npc[(int)Projectile.ai[1]].Center + value30 - new Vector2((float)Projectile.width, (float)Projectile.height) / 2f;
            }
            else
            {
                if (!Main.projectile[(int)Projectile.ai[1]].active)
                {
                    Projectile.Kill();
                    return;
                }
                float num790 = (float)((int)Projectile.ai[0]) - 2.5f;
                Vector2 value35 = Vector2.Normalize(Main.projectile[(int)Projectile.ai[1]].velocity);
                float num791 = num790 * 0.5235988f;
                Vector2 value36 = Vector2.Zero;
                float num792;
                float y;
                float num793;
                float scaleFactor6;
                if (Projectile.ai[0] < 180f)
                {
                    num792 = 1f - Projectile.ai[0] / 180f;
                    y = 20f - Projectile.ai[0] / 180f * 14f;
                    if (Projectile.ai[0] < 120f)
                    {
                        num793 = 20f - 4f * (Projectile.ai[0] / 120f);
                        Projectile.Opacity = Projectile.ai[0] / 120f * 0.4f;
                    }
                    else
                    {
                        num793 = 16f - 10f * ((Projectile.ai[0] - 120f) / 60f);
                        Projectile.Opacity = 0.4f + (Projectile.ai[0] - 120f) / 60f * 0.6f;
                    }
                    scaleFactor6 = -22f + Projectile.ai[0] / 180f * 20f;
                }
                else
                {
                    num792 = 0f;
                    num793 = 1.75f;
                    y = 6f;
                    Projectile.Opacity = 1f;
                    scaleFactor6 = -2f;
                }
                float num794 = (Projectile.ai[0] + num790 * num793) / (num793 * 6f) * 6.28318548f;
                num791 = Vector2.UnitY.RotatedBy((double)num794, default(Vector2)).Y * 0.5235988f * num792;
                value36 = (Vector2.UnitY.RotatedBy((double)num794, default(Vector2)) * new Vector2(4f, y)).RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
                Projectile.position = Projectile.Center + value35 * 16f - Projectile.Size / 2f + new Vector2(0f, -Main.projectile[(int)Projectile.ai[1]].gfxOffY);
                Projectile.position += Projectile.velocity.ToRotation().ToRotationVector2() * scaleFactor6;
                Projectile.position += value36;
                Projectile.velocity = Vector2.Normalize(Projectile.velocity).RotatedBy((double)num791, default(Vector2));
                Projectile.scale = 1.4f * (1f - num792);
                if (Projectile.ai[0] >= 180f)
                {
                    Projectile.damage *= 3;
                    vector69 = new Vector2?(Projectile.Center);
                }
                if (!Collision.CanHitLine(Main.player[Projectile.owner].Center, 0, 0, Projectile.Center, 0, 0))
                {
                    vector69 = new Vector2?(Main.player[Projectile.owner].Center);
                }
                Projectile.friendly = (Projectile.ai[0] > 30f);
            }
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }
            if (Projectile.localAI[0] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Zombie104, Projectile.position);
            }
            float num795 = 1f;
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] >= 180f)
            {
                Projectile.Kill();
                return;
            }
            Projectile.scale = (float)Math.Sin((double)(Projectile.localAI[0] * 3.14159274f / 180f)) * 10f * num795;
            if (Projectile.scale > num795)
            {
                Projectile.scale = num795;
            }
            float num798 = Projectile.velocity.ToRotation();
            num798 += Projectile.ai[0];
            Projectile.rotation = num798 - 1.57079637f;
            Projectile.velocity = num798.ToRotationVector2();
            float num799 = 3f;
            float num800 = Projectile.width;
            Vector2 samplingPoint = Projectile.Center;
            if (vector69.HasValue)
            {
                samplingPoint = vector69.Value;
            }
            
            float[] array3 = new float[(int)num799];
            Collision.LaserScan(samplingPoint, Projectile.velocity, num800 * Projectile.scale, 2400f, array3);
            float num801 = 0f;
            for (int num802 = 0; num802 < array3.Length; num802++)
            {
                num801 += array3[num802];
            }
            num801 /= num799;
            float amount = 0.5f;
            Projectile.localAI[1] = MathHelper.Lerp(Projectile.localAI[1], num801, amount);
            Vector2 vector70 = Projectile.Center + Projectile.velocity * (Projectile.localAI[1] - 14f);
            for (int num803 = 0; num803 < 2; num803++)
            {
                float num804 = Projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                float num805 = (float)Main.rand.NextDouble() * 2f + 2f;
                Vector2 vector71 = new Vector2((float)Math.Cos((double)num804) * num805, (float)Math.Sin((double)num804) * num805);
                int num806 = Dust.NewDust(vector70, 0, 0, DustID.Vortex, vector71.X, vector71.Y, 0, default(Color), 1f);
                Main.dust[num806].noGravity = true;
                Main.dust[num806].scale = 1.7f;
            }
            if (Main.rand.Next(5) == 0)
            {
                Vector2 value37 = Projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)Projectile.width;
                int num807 = Dust.NewDust(vector70 + value37 - Vector2.One * 4f, 8, 8, DustID.Smoke, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num807].velocity *= 0.5f;
                Main.dust[num807].velocity.Y = -Math.Abs(Main.dust[num807].velocity.Y);
            }
            DelegateMethods.v3_1 = new Vector3(0.3f, 0.65f, 0.7f);
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * Projectile.localAI[1], (float)Projectile.width * Projectile.scale, new Utils.TileActionAttempt(DelegateMethods.CastLight));
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.velocity == Vector2.Zero)
            {
                return false;
            }
            Texture2D texture2D18 = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D texture2D19 = Mod.GetTexture("_Unreleased/NPCs/Bosses/SoC/CLaserTex");
            Texture2D texture2D20 = Mod.GetTexture("_Unreleased/NPCs/Bosses/SoC/CLaserHead");
            float num224 = Projectile.localAI[1];
            Microsoft.Xna.Framework.Color color44 = new Microsoft.Xna.Framework.Color(255, 255, 255, 0) * 0.9f;
            Main.spriteBatch.Draw(texture2D18, Projectile.Center - Main.screenPosition, null, color44, Projectile.rotation, texture2D18.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            num224 -= (float)(texture2D18.Height / 2 + texture2D20.Height) * Projectile.scale;
            Vector2 value21 = Projectile.Center;
            value21 += Projectile.velocity * Projectile.scale * (float)texture2D18.Height / 2f;
            if (num224 > 0f)
            {
                float num225 = 0f;
                Rectangle value22 = new Microsoft.Xna.Framework.Rectangle(0, 16 * (Projectile.timeLeft / 3 % 5), texture2D19.Width, 16);
                while (num225 + 1f < num224)
                {
                    if (num224 - num225 < (float)value22.Height)
                    {
                        value22.Height = (int)(num224 - num225);
                    }
                    Main.spriteBatch.Draw(texture2D19, value21 - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(value22), color44, Projectile.rotation, new Vector2((float)(value22.Width / 2), 0f), Projectile.scale, SpriteEffects.None, 0f);
                    num225 += (float)value22.Height * Projectile.scale;
                    value21 += Projectile.velocity * (float)value22.Height * Projectile.scale;
                    value22.Y += 16;
                    if (value22.Y + value22.Height > texture2D19.Height)
                    {
                        value22.Y = 0;
                    }
                }
            }
            Main.spriteBatch.Draw(texture2D20, value21 - Main.screenPosition, null, color44, Projectile.rotation, texture2D20.Frame(1, 1, 0, 0).Top(), Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}
