using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Anubis
{
    internal class Sandthrower_Sandstorm : ModProjectile
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sandstorm");
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.ignoreWater = false;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 100;
            Projectile.aiStyle = -1;
			Projectile.DamageType = DamageClass.Ranged;
        }
        
        public override void AI()
        {
            if (Projectile.timeLeft > 60)
            {
                Projectile.timeLeft = 60;
            }
            if (Projectile.ai[0] > 7f)
            {
                float num296 = 1f;
                if (Projectile.ai[0] == 8f)
                {
                    num296 = 0.25f;
                }
                else if (Projectile.ai[0] == 9f)
                {
                    num296 = 0.5f;
                }
                else if (Projectile.ai[0] == 10f)
                {
                    num296 = 0.75f;
                }
                Projectile.ai[0] += 1f;
                int num297 = DustID.Sandnado;
                if (Main.rand.NextBool(2))
                {
                    for (int num298 = 0; num298 < 3; num298++)
                    {
                        int num299 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num297, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
                        if (Main.rand.NextBool(3))
                        {
                            Main.dust[num299].noGravity = true;
                            Main.dust[num299].scale *= 1.5f;
                            Dust expr_DD5D_cp_0 = Main.dust[num299];
                            expr_DD5D_cp_0.velocity.X *= 2f;
                            Dust expr_DD7D_cp_0 = Main.dust[num299];
                            expr_DD7D_cp_0.velocity.Y *= 2f;
                        }
                        Main.dust[num299].scale *= 1f;
                        Dust expr_DDE2_cp_0 = Main.dust[num299];
                        expr_DDE2_cp_0.velocity.X *= 1.2f;
                        Dust expr_DE02_cp_0 = Main.dust[num299];
                        expr_DE02_cp_0.velocity.Y *= 1.2f;
                        Main.dust[num299].scale *= num296;
                        Main.dust[num299].velocity += Projectile.velocity;
                        if (!Main.dust[num299].noGravity)
                        {
                            Main.dust[num299].velocity *= 0.5f;
                        }
                    }
                }
            }
            else
            {
                Projectile.ai[0] += 1f;
            }
        }
    }
}