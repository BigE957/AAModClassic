using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs._BossShen.GripsShen
{
    public class BlazeGripRay : ModProjectile
    {
        public override string Texture => "AAModClassic/_Content/Chaos/_PostMoonlord/NPCs/_BossShen/GripsShen/BlazeGripRay";
        private const float maxTime = 300;
        public float maxScale = 1f;
        private float timer = 0;

        public NPC centerNPC;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blaze Grip Deathray");
        }

        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.hostile = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            CooldownSlot = 1;
        }

        public override bool CanHitPlayer(Player target)
        {
            return Projectile.scale >= 1f;
        }

        public int proj = 0;

        public override void AI()
        {
            Vector2? vector78 = null;
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }

            timer++;

            if(proj == 0)
            {
                for(int i = 0; i<1000; i++)
                {
                    if(Main.projectile[i].type == ModContent.ProjectileType<BlazeBomb>())
                    {
                        proj = Main.projectile[i].whoAmI;
                    }
                } 
            }

            float raydirection = 1f;
            
            if(proj != 0)
            {
                if(Main.projectile[proj].active && Main.projectile[proj].ModProjectile is BlazeBomb)
                {
                    Projectile.Center = Main.projectile[proj].position + new Vector2(Main.projectile[proj].width/2, Main.projectile[proj].height/2);

                    /* 
                    Vector2 dir = Vector2.Normalize(Main.player[centerNPC.target].Center - Main.projectile[proj].Center);
                    if (dir.Y < 0f)
                    {
                        raydirection = -1f;
                    }
                    */

                    raydirection = Main.player[centerNPC.target].Center.ToRotation() - Main.projectile[proj].Center.ToRotation() > 0? 1f:-1f;

                    //projectile.velocity = Vector2.Normalize(projectile.velocity);
                    Projectile.position += 30 * Projectile.velocity;
                    Projectile.position += 10 * Projectile.velocity.RotatedBy(Main.npc[proj].spriteDirection > 0 ? -Math.PI / 2 : Math.PI / 2);
                }
                else
                {
                    if(Projectile.localAI[0] < 290)Projectile.localAI[0] = 290;
                }
            }
            else if(timer < 100)
            {
                centerNPC = Main.npc[(int)Projectile.ai[1]];
                if (centerNPC.active && centerNPC.ModNPC is BlazeGrip)
                {
                    Projectile.Center = Main.npc[(int)Projectile.ai[1]].Center;

                    Vector2 dir = Vector2.Normalize(Main.player[centerNPC.target].Center - centerNPC.Center);
                    if (dir.Y < 0f)
                    {
                        raydirection = -1f;
                    }

                    /* 
                    float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                    double startAngle = Math.Atan2(dir.X, dir.Y);
                    double deltaAngle = 45f * 0.0174f;
                    double offsetAngle = startAngle + (deltaAngle * projectile.ai[0]);
                    Vector2 shootdir = new Vector2(baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle));
                    

                    projectile.velocity = Vector2.Normalize(shootdir);
                    */
                    Projectile.position += 30 * Projectile.velocity;
                    Projectile.position += 10 * Projectile.velocity.RotatedBy(Main.npc[(int)Projectile.ai[1]].spriteDirection > 0 ? -Math.PI / 2 : Math.PI / 2);
                }
                else
                {
                    if(Projectile.localAI[0] < 290)Projectile.localAI[0] = 290;
                }
            }
            
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }
            if (Projectile.localAI[0] == 0f && maxScale >= 1)
            {
                SoundEngine.PlaySound(SoundID.Zombie104, Projectile.position);
            }
            float num801 = maxScale;
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] >= maxTime)
            {
                Projectile.Kill();
                return;
            }
            Projectile.scale = (float)Math.Sin(Projectile.localAI[0] * 3.14159274f / maxTime) * 10f * num801;
            if (Projectile.scale > num801)
            {
                Projectile.scale = num801;
            }
            float num804 = Projectile.velocity.ToRotation();
            num804 += 6.2831855f / 750f * raydirection;
            Projectile.rotation = num804 - 1.57079637f;
            Projectile.velocity = num804.ToRotationVector2();
            float num805 = 3f;
            float num806 = Projectile.width;
            Vector2 samplingPoint = Projectile.Center;
            if (vector78.HasValue)
            {
                samplingPoint = vector78.Value;
            }
            float[] array3 = new float[(int)num805];
            Collision.LaserScan(samplingPoint, Projectile.velocity, num806 * Projectile.scale, 2400f, array3);
            float num807 = 0f;
            int num3;
            for (int num808 = 0; num808 < array3.Length; num808 = num3 + 1)
            {
                num807 += array3[num808];
                num3 = num808;
            }
            num807 /= num805;
            float amount = 0.5f;
            Projectile.localAI[1] = MathHelper.Lerp(Projectile.localAI[1], num807, amount);
            Vector2 vector79 = Projectile.Center + Projectile.velocity * (Projectile.localAI[1] - 14f);
            for (int num809 = 0; num809 < 2; num809 = num3 + 1)
            {
                float num810 = Projectile.velocity.ToRotation() + ((Main.rand.NextBool(2)) ? -1f : 1f) * 1.57079637f;
                float num811 = (float)Main.rand.NextDouble() * 2f + 2f;
                Vector2 vector80 = new Vector2((float)Math.Cos(num810) * num811, (float)Math.Sin(num810) * num811);
                int num812 = Dust.NewDust(vector79, 0, 0, DustID.CopperCoin, vector80.X, vector80.Y, 0, default, 1f);
                Main.dust[num812].noGravity = true;
                Main.dust[num812].scale = 1.7f;
                num3 = num809;
            }
            if (Main.rand.NextBool(5))
            {
                Vector2 value29 = Projectile.velocity.RotatedBy(1.5707963705062866, default) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
                int num813 = Dust.NewDust(vector79 + value29 - Vector2.One * 4f, 8, 8, DustID.CopperCoin, 0f, 0f, 100, default, 1.5f);
                Dust dust = Main.dust[num813];
                dust.velocity *= 0.5f;
                Main.dust[num813].velocity.Y = -Math.Abs(Main.dust[num813].velocity.Y);
            }
            //DelegateMethods.v3_1 = new Vector3(0.3f, 0.65f, 0.7f);
            //Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * projectile.localAI[1], (float)projectile.width * projectile.scale, new Utils.TileActionAttempt(DelegateMethods.CastLight));
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.velocity == Vector2.Zero)
            {
                return false;
            }
            Texture2D texture2D19 = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D texture2D20 = Mod.GetTexture("_Content/Chaos/_PostMoonlord/NPCs/_BossShen/GripsShen/BlazeGripRay2");
            Texture2D texture2D21 = Mod.GetTexture("_Content/Chaos/_PostMoonlord/NPCs/_BossShen/GripsShen/BlazeGripRay3");
            float num223 = Projectile.localAI[1];
            Color color44 = new Color(255, 255, 255, 0) * 0.9f;
            SpriteBatch arg_ABD8_0 = Main.spriteBatch;
            Texture2D arg_ABD8_1 = texture2D19;
            Vector2 arg_ABD8_2 = Projectile.Center - Main.screenPosition;
            Rectangle? sourceRectangle2 = null;
            arg_ABD8_0.Draw(arg_ABD8_1, arg_ABD8_2, sourceRectangle2, color44, Projectile.rotation, texture2D19.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            num223 -= (texture2D19.Height / 2 + texture2D21.Height) * Projectile.scale;
            Vector2 value20 = Projectile.Center;
            value20 += Projectile.velocity * Projectile.scale * texture2D19.Height / 2f;
            if (num223 > 0f)
            {
                float num224 = 0f;
                Rectangle rectangle7 = new Rectangle(0, 16 * (Projectile.timeLeft / 3 % 5), texture2D20.Width, 16);
                while (num224 + 1f < num223)
                {
                    if (num223 - num224 < rectangle7.Height)
                    {
                        rectangle7.Height = (int)(num223 - num224);
                    }
                    Main.spriteBatch.Draw(texture2D20, value20 - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(rectangle7), color44, Projectile.rotation, new Vector2(rectangle7.Width / 2, 0f), Projectile.scale, SpriteEffects.None, 0f);
                    num224 += rectangle7.Height * Projectile.scale;
                    value20 += Projectile.velocity * rectangle7.Height * Projectile.scale;
                    rectangle7.Y += 16;
                    if (rectangle7.Y + rectangle7.Height > texture2D20.Height)
                    {
                        rectangle7.Y = 0;
                    }
                }
            }
            SpriteBatch arg_AE2D_0 = Main.spriteBatch;
            Texture2D arg_AE2D_1 = texture2D21;
            Vector2 arg_AE2D_2 = value20 - Main.screenPosition;
            sourceRectangle2 = null;
            arg_AE2D_0.Draw(arg_AE2D_1, arg_AE2D_2, sourceRectangle2, color44, Projectile.rotation, texture2D21.Frame(1, 1, 0, 0).Top(), Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 unit = Projectile.velocity;
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * Projectile.localAI[1], Projectile.width * Projectile.scale, new Utils.TileActionAttempt(DelegateMethods.CutTiles));
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projHitbox.Intersects(targetHitbox))
            {
                return true;
            }
            float num6 = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity * Projectile.localAI[1], 22f * Projectile.scale, ref num6))
            {
                return true;
            }
            return false;
        }
    }
}