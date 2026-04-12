using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Terrarium._Hardmode.NPCs
{
    public class UnityWatcher_Sphere : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = 0;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.timeLeft = 800;
            Projectile.penetrate = 10;
            Projectile.tileCollide = true;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
        }

		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Sphere");
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {

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
            Projectile.penetrate -= 1;

            if (Projectile.penetrate <= 0)
            {
                return true;
            }
            return false;
        }

        public override void PostAI()
        {
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 9f)
            {
                for (int num447 = 0; num447 < 4; num447++)
                {
                    Vector2 vector33 = Projectile.position;
                    vector33 -= Projectile.velocity * (num447 * 0.25f);
                    Projectile.alpha = 255;
                    int num448 = Dust.NewDust(vector33, Projectile.width, Projectile.height, ModContent.DustType<Dusts.SummonDust>(), 0f, 0f, 200);
                    Main.dust[num448].position = vector33;
                    Main.dust[num448].scale = Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[num448].velocity *= 0.2f;
                    Main.dust[num448].noGravity = true;
                }
            }
        }

    }
}
