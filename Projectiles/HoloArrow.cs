using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class HoloArrow : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Holo Arrow");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 600;
            Projectile.light = 0f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            AIType = ProjectileID.Bullet;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Red;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}