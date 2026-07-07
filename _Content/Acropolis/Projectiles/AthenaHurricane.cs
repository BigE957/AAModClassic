using AAModClassic.Assets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static System.Net.Mime.MediaTypeNames;

namespace AAModClassic._Content.Acropolis.Projectiles
{
    public class AthenaHurricane : ModProjectile
    {
        public override string Texture => AssetDirectory.General.Nothing;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Tornado");
		}

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1200;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
			float upTime = 600f;
            if (Projectile.soundDelay == 0)
            {
                Projectile.soundDelay = -1;
                SoundEngine.PlaySound(SoundID.Item82, Projectile.Center);
            }
            Projectile.ai[0]++;
            if (Projectile.ai[0] >= upTime)
            {
                Projectile.Kill();
            }

            float num1043 = 15f;
            float num1044 = 15f;
            Point centerTile = Projectile.Center.ToTileCoordinates();
            Collision.ExpandVertically(centerTile.X, centerTile.Y, out var topY, out var bottomY, (int)num1043, (int)num1044);
            topY++;
            bottomY--;
            Vector2 collisionTop = new Vector2(centerTile.X, topY) * 16f + new Vector2(8f);
            Vector2 collisionBottom = new Vector2(centerTile.X, bottomY) * 16f + new Vector2(8f);
            Vector2 collisionMiddle = Vector2.Lerp(collisionTop, collisionBottom, 0.5f);
            Vector2 sizeVector = new Vector2(0f, collisionBottom.Y - collisionTop.Y);
            sizeVector.X = sizeVector.Y * 0.2f;
            Projectile.width = (int)(sizeVector.X * 0.65f);
            Projectile.height = (int)sizeVector.Y;
            Projectile.Center = collisionMiddle;
            if (Projectile.owner == Main.myPlayer)
            {
                bool canHit = false;
                Vector2 center20 = Main.player[Projectile.owner].Center;
                Vector2 top = Main.player[Projectile.owner].Top;
                for (float num1045 = 0f; num1045 < 1f; num1045 += 0.05f)
                {
                    Vector2 position = Vector2.Lerp(collisionTop, collisionBottom, num1045);
                    if (Collision.CanHitLine(position, 0, 0, center20, 0, 0) || Collision.CanHitLine(position, 0, 0, top, 0, 0))
                    {
                        canHit = true;
                        break;
                    }
                }
                if (!canHit && Projectile.ai[0] < upTime - 120f)
                {
                    float seconds = Projectile.ai[0] % 60f;
                    Projectile.ai[0] = upTime - 120f + seconds;
                    Projectile.netUpdate = true;
                }
            }
            if (!(Projectile.ai[0] < upTime - 120f))
            {
                return;
            }
            for (int i = 0; i < 1; i++)
            {
                float value22 = -0.5f;
                float value23 = 0.9f;
                float amount3 = Main.rand.NextFloat();
                Vector2 vector166 = new Vector2(MathHelper.Lerp(0.1f, 1f, Main.rand.NextFloat()), MathHelper.Lerp(value22, value23, amount3));
                vector166.X *= MathHelper.Lerp(2.2f, 0.6f, amount3);
                vector166.X *= -1f;
                Vector2 vector167 = new Vector2(6f, 10f);
                Vector2 vector168 = collisionMiddle + sizeVector * vector166 * 0.5f + vector167;
                Dust dust57 = Main.dust[Dust.NewDust(vector168, 0, 0, DustID.Cloud)];
                dust57.position = vector168;
                dust57.customData = collisionMiddle + vector167;
                dust57.fadeIn = 1f;
                dust57.scale = 0.3f;
                if (vector166.X > -1.2f)
                {
                    dust57.velocity.X = 1f + Main.rand.NextFloat();
                }
                dust57.velocity.Y = Main.rand.NextFloat() * -0.5f - 1f;
            }

            foreach (NPC target in Main.ActiveNPCs)
            {
                if (target.type != NPCID.TargetDummy && !target.boss && target.chaseable && target.chaseable && Vector2.Distance(Projectile.Center, target.Center) < 150)
                {
                    float num3 = 6f;
                    Vector2 vector = new Vector2(target.position.X + target.width / 2, target.position.Y + target.height / 2);
                    float num4 = Projectile.Center.X - vector.X;
                    float num5 = Projectile.Center.Y - vector.Y;
                    float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
                    num6 = num3 / num6;
                    num4 *= num6;
                    num5 *= num6;
                    int num7 = 6;
                    target.velocity.X = (target.velocity.X * (num7 - 1) + num4) / num7;
                    target.velocity.Y = (target.velocity.Y * (num7 - 1) + num5) / num7;
                    target.velocity *= target.knockBackResist;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            float upTime = 600f;
            float counter = Projectile.ai[0];
            float opacity = MathHelper.Clamp(counter / 30f, 0f, 1f);
            if (counter > upTime - 60f)
                opacity = MathHelper.Lerp(1f, 0f, (counter - (upTime - 60f)) / 60f);
            
            Point point5 = Projectile.Center.ToTileCoordinates();
            Collision.ExpandVertically(point5.X, point5.Y, out var topY, out var bottomY, 15, 15);
			float topWorld = (topY + 1) * 16 + 8;
			float bottomWorld = (bottomY - 1) * 16 + 8;
            float distance = bottomWorld - topWorld;
            Texture2D texture = TextureAssets.Projectile[ProjectileID.SandnadoFriendly].Value;
            float baseRotation = -(float)Math.PI / 50f * counter;
            float distTravelled = 0f;
            float jump = 5.1f;
            Color baseColor = new(225, 225, 225);
            for (float i = (int)bottomWorld; i > (int)topWorld; i -= jump)
            {
                distTravelled += jump;
                float travelRatio = distTravelled / distance;
                float rotationOffset = distTravelled * ((float)Math.PI * 2f) / -20f;
                float scale = travelRatio - 0.15f;
                Color drawColor = Color.Lerp(Color.Transparent, baseColor, travelRatio * 2f);
                if (travelRatio > 0.5f)
                {
                    drawColor = Color.Lerp(Color.Transparent, baseColor, 2f - travelRatio * 2f);
                }
                drawColor.A = (byte)(drawColor.A * 0.5f);
                drawColor *= opacity;
                Vector2 drawPos = new Vector2(Projectile.Center.X, i) - Main.screenPosition;
                Main.EntitySpriteDraw(texture, drawPos, null, drawColor, baseRotation + rotationOffset, texture.Size() * 0.5f, 1f + scale, SpriteEffects.None);
            }
            return false;
        }
    }
}
