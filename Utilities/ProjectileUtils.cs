using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Utilities
{
    public static class ProjectileUtils
    {
        public static void MakeSpriteCenteredOnInaccurateHitbox(this ModProjectile proj, Vector2 spriteSize)
        {
            proj.DrawOffsetX = (int)((proj.Projectile.width - spriteSize.X) / 2);
            proj.DrawOriginOffsetY = (int)((proj.Projectile.height - spriteSize.Y) / 2);
        }
    }
}
