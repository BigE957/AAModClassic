using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAModClassic._Vanilla.Facsimiles._1._3._5._3
{
    public abstract class BoulderStaffOfEarthFacsimile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 34;
            Projectile.aiStyle = 14;
            Projectile.friendly = true;
            Projectile.penetrate = 6;
            //Projectile.magic = true; //yucky you
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] > 15f)
            {
                Projectile.ai[0] = 15f;
                if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
                {
                    Projectile.velocity.X *= 0.97f;
                    if ((double)Projectile.velocity.X > -0.01 && (double)Projectile.velocity.X < 0.01)
                    {
                        Projectile.Kill();
                    }
                }
                Projectile.velocity.Y += 0.2f;
            }
            Projectile.rotation += Projectile.velocity.X * 0.05f;

            if (Projectile.velocity.Y > 16f)
                Projectile.velocity.Y = 16f;
        }

    }
}
