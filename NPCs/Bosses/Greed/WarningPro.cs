using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAModClassic.NPCs.Bosses.Greed
{
    public class WarningPro : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Warning");
            Main.projFrames[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 0;
            Projectile.timeLeft = 120;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 2)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 2)
                {
                    Projectile.frame = 0;
                }
            }
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] >= 60f)
            {
                Projectile.alpha = 255;
                if (Projectile.ai[0] == 0)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        int A = Main.rand.Next(-50, 50);
                        int B = Main.rand.Next(-200, 200) - 1000;

                        int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X + A, Projectile.Center.Y + B, 0f, 12f, Mod.Find<ModProjectile>("CovStalactitePro").Type, 43, 1);
                        Main.projectile[p].netUpdate = true;
                    }
                }
                else
                {
                    if (Main.rand.Next(10) == 0)
                    {
                        int A = Main.rand.Next(-80, 80);
                        int B = Main.rand.Next(-200, 200) - 1000;

                        int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X + A, Projectile.Center.Y + B, 0f, 10f, Mod.Find<ModProjectile>("DesireBeam").Type, 43, 1);
                        Main.projectile[p].netUpdate = true;
                    }
                }
            }
        }
    }
}