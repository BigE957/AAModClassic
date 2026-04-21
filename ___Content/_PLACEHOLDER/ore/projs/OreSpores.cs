using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content._PLACEHOLDER.ore.projs
{
    public class OreSpores : ModProjectile
    {
        public override void SetStaticDefaults()
        {    
            Main.projFrames[Projectile.type] = 3;     
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = ProjAIStyleID.SporeGas;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 3600;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void AI()
        {
            Projectile.frame = (int)Projectile.ai[1];
            Projectile.rotation += Projectile.velocity.X * 0.02f;
            if (Projectile.velocity.X < 0f)
            {
                Projectile.rotation -= Math.Abs(Projectile.velocity.Y) * 0.02f;
            }
            else
            {
                Projectile.rotation += Math.Abs(Projectile.velocity.Y) * 0.02f;
            }
            Projectile.velocity *= 0.98f;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 60f)
            {
                if (Projectile.alpha < 255)
                {
                    Projectile.alpha += 5;
                    if (Projectile.alpha > 255)
                    {
                        Projectile.alpha = 255;
                        return;
                    }
                }
                else if (Projectile.owner == Main.myPlayer)
                {
                    Projectile.Kill();
                    return;
                }
            }
            else if (Projectile.alpha > 80)
            {
                Projectile.alpha -= 30;
                if (Projectile.alpha < 80)
                {
                    Projectile.alpha = 80;
                    return;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.timeLeft = 0;
        }
    }
}
