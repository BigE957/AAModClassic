using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero.Weapons
{
    // to investigate: Projectile.Damage, (8843)
    class InfinityBlade_Rift : ModProjectile
	{
        public override string GlowTexture => Texture + "_Glow";

        public override void SetDefaults()
		{
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.alpha = 100;
            Projectile.light = 0.2f;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            Projectile.tileCollide = false;
            Projectile.scale = 0.9f;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 300;

        }
        public override void AI()
        {
            float num472 = Projectile.Center.X;
            float num473 = Projectile.Center.Y;
            float num474 = 400f;
            bool flag17 = false;
            for (int num475 = 0; num475 < 200; num475++)
            {
                if (Main.npc[num475].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[num475].Center, 1, 1))
                {
                    float num476 = Main.npc[num475].position.X + Main.npc[num475].width / 2;
                    float num477 = Main.npc[num475].position.Y + Main.npc[num475].height / 2;
                    float num478 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num476) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num477);
                    if (num478 < num474)
                    {
                        num474 = num478;
                        num472 = num476;
                        num473 = num477;
                        flag17 = true;
                    }
                }
            }
            if (flag17)
            {
                float num483 = 20f;
                Vector2 vector35 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                float num484 = num472 - vector35.X;
                float num485 = num473 - vector35.Y;
                float num486 = (float)Math.Sqrt((double)(num484 * num484 + num485 * num485));
                num486 = num483 / num486;
                num484 *= num486;
                num485 *= num486;
                Projectile.velocity.X = (Projectile.velocity.X * 20f + num484) / 21f;
                Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num485) / 21f;
                Projectile.rotation += Projectile.direction * 0.8f;
                Projectile.ai[0] += 1f;
                if (Projectile.ai[0] >= 30f)
                {
                    if (Projectile.ai[0] < 100f)
                    {
                        Projectile.velocity *= 1.00f;
                    }
                    else
                    {
                        Projectile.ai[0] = 200f;
                    }
                }
                for (int num257 = 0; num257 < 2; num257++)
                {
                    int num258 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.VoidDust_Unreleased>(), 0f, 0f, 100, new Color(120, 0, 30), 1f);
                    Main.dust[num258].noGravity = true;
                }
                return;
            }
            Projectile.rotation += Projectile.direction * 0.8f;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 30f)
            {
                if (Projectile.ai[0] < 100f)
                {
                    Projectile.velocity *= 1.00f;
                }
                else
                {
                    Projectile.ai[0] = 200f;
                }
            }
            for (int num257 = 0; num257 < 2; num257++)
            {
                int num258 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.VoidDust_Unreleased>(), 0f, 0f, 100, new Color(120, 0, 30), 1f);
                Main.dust[num258].noGravity = true;
            }
            return;
        }
    }
}
