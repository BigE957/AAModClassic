using AAModClassic.UI.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.___PreHardmode.NPCs.__BossDesertDjinn
{
    public class DesertDjinn_Djinnado : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Djinnado");
		}

		public override void SetDefaults()
		{
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = ProjAIStyleID.AncientStorm;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1200;
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                Projectile.alpha = 255;
        }

        public override void AI()
        {
            if (Projectile.localAI[0] == 1f)
            {
                Projectile.alpha += 10;
                if (Projectile.alpha >= 255)
                {
                    Projectile.Kill();
                }
            }
            else
            {
                
                if (Projectile.alpha <= 0)
                {
                    Projectile.alpha = 0;
                    if (Projectile.ai[2]-- <= 0)
                        Projectile.localAI[0] = 1f;
                }
                else
                    Projectile.alpha -= 10;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                float counter = Projectile.ai[0];
                float opacity = Projectile.Opacity;

                Point point5 = Projectile.Center.ToTileCoordinates();
                Collision.ExpandVertically(point5.X, point5.Y, out var topY, out var bottomY, 15, 15);
                float topWorld = (topY + 1) * 16 + 8;
                float bottomWorld = (bottomY - 1) * 16 + 8;
                float distance = bottomWorld - topWorld;
                Texture2D texture = TextureAssets.Projectile[ProjectileID.SandnadoFriendly].Value;
                float baseRotation = -(float)Math.PI / 50f * counter;
                float distTravelled = 0f;
                float jump = 5.1f;
                Color baseColor = new(212, 192, 100);
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

                //Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, new Vector2(Projectile.Center.X, Projectile.position.Y) - Main.screenPosition, null, Color.White, Projectile.rotation, TextureAssets.Projectile[Projectile.type].Size() * 0.5f, 1, SpriteEffects.None);
            }

            return !WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);
        }
    }
}
