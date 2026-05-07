using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert._PostMoonlord._BossAnubisA
{
    public class AnubisFireball : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Anubis Fireball");
			Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			Projectile.width = 36;
			Projectile.height = 36;
			Projectile.hostile = true;
			Projectile.ignoreWater = true;
			Projectile.penetrate = 1;
			Projectile.alpha = 50;
			Projectile.timeLeft = 150;
			CooldownSlot = 1;
		}

		public override void AI()
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 4)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 3)
			{
				Projectile.frame = 0;
			}
			Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.9f) / 255f, ((255 - Projectile.alpha) * 0.2f) / 255f);

            if (Projectile.ai[0]++ > 180)
            {
                Projectile.Kill();
            }
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}

		public override void OnKill(int timeLeft)
		{
			float spread = 45f * 0.0174f;
			double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - spread / 2;
			double deltaAngle = spread / 6f;
            for (int i = 0; i < 6; i++)
            {
                double offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 7f), (float)(Math.Cos(offsetAngle) * 7f), ModContent.ProjectileType<CurseFlame>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 1f);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 7f), (float)(-Math.Cos(offsetAngle) * 7f), ModContent.ProjectileType<CurseFlame>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 1f);
            }
            for (int dust = 0; dust < 5; dust++)
			{
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f);
			}
		}
	}
}
