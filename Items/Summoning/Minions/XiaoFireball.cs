using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    public class XiaoFireball : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Xiao Fireball");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = -1;
            Projectile.minion = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 120 * Projectile.extraUpdates;
            Projectile.friendly = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int p = Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("XiaoExplosion").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
            Main.projectile[p].Center = target.Center;

            target.AddBuff(ModContent.BuffType<Buffs.DiscordInferno>(), 200);
        }

        public override void AI()
        {
            for (int num572 = 0; num572 < 5; num572++)
            {
                float num573 = Projectile.velocity.X * 0.2f * num572;
                float num574 = -(Projectile.velocity.Y * 0.2f) * num572;
                int num575 = Dust.NewDust(Vector2.Zero, Projectile.width, Projectile.height, ModContent.DustType<Dusts.Discord>(), 0f, 0f, 100, default, 1f);
                Main.dust[num575].noGravity = true;
                Main.dust[num575].velocity *= 0f;
                Dust expr_178B4_cp_0 = Main.dust[num575];
                expr_178B4_cp_0.position.X -= num573;
                Dust expr_178D3_cp_0 = Main.dust[num575];
                expr_178D3_cp_0.position.Y -= num574;
            }

            if (Projectile.frameCounter++ > 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int num468 = 0; num468 < 3; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, 1, ModContent.DustType<Dusts.DiscordLight>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default, 2.5f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
            }
            Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("XiaoExplosion").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
    }
}
