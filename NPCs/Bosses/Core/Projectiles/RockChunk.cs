using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Core.Projectiles;

public class RockChunk : ModProjectile
{
	public override string Texture => "AAModClassic/NPCs/Bosses/Core/Projectiles/RockChunk0";

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
			Projectile.rotation += 0.2f * (float)Projectile.direction;
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
		Texture2D texture = AAMod.instance.GetTexture("NPCs/Bosses/Core/Projectiles/RockChunk" + Projectile.ai[1]);
		BaseDrawing.DrawTexture(Main.spriteBatch, texture, 0, Projectile, lightColor, drawCentered: true);
		return false;
	}
}
