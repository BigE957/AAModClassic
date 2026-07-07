using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard._PostMoonlord.NPCs.__BossGreedA
{
    public class GreedAHead_DesireSpark : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Desire Spark");
        }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.penetrate = 1;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 80;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            if (Main.rand.NextBool(4))
            {
                int DustID2 = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Enchanted_Gold, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 20, default, 1f);
                Main.dust[DustID2].noGravity = true;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Enchanted_Gold, 0f, 0f, 100, default, 1.5f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
        }
    }
}