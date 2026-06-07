using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened
{
    public class YamataAHead_MireDeathraySmall : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<YamataAHead_MireDeathray>().Texture;

        private const float maxTime = 30;

        public static Asset<Texture2D> Body;
        public static Asset<Texture2D> Tail;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mire Deathray");

            Body = ModContent.Request<Texture2D>(Texture + "_Body");
            Tail = ModContent.Request<Texture2D>(Texture + "_Tail");
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

        public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
        {
            return false;
        }

        public override void AI()
        {
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }
            if (Main.npc[(int)Projectile.ai[1]].active && Main.npc[(int)Projectile.ai[1]].type == ModContent.NPCType<YamataAHeadFake>())
            {
                Projectile.Center = Main.npc[(int)Projectile.ai[1]].Center;// + Vector2.UnitY * 45;
            }
            else
            {
                Projectile.Kill();
                return;
            }
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }
            if (Projectile.localAI[0] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Zombie104, new Vector2(Main.player[Main.myPlayer].Center.X, Main.player[Main.myPlayer].Center.Y));
            }
            float num801 = 0.5f;
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] >= maxTime)
            {
                Projectile.Kill();
                return;
            }
            Projectile.scale = (float)Math.Sin(Projectile.localAI[0] * 3.14159274f / maxTime) * 2.5f * num801;
            if (Projectile.scale > num801)
            {
                Projectile.scale = num801;
            }
            float num804 = Projectile.velocity.ToRotation();
            //timer += projectile.ai[0];
            //num804 += (float)Math.Cos(timer) * 0.014f * Math.Sign(projectile.ai[0]);
            Projectile.rotation = num804 - 1.57079637f;
            Projectile.velocity = num804.ToRotationVector2();
            float num805 = 3f;
            float[] array3 = new float[(int)num805];
            //Collision.LaserScan(samplingPoint, projectile.velocity, num806 * projectile.scale, 3000f, array3);
            for (int i = 0; i < array3.Length; i++)
                array3[i] = 3000f;
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
                float num810 = Projectile.velocity.ToRotation() + (Main.rand.NextBool(2) ? -1f : 1f) * 1.57079637f;
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

        public override void OnKill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<YamataAHead_MireDeathray>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.ai[0], Projectile.ai[1]);
        }

        //TODO: Cache these textures in a static Asset<Texture2D> field

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.velocity == Vector2.Zero)
            {
                return false;
            }
            Texture2D texture2D19 = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D texture2D20 = Body.Value;
            Texture2D texture2D21 = Tail.Value;

            float localAI = Projectile.localAI[1];
            Color drawColor = new Color(255, 255, 255, 0) * 0.9f;
            SpriteBatch spriteBatch = Main.spriteBatch;
            Vector2 drawPos = Projectile.Center - Main.screenPosition;
            spriteBatch.Draw(texture2D19, drawPos, null, drawColor, Projectile.rotation, texture2D19.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            localAI -= (texture2D19.Height / 2 + texture2D21.Height) * Projectile.scale;
            Vector2 laserEnd = Projectile.Center;
            laserEnd += Projectile.velocity * Projectile.scale * texture2D19.Height / 2f;
            if (localAI > 0f)
            {
                Rectangle frame = texture2D20.Frame();
                int moveAmt = texture2D20.Height / 2;
                float mult = ((int.MaxValue - Projectile.timeLeft) % moveAmt) / (float)moveAmt;
                float accumulatedLength = (int)(texture2D20.Height * mult);
                spriteBatch.Draw(texture2D20, laserEnd - Main.screenPosition, new Rectangle(0, (int)(texture2D20.Height * (1 - mult)), texture2D20.Width, (int)accumulatedLength), drawColor, Projectile.rotation, new Vector2(frame.Width / 2f, 0f), Projectile.scale, SpriteEffects.None, 0f);
                accumulatedLength *= Projectile.scale;
                laserEnd += Projectile.velocity * accumulatedLength;
                while (accumulatedLength + 1f < localAI)
                {
                    if (localAI - accumulatedLength < frame.Height)
                    {
                        frame.Height = (int)(localAI - accumulatedLength);
                    }
                    spriteBatch.Draw(texture2D20, laserEnd - Main.screenPosition, frame, drawColor, Projectile.rotation, new Vector2(frame.Width / 2f, 0f), Projectile.scale, SpriteEffects.None, 0f);
                    accumulatedLength += frame.Height * Projectile.scale;
                    laserEnd += Projectile.velocity * frame.Height * Projectile.scale;
                }
            }
            Vector2 drawPos2 = laserEnd - Main.screenPosition;
            spriteBatch.Draw(texture2D21, drawPos2, null, drawColor, Projectile.rotation, texture2D21.Frame(1, 1, 0, 0).Top(), Projectile.scale, SpriteEffects.None, 0f);
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