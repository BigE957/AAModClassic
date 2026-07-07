using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep._Hardmode.NPCs.__BossBiomiteCore;

public class BiomiteCore_BoulderChunk : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(261);
		Projectile.width = 16;
		Projectile.height = 16;
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
			Projectile.rotation += 0.2f * Projectile.direction;
		}
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 5; i++)
		{
			float speedX = (0f - Projectile.velocity.X) * 0.2f;
			float speedY = (0f - Projectile.velocity.Y) * 0.2f;
			Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Stone, speedX, speedY);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture = ModContent.Request<Texture2D>(Texture + Projectile.ai[1]).Value;
		BaseDrawing.DrawTexture(Main.spriteBatch, texture, 0, Projectile, lightColor, drawCentered: true);
		return false;
	}
}
