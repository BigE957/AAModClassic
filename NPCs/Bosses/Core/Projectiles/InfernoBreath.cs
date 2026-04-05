using AAModClassic.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Core.Projectiles;

internal class InfernoBreath : ModProjectile
{
	public override string Texture => "AAModClassic/BlankTex";

	public override void SetStaticDefaults()
	{
		//((ModProjectile)this).DisplayName.SetDefault("Fire Breath");
	}

	public override void SetDefaults()
	{
		Projectile.width = 10;
		Projectile.height = 10;
		Projectile.hostile = true;
		Projectile.friendly = false;
		Projectile.damage = 35;
		Projectile.ignoreWater = true;
		Projectile.penetrate = 1;
		Projectile.alpha = 255;
		Projectile.timeLeft = 100;
	}

	public override void AI()
	{
		if (Projectile.timeLeft > 60)
		{
			Projectile.timeLeft = 60;
		}
		if (Projectile.ai[0] > 7f)
		{
			float num = 1f;
			if (Projectile.ai[0] == 8f)
			{
				num = 0.25f;
			}
			else if (Projectile.ai[0] == 9f)
			{
				num = 0.5f;
			}
			else if (Projectile.ai[0] == 10f)
			{
				num = 0.75f;
			}
			Projectile.ai[0] += 1f;
			int type = 6;
			for (int i = 0; i < 1; i++)
			{
				int num2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, type, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
				if (Main.rand.Next(3) != 0)
				{
					Main.dust[num2].noGravity = true;
					Main.dust[num2].scale *= 3f;
					Dust dust = Main.dust[num2];
					dust.velocity.X = dust.velocity.X * 2f;
					Dust dust2 = Main.dust[num2];
					dust2.velocity.Y = dust2.velocity.Y * 2f;
				}
				Main.dust[num2].scale *= 1.5f;
				Dust dust3 = Main.dust[num2];
				dust3.velocity.X = dust3.velocity.X * 1.2f;
				Dust dust4 = Main.dust[num2];
				dust4.velocity.Y = dust4.velocity.Y * 1.2f;
				Main.dust[num2].scale *= num;
			}
		}
		else
		{
			Projectile.ai[0] += 1f;
		}
		Projectile.rotation += 0.3f * (float)Projectile.direction;
	}

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
		target.AddBuff(ModContent.BuffType<Buffs.DragonFire>(), 300);
	}
}
