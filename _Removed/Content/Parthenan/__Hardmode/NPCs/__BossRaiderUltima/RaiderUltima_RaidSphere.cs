using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossRaiderUltima
{
    public class RaiderUltima_RaidSphere : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 11;
        }

        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.friendly = false;
            Projectile.tileCollide = true;
            AIType = ProjectileID.ThrowingKnife;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
        }

        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.Length() * 0.025f;
            Projectile.velocity.Y += .15f;
        }
        
        public override void OnKill(int timeLeft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, 1, ModContent.DustType<Dusts.FulguriteDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.FulguriteDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y + 20, 0, 0, ModContent.ProjectileType<RaiderUltima_RaidShock>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 10)
                    Projectile.frame = 0;
            }
            return true;
        }
    }
}