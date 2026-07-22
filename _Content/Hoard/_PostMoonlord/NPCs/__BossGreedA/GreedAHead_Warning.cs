using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard._PostMoonlord.NPCs.__BossGreedA
{
    public class GreedAHead_Warning : ModProjectile
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
                    if (Main.rand.NextBool(2))
                    {
                        int A = Main.rand.Next(-50, 50);
                        int B = Main.rand.Next(-200, 200) - 1000;

                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X + A, Projectile.Center.Y + B, 0f, 12f, ModContent.ProjectileType<GreedAHead_CovetiteStalagtite>(), 43, 1);
                    }
                }
                else
                {
                    if (Main.rand.NextBool(10))
                    {
                        int A = Main.rand.Next(-80, 80);
                        int B = Main.rand.Next(-200, 200) - 1000;

                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X + A, Projectile.Center.Y + B, 0f, 10f, ModContent.ProjectileType<SingularityOfDesire_DesireBeam>(), 43, 1);
                    }
                }
            }
        }
    }
}