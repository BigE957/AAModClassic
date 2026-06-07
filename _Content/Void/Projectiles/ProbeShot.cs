using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.Projectiles
{
    internal class ProbeShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Probe Laser");
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 1000;
            Projectile.minion = true;
            Projectile.friendly = true;
        }

        public bool playedSound = false;
        public int dontDrawDelay = 2;
        public override void AI()
        {
            if (!playedSound)
            {
                playedSound = true;
                SoundEngine.PlaySound(SoundID.Item12, Projectile.Center);
            }
            Effects();
            if (Projectile.velocity.Length() < 12f)
            {
                Projectile.velocity.X *= 1.05f;
                Projectile.velocity.Y *= 1.05f;
            }
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        }

        public virtual void Effects()
        {
            Lighting.AddLight(Projectile.Center, (255 - Projectile.alpha) * 0.5f / 255f, (255 - Projectile.alpha) * 0.05f / 255f, (255 - Projectile.alpha) * 0.05f / 255f);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            dontDrawDelay = Math.Max(0, dontDrawDelay - 1);
            return dontDrawDelay == 0;
        }

    }
}