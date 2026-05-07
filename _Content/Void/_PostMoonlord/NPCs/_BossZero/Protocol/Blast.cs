using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Globals;

namespace AAModClassic._Content.Void._PostMoonlord.NPCs._BossZero.Protocol
{
    public class Blast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4; 
        }
        public override void SetDefaults()
        {
            Projectile.damage = 1;
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.hostile = true;
            Projectile.aiStyle = -1;
        }

        public override Color? GetAlpha(Color drawColor)
        {
            return AAColor.Oblivion;
        }

        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 4)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
            if (Projectile.ai[1]++ == 120 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                switch (Projectile.ai[0])
                {
                    case 0f:
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(10, 0), ModContent.ProjectileType<EchoRay>(), 70, 3f, Main.myPlayer, 0, Projectile.whoAmI);
                        break;
                    case 1f:
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(-10, 0), ModContent.ProjectileType<EchoRay>(), 70, 3f, Main.myPlayer, 0, Projectile.whoAmI);
                        break;
                    case 2f:
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0, 10), ModContent.ProjectileType<EchoRay>(), 70, 3f, Main.myPlayer, 0, Projectile.whoAmI);
                        break;
                    default:
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0, -10), ModContent.ProjectileType<EchoRay>(), 70, 3f, Main.myPlayer, 0, Projectile.whoAmI);
                        break;
                }
            }
            if (Projectile.ai[1] > 240)
            {
                Projectile.Kill();
            }
        }

        public override void OnKill(int timeLeft)
        {
            Vector2 position = Projectile.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, DustID.GemRuby, 0f, 0f, 100, default, 1.5f);
                //Main.dust[num86].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num86].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 15; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, DustID.GemRuby, 0, 0, 100, new Color(), 2f);
                //Main.dust[num88].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num88].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, DustID.GemRuby, 0, 0, 100, new Color(), 2f);
                //Main.dust[num88].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num88].position = Projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += Projectile.DirectionTo(Main.dust[num88].position) * 8f;
            }
        }
    }
}
