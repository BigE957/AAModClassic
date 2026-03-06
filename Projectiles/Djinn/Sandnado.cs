using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Djinn
{
    public class Sandnado : ModProjectile
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Radium Arrow");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8; 
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;      
		}

		public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
        }

        public override void AI()
        {
            float num1123 = 900f;
            if (Projectile.soundDelay == 0)
            {
                Projectile.soundDelay = -1;
                SoundEngine.PlaySound(SoundID.Item82, Projectile.Center);
            }
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= num1123)
            {
                Projectile.Kill();
            }
            Projectile.damage = 0;
            if (Projectile.ai[0] < num1123 - 120f)
            {
                float num1124 = Projectile.ai[0] % 60f;
                Projectile.ai[0] = num1123 - 120f + num1124;
                Projectile.netUpdate = true;
            }
            float num1125 = 15f;
            float num1126 = 15f;
            Point point8 = Projectile.Center.ToTileCoordinates();
            Collision.ExpandVertically(point8.X, point8.Y, out int num1127, out int num1128, (int)num1125, (int)num1126);
            num1127++;
            num1128--;
            Vector2 value72 = new Vector2(point8.X, num1127) * 16f + new Vector2(8f);
            Vector2 value73 = new Vector2(point8.X, num1128) * 16f + new Vector2(8f);
            Vector2 vector145 = Vector2.Lerp(value72, value73, 0.5f);
            Vector2 value74 = new Vector2(0f, value73.Y - value72.Y);
            value74.X = value74.Y * 0.2f;
            Projectile.width = (int)(value74.X * 0.65f);
            Projectile.height = (int)value74.Y;
            Projectile.Center = vector145;
            if (Projectile.owner == Main.myPlayer)
            {
                bool flag75 = false;
                Vector2 center16 = Main.player[Projectile.owner].Center;
                Vector2 top = Main.player[Projectile.owner].Top;
                for (float num1129 = 0f; num1129 < 1f; num1129 += 0.05f)
                {
                    Vector2 position2 = Vector2.Lerp(value72, value73, num1129);
                    if (Collision.CanHitLine(position2, 0, 0, center16, 0, 0) || Collision.CanHitLine(position2, 0, 0, top, 0, 0))
                    {
                        flag75 = true;
                        break;
                    }
                }
                if (!flag75 && Projectile.ai[0] < num1123 - 120f)
                {
                    float num1130 = Projectile.ai[0] % 60f;
                    Projectile.ai[0] = num1123 - 120f + num1130;
                    Projectile.netUpdate = true;
                }
            }
            if (Projectile.ai[0] < num1123 - 120f)
            {
                for (int num1131 = 0; num1131 < 1; num1131++)
                {
                    float value75 = -0.5f;
                    float value76 = 0.9f;
                    float amount3 = Main.rand.NextFloat();
                    Vector2 value77 = new Vector2(MathHelper.Lerp(0.1f, 1f, Main.rand.NextFloat()), MathHelper.Lerp(value75, value76, amount3));
                    value77.X *= MathHelper.Lerp(2.2f, 0.6f, amount3);
                    value77.X *= -1f;
                    Vector2 value78 = new Vector2(6f, 10f);
                    Vector2 position3 = vector145 + value74 * value77 * 0.5f + value78;
                    Dust dust34 = Main.dust[Dust.NewDust(position3, 0, 0, DustID.Sandnado, 0f, 0f, 0, default, 1f)];
                    dust34.position = position3;
                    dust34.customData = vector145 + value78;
                    dust34.fadeIn = 1f;
                    dust34.scale = 0.3f;
                    if (value77.X > -1.2f)
                    {
                        dust34.velocity.X = .5f + Main.rand.NextFloat();
                    }
                    dust34.velocity.Y = Main.rand.NextFloat() * -0.5f - 1f;
                }
                return;
            }
        }
    }
}
