using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.NPCs._BossZero.Protocol
{
    internal class GlitchBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("01010011");
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 1000;
            CooldownSlot = 1;
            Projectile.tileCollide = false;
        }

        public bool playedSound = false;
        public int dontDrawDelay = 2;
        public override void AI()
        {
            if (!playedSound)
            {
                playedSound = true;
            }
            Effects();
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Glitch"), Projectile.Center);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position, Vector2.Zero, ModContent.ProjectileType<GlitchBoom>(), Projectile.damage, 2, Projectile.owner);
        }

        public void Effects()
        {
            Lighting.AddLight(Projectile.Center, (255 - Projectile.alpha) * 0.5f / 255f, (255 - Projectile.alpha) * 0.05f / 255f, (255 - Projectile.alpha) * 0.05f / 255f);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Oblivion;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            dontDrawDelay = Math.Max(0, dontDrawDelay - 1);
            return dontDrawDelay == 0;
        }

    }
}