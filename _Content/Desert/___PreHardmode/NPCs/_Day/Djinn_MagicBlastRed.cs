using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.___PreHardmode.NPCs._Day
{
    public class Djinn_MagicBlastRed : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Magic Blast");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = false;
            Projectile.penetrate = 1;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }


        public override void AI()
        {

            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;
            Projectile.alpha = (int)Projectile.localAI[0] * 2;


            Vector2 position = Projectile.Center + Vector2.One * -20f;
            int num84 = 40;
            int height3 = num84;

            for (int num85 = 0; num85 < 4; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.InfinityOverloadR>(), 0f, 0f, 100, default, 1.2f);
                Main.dust[num86].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num86].noGravity = true;
            }

            if (Projectile.localAI[0] > 200f)
            {
                Projectile.Kill();
            }

        }

        public override void OnKill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.InfinityOverloadR>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.InfinityOverloadR>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X, Projectile.position.Y, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<Djinn_BurstRed>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 10)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }
            return false;
        }


        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire, 600);
        }

    }
}