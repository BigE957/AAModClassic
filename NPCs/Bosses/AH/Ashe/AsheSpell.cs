using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.AH.Ashe
{
    public class AsheSpell : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fireball");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void PostAI()
        {
            if (Projectile.frameCounter++ > 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
            Projectile.timeLeft = 720;
            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 1;
            CooldownSlot = 1;
        }

        public override void AI()
        {
            Projectile.velocity *= 1f + Math.Abs(Projectile.ai[0]);

            Vector2 acceleration = Projectile.velocity.RotatedBy(Math.PI / 2);
            acceleration *= Projectile.ai[1];
            Projectile.velocity += acceleration;

            if (Projectile.velocity.X > 9)
            {
                Projectile.velocity.X = 9;
            }
        }
    }
}