using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terrarium.__Hardmode.NPCs._BossBiomiteCore.Projectiles;

public class Rainbow : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = 12;
		Projectile.height = 12;
		Projectile.penetrate = -1;
		Projectile.alpha = 255;
		Projectile.ignoreWater = true;
		Projectile.scale = 1.25f;
		Projectile.hostile = true;
		Projectile.friendly = false;
	}

	public override void AI()
	{
		float num = (float)Main.DiscoR / 255f;
		float num2 = (float)Main.DiscoG / 255f;
		float num3 = (float)Main.DiscoB / 255f;
		num = (num + 1f) / 2f;
		num2 = (num2 + 1f) / 2f;
		num3 = (num3 + 1f) / 2f;
		num *= Projectile.light;
		num2 *= Projectile.light;
		num3 *= Projectile.light;
		Lighting.AddLight((int)((Projectile.position.X + (float)(Projectile.width / 2)) / 16f), (int)((Projectile.position.Y + (float)(Projectile.height / 2)) / 16f), num, num2, num3);
		int num4 = 40;
		if (Projectile.ai[1] == 0f)
		{
			if (Projectile.owner == Main.myPlayer)
			{
				Projectile.localAI[0] += 1f;
				if (Projectile.localAI[0] > 4f)
				{
					Projectile.localAI[0] = 3f;
					int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0.001f, Projectile.velocity.Y * 0.001f, ProjectileID.RainbowBack, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 1f);
					if (p >= 0)
					{
						Main.projectile[p].hostile = true;
                        Main.projectile[p].friendly = false;
						Main.projectile[p].extraUpdates = 4;
                    }
				}
				if (Projectile.timeLeft > num4)
				{
					Projectile.timeLeft = num4;
				}
			}
			float num5 = 1f;
			if (Projectile.velocity.Y < 0f)
			{
				num5 -= Projectile.velocity.Y / 3f;
			}
			Projectile.ai[0] += num5;
			if (Projectile.ai[0] > 30f)
			{
				Projectile.velocity.Y = Projectile.velocity.Y + 0.5f;
				if (Projectile.velocity.Y > 0f)
				{
					Projectile.velocity.X = Projectile.velocity.X * 0.95f;
				}
				else
				{
					Projectile.velocity.X = Projectile.velocity.X * 1.05f;
				}
			}
			float x = Projectile.velocity.X;
			float y = Projectile.velocity.Y;
			float num6 = (float)Math.Sqrt(x * x + y * y);
			num6 = 15.95f * Projectile.scale / num6;
			x *= num6;
			y *= num6;
			Projectile.velocity.X = x;
			Projectile.velocity.Y = y;
			Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) - 1.57f;
			return;
		}
		if (Projectile.localAI[0] == 0f)
		{
			if (Projectile.velocity.X > 0f)
			{
				Projectile.spriteDirection = -1;
				Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) - 1.57f;
			}
			else
			{
				Projectile.spriteDirection = 1;
				Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) - 1.57f;
			}
			Projectile.localAI[0] = 1f;
			Projectile.timeLeft = num4;
		}
		Projectile.velocity.X = Projectile.velocity.X * 0.98f;
		Projectile.velocity.Y = Projectile.velocity.Y * 0.98f;
		if (Projectile.rotation == 0f)
		{
			Projectile.alpha = 255;
		}
		else if (Projectile.timeLeft < 10)
		{
			Projectile.alpha = 255 - (int)(255f * (float)Projectile.timeLeft / 10f);
		}
		else if (Projectile.timeLeft > num4 - 10)
		{
			int num7 = num4 - Projectile.timeLeft;
			Projectile.alpha = 255 - (int)(255f * (float)num7 / 10f);
		}
		else
		{
			Projectile.alpha = 0;
		}
	}

    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
		width = 6;
		height = 6;
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		if (Projectile.ai[1] == 0f)
			return Color.Transparent;

		int num = 255 - Projectile.alpha;
		int num2 = 255 - Projectile.alpha;
		int num3 = 255 - Projectile.alpha;
		return new Color(num, num2, num3, 0);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
        int num = 18;
		int num2 = -10;
		float num3 = (float)tex.Width - (float)Projectile.width * 0.5f + (float)Projectile.width * 0.5f;
		Main.spriteBatch.Draw(tex, new Vector2(Projectile.position.X - Main.screenPosition.X + num3 + (float)num2, Projectile.position.Y - Main.screenPosition.Y + (float)(Projectile.height / 2) + Projectile.gfxOffY), (Rectangle?)new Rectangle(0, 0, tex.Width, tex.Height), Projectile.GetAlpha(lightColor), Projectile.rotation, new Vector2(num3, (float)(Projectile.height / 2 + num)), Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}
}
