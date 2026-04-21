using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAModClassic.___Content.Hoard.Projectiles
{
    public class GreedGold : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gold");
            Main.projFrames[Projectile.type] = 8;
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 6;
        }

        public override void AI()
        {
            if (Projectile.ai[1] == 0)
            {
                Projectile.DamageType = DamageClass.Magic;
            }
            else if (Projectile.ai[1] == 1)
            {
                Projectile.DamageType = DamageClass.Ranged;
            }
            else
            {
                Projectile.minion = true;
            }
            Dust.NewDust(Projectile.position, 12, 12, DustID.GoldCoin);
            Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.03f * Projectile.direction;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 20)
            {
                Projectile.velocity.Y = Projectile.velocity.Y + 0.25f;
            }
            if (Projectile.velocity.Y > 16) { Projectile.velocity.Y = 16; }
        }
    }
}