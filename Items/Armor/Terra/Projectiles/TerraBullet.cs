using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Terra.Projectiles
{
    public class TerraBullet : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Bullet");
		}

		public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.light = 0.3f;
            Projectile.alpha = 255;
            Projectile.extraUpdates = 4;
            Projectile.scale = 1.18f;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0, .7f, 0);
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 1f;
                SoundEngine.PlaySound(SoundID.Item11, Projectile.position);
            }
            float num100 = (float)Math.Sqrt(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y);
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= (byte)(num100 * 0.9);
            }
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}
