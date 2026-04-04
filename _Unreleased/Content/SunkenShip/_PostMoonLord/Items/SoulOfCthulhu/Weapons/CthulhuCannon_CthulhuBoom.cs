using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.Weapons
{
    public class CthulhuCannon_CthulhuBoom : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cthulhusplosion");
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
        {
            Projectile.width = 70;
            Projectile.height = 70;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 4;
            Projectile.alpha = 255;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        public bool Shrink = false;
        public override void AI()
        {
            Projectile.scale = 1f - Projectile.alpha / 255f;
            Projectile.rotation += .1f;
            if (Projectile.alpha <= 0 && !Shrink)
            {
                Shrink = true;
            }
            if (!Shrink)
            {
                Projectile.alpha -= 8;
            }
            if (Shrink)
            {
                Projectile.alpha += 8;
                if (Projectile.alpha >= 255)
                {
                    Projectile.active = false;
                }
            }
        }
        

        public override void OnKill(int timeLeft)
        {
            Projectile.timeLeft = 0;
        }

    }
}
