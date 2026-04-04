using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using AAModClassic.Globals;

namespace AAModClassic.Projectiles.Greed
{
    public class Dig : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gold Digger");
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 12;
            }
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[1] += 1f;
                if (Projectile.ai[1] >= 45f)
                {
                    float num975 = 0.98f;
                    float num976 = 0.35f;
                    Projectile.ai[1] = 45f;
                    Projectile.velocity.X = Projectile.velocity.X * num975;
                    Projectile.velocity.Y = Projectile.velocity.Y + num976;
                }
                Projectile.rotation = Projectile.velocity.ToRotation() + 0.785f; //1.57079637f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            Vector2 vector = Vector2.Normalize(Projectile.velocity);
            if (!AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<GoldFountain>()))
            {
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X - vector.X * 20f, Projectile.position.Y - vector.Y * 20f, 0, 0, ModContent.ProjectileType<GoldFountain>(), Projectile.damage, 1, Projectile.owner, 0, 0);
            }
        }
    }
}