using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Zero
{
    public class Neutralizer : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 800;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
        }

		public override void SetStaticDefaults()
		{
		// DisplayName.SetDefault("Death Beam");
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.position.X = Projectile.position.X + Projectile.velocity.X;
                Projectile.velocity.X = -oldVelocity.X;
                Projectile.damage = (int)(Projectile.damage * 1.2);
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.position.Y = Projectile.position.Y + Projectile.velocity.Y;
                Projectile.velocity.Y = -oldVelocity.Y;
                Projectile.damage = (int)(Projectile.damage * 1.2);
            }
            if (Projectile.damage > player.GetDamage(DamageClass.Ranged).ApplyTo(3000))
            {
                Projectile.damage = (int)(player.GetDamage(DamageClass.Ranged).ApplyTo(3000));
            }
            return false; // return false because we are handling collision
        }

        public override void AI()
        {
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 9f)
            {
                for (int num447 = 0; num447 < 4; num447++)
                {
                    Vector2 vector33 = Projectile.position;
                    vector33 -= Projectile.velocity * (num447 * 0.25f);
                    Projectile.alpha = 255;
                    int num448 = Dust.NewDust(vector33, Projectile.width, Projectile.height, ModContent.DustType<Dusts.VoidDust>(), 0f, 0f, 200); //Dust.NewDust(projectile.position, projectile.width, projectile.height, Terraria.ModLoader.ModContent.DustType<Dusts.VoidDust>(), 0f, 0f, 200);
                    Main.dust[num448].position = vector33;
                    Main.dust[num448].scale = Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[num448].velocity *= 0.2f;
                    Main.dust[num448].noGravity = true;
                }
            }
        }

    }
}
