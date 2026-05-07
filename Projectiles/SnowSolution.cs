using AAModClassic.Conversions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    internal class SnowSolution : ModProjectile
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 2;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            int dustType = ModContent.DustType<Dusts.SnowDustLight>();
            if (Projectile.owner == Main.myPlayer)
            {
                Point tilePos = Projectile.Center.ToTileCoordinates();
                WorldGen.Convert(tilePos.X, tilePos.Y, ModContent.GetInstance<TundraConversion>().Type);
            }
            if (Projectile.timeLeft > 133)
            {
                Projectile.timeLeft = 133;
            }
            if (Projectile.ai[0] > 7f)
            {
                float dustScale = 1f;
                if (Projectile.ai[0] == 8f)
                {
                    dustScale = 0.2f;
                }
                else if (Projectile.ai[0] == 9f)
                {
                    dustScale = 0.4f;
                }
                else if (Projectile.ai[0] == 10f)
                {
                    dustScale = 0.6f;
                }
                else if (Projectile.ai[0] == 11f)
                {
                    dustScale = 0.8f;
                }
                Projectile.ai[0] += 1f;
                for (int i = 0; i < 1; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, Color.Black, 1f);
                    Dust dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                    dust.scale *= 1.75f;
                    dust.velocity.X *= 2f;
                    dust.velocity.Y *= dust.velocity.Y * 2f;
                    dust.scale *= dustScale;
                }
            }
            else
            {
                Projectile.ai[0] += 1f;
            }
            Projectile.rotation += 0.3f * Projectile.direction;
        }
    }
}