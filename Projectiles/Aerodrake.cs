using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class Aerodrake : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = 3;
			Projectile.aiStyle = -1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
			Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 120;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 400);
        }
		
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			// Inflate some target hitboxes if they are beyond 8,8 size
			if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
			{
				targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
			}
			// Return if the hitboxes intersects, which means the javelin collides or not
			return projHitbox.Intersects(targetHitbox);
		}
		
		public override void AI()
		{
			Projectile.rotation =
			Projectile.velocity.ToRotation() +
			MathHelper.ToRadians(90f);
			
			if (Main.rand.Next(1) == 0)
			{
				int dustnumber = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 200, default, 0.8f);
				Main.dust[dustnumber].velocity *= 0.3f;
			}
		}

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound));
            float spread = 12f * 0.0174f;
            double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - spread / 2;
            double deltaAngle = spread / 3;
            for (int i = 0; i < 3; i++)
            {
                double offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 3f) * 5, (float)(Math.Cos(offsetAngle) * 3f) * 5, Mod.Find<ModProjectile>("Aerodrake1").Type, Projectile.damage / 6, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 3f) * 5, (float)(-Math.Cos(offsetAngle) * 3f) * 5, Mod.Find<ModProjectile>("Aerodrake1").Type, Projectile.damage / 6, Projectile.knockBack, Projectile.owner, 0f, 0f);
            }
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