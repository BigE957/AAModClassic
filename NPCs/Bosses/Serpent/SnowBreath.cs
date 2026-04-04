using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Serpent
{
    internal class SnowBreath : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Subzero Breath");
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 60;
            Projectile.timeLeft = 100;
            Projectile.tileCollide = false;
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

                int num297 = ModContent.DustType<SnowDustLight>();
                if (Projectile.ai[1] == 1)
                {
                    num297 = 75;
                }
                if (Projectile.ai[1] == 2)
                {
                    num297 = DustID.GoldFlame;
                }
                if (Projectile.ai[1] == 3)
                {
                    num297 = ModContent.DustType<BroodmotherDust>();
                }
                if (Projectile.ai[1] == 4)
                {
                    num297 = ModContent.DustType<AcidDust>();
                }
                if (Projectile.ai[1] == 5)
                {
                    num297 = ModContent.DustType<HallowedDustT>();
                }

                if (Main.rand.Next(2) == 0)
                {
                    for (int num298 = 0; num298 < 3; num298++)
                    {
                        int num299 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num297, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 1f);
                        if (Main.rand.Next(3) == 0)
                        {
                            Main.dust[num299].noGravity = true;
                            Main.dust[num299].scale *= 2.2f;
                            Dust expr_DD5D_cp_0 = Main.dust[num299];
                            expr_DD5D_cp_0.velocity.X *= 2f;
                            Dust expr_DD7D_cp_0 = Main.dust[num299];
                            expr_DD7D_cp_0.velocity.Y *= 2f;
                        }
                        Main.dust[num299].scale *= 1.2f;
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
            Projectile.rotation += 0.3f * Projectile.direction;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Chilled, 300);
            if  (Projectile.ai[1] == 1)
            {
                target.AddBuff(BuffID.CursedInferno, 180);
            }
            if (Projectile.ai[1] == 2)
            {
                target.AddBuff(BuffID.Ichor, 180);
            }
            if (Projectile.ai[1] == 3)
            {
                target.AddBuff(BuffID.OnFire, 180);
            }
            if (Projectile.ai[1] == 4)
            {
                target.AddBuff(BuffID.Poisoned, 180);
            }
        }
    }
}