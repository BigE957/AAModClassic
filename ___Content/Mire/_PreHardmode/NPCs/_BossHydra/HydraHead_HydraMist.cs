using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace AAModClassic.NPCs.Bosses.Hydra
{
    internal class HydraMist : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hydra Breath");
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.scale = 1.1f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }

        public override void AI()
        {
            Projectile.tileCollide = false;
            Projectile.ai[1] += 1f;
            if (Projectile.ai[1] > 60f)
            {
                Projectile.ai[0] += 10f;
            }
            if (Projectile.ai[0] > 255f)
            {
                Projectile.Kill();
                Projectile.ai[0] = 255f;
            }
            Projectile.alpha = (int)(100.0 + Projectile.ai[0] * 0.7);
            Projectile.rotation += Projectile.velocity.X * 0.1f;
            Projectile.rotation += Projectile.direction * 0.003f;
            Projectile.velocity *= 0.96f;
            Rectangle rectangle5 = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
            for (int num886 = 0; num886 < 1000; num886++)
            {
                if (num886 != Projectile.whoAmI && Main.projectile[num886].active && Main.projectile[num886].type >= ProjectileID.ToxicCloud && Main.projectile[num886].type <= ProjectileID.ToxicCloud3)
                {
                    Rectangle value53 = new Rectangle((int)Main.projectile[num886].position.X, (int)Main.projectile[num886].position.Y, Main.projectile[num886].width, Main.projectile[num886].height);
                    if (rectangle5.Intersects(value53))
                    {
                        Vector2 vector91 = Main.projectile[num886].Center - Projectile.Center;
                        if (vector91.X == 0f && vector91.Y == 0f)
                        {
                            if (num886 < Projectile.whoAmI)
                            {
                                vector91.X = -1f;
                                vector91.Y = 1f;
                            }
                            else
                            {
                                vector91.X = 1f;
                                vector91.Y = -1f;
                            }
                        }
                        vector91.Normalize();
                        vector91 *= 0.005f;
                        Projectile.velocity -= vector91;
                        Main.projectile[num886].velocity += vector91;
                    }
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Poison_Buff>(), 300);
        }
    }
}