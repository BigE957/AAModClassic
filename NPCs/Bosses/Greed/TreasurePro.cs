using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;

namespace AAModClassic.NPCs.Bosses.Greed
{
    public class TreasurePro : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Memories of Something Grand");
            Main.projFrames[Projectile.type] = 8;
        }

        public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.alpha = 255;
            Projectile.tileCollide = false;
        }

        public bool offsetLeft = false;

        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                int changeChoice = Main.rand.Next(8);
                if (changeChoice == 0)
                {
                    Projectile.frame = 0;
                }
                if (changeChoice == 1)
                {
                    Projectile.frame = 1;
                }
                if (changeChoice == 2)
                {
                    Projectile.frame = 2;
                }
                if (changeChoice == 3)
                {
                    Projectile.frame = 3;
                }
                if (changeChoice == 4)
                {
                    Projectile.frame = 4;
                }
                if (changeChoice == 5)
                {
                    Projectile.frame = 5;
                }
                if (changeChoice == 6)
                {
                    Projectile.frame = 6;
                }
                if (changeChoice == 7)
                {
                    Projectile.frame = 7;
                }
                Projectile.ai[0] = 1;
            }
            Projectile.alpha -= 4;
            if (Projectile.alpha <= 0)
            {
                if (Projectile.velocity.Y < 0)
                {
                    Projectile.velocity.Y += 0.1f;
                }
            }
            if (Projectile.velocity.Y >= 0)
            {
                if (offsetLeft)
                {
                    Projectile.rotation -= 0.025f;
                    if (Projectile.rotation <= -0.1f)
                    {
                        Projectile.rotation = -0.1f;
                        offsetLeft = false;
                    }
                }
                else
                {
                    Projectile.rotation += 0.025f;
                    if (Projectile.rotation >= 0.1f)
                    {
                        Projectile.rotation = 0.1f;
                        offsetLeft = true;
                    }
                }
                if (++Projectile.localAI[1] >= 60)
                {
                    Vector2 vel = Vector2.Normalize(Projectile.velocity);
                    for (int i = 0; i < 12; ++i)
                    {
                        vel = vel.RotatedBy(Math.PI / 6);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel * 2, ModContent.ProjectileType<DesireSparkPro>(), Projectile.damage / 2, 0f, Main.myPlayer);
                    }
                    Projectile.Kill();
                }
            }
        }
    }
}