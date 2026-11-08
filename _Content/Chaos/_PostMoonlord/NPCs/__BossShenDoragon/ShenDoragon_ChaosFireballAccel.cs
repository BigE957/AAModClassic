using Microsoft.Xna.Framework;
using System;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon
{
    public class ShenDoragon_ChaosFireballAccel : ShenDoragon_ChaosFireballAbstract
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.timeLeft = 360;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            Projectile.velocity *= 1f + Math.Abs(Projectile.ai[0]);

            Vector2 acceleration = Projectile.velocity.RotatedBy(Math.PI / 2);
            acceleration *= Projectile.ai[1];
            Projectile.velocity += acceleration;
        }
    }
}