using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep._Hardmode.NPCs.__BossBiomiteCore.Projectiles;

public class Rock : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(261);
		Projectile.width = 32;
		Projectile.height = 32;
		Projectile.aiStyle = -1;
		Projectile.hostile = true;
		Projectile.penetrate = 1;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = true;
	}

	public override void AI()
	{
		if (Projectile.velocity.X > 0f)
		{
			Projectile.direction = 1;
		}
		else
		{
			Projectile.direction = -1;
		}
		if (Projectile.velocity.X != 0f)
		{
			Projectile.rotation += 0.2f * (float)Projectile.direction;
		}

		Projectile.velocity.Y += 0.4f;
	}

	public override void OnKill(int timeLeft)
	{
		for (float num = 0f; num < 5f; num += 1f)
		{
			Dust dust = Dust.NewDustDirect(Projectile.Bottom, Projectile.width, 1, DustID.Stone);
			dust.alpha = 0;
			dust.velocity.Y -= 3f;
			dust.velocity.X *= 0.5f;
			dust.fadeIn = 0.5f + Main.rand.NextFloat() * 0.5f;
		}
		SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
		for (int i = 0; i < Main.rand.Next(5, 10); i++)
		{
			int num2 = Main.rand.Next(-6, 6);
			int num3 = -Main.rand.Next(3, 5);
			int num4 = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position, new Vector2((float)num2, (float)num3), ModContent.ProjectileType<RockChunk>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, (float)Main.rand.Next(3));
			Main.projectile[num4].Center = Projectile.Center - new Vector2(0f, 25f);
		}
	}
}
