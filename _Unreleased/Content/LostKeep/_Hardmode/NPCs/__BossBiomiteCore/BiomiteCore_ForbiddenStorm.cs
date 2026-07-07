using System;
using AAModClassic.Assets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep._Hardmode.NPCs.__BossBiomiteCore;

public class BiomiteCore_ForbiddenStorm : ModProjectile
{
	public override string Texture => AssetDirectory.General.Nothing;

	public override void SetStaticDefaults()
	{
		//((ModProjectile)this).DisplayName.SetDefault("Forbidden Storm");
	}

	public override void SetDefaults()
	{
		Projectile.width = 14;
		Projectile.height = 14;
		Projectile.aiStyle = ProjAIStyleID.AncientStormMark;
		Projectile.penetrate = 1;
		Projectile.timeLeft = 900;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.alpha = 255;
		Projectile.hostile = true;
	}

	public override void AI()
	{
		Color newColor = new(255, 255, 255);
		if (Projectile.soundDelay == 0)
		{
			Projectile.soundDelay = -1;
			SoundEngine.PlaySound(SoundID.Item60, Projectile.Center);
		}
		if (Projectile.localAI[1] < 30f)
		{
			Vector2 val = default;
			Vector2 val2 = default;
			for (int i = 0; i < 1; i++)
			{
				float num = -0.5f;
				float num2 = 0.9f;
				float num3 = Main.rand.NextFloat();
				val = new(MathHelper.Lerp(0.1f, 1f, Main.rand.NextFloat()), MathHelper.Lerp(num, num2, num3));
				val.X *= MathHelper.Lerp(2.2f, 0.6f, num3);
				val.X *= -1f;
				val2 = new(2f, 10f);
				Vector2 position = Projectile.Center + new Vector2(60f, 200f) * val * 0.5f + val2;
				Dust dust = Main.dust[Dust.NewDust(position, 0, 0, DustID.Sandnado)];
				dust.position = position;
				dust.customData = Projectile.Center + val2;
				dust.fadeIn = 1f;
				dust.scale = 0.3f;
				if (val.X > -1.2f)
				{
					dust.velocity.X = 1f + Main.rand.NextFloat();
				}
				dust.velocity.Y = Main.rand.NextFloat() * -0.5f - 1f;
			}
		}
		if (Projectile.localAI[0] == 0f)
		{
			Projectile.localAI[0] = 0.8f;
			Projectile.direction = 1;
			Point val3 = Projectile.Center.ToTileCoordinates();
			Projectile.Center = new Vector2(val3.X * 16 + 8, val3.Y * 16 + 8);
		}
		Projectile.rotation = Projectile.localAI[1] / 40f * ((float)Math.PI * 2f) * Projectile.direction;
		if (Projectile.localAI[1] < 33f)
		{
			if (Projectile.alpha > 0)
			{
				Projectile.alpha -= 8;
			}
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
		}
		if (Projectile.localAI[1] > 103f)
		{
			if (Projectile.alpha < 255)
			{
				Projectile.alpha += 16;
			}
			if (Projectile.alpha > 255)
			{
				Projectile.alpha = 255;
			}
		}
		if (Projectile.alpha == 0)
		{
			Lighting.AddLight(Projectile.Center, newColor.ToVector3() * 0.5f);
		}
		for (int j = 0; j < 2; j++)
		{
			if (Main.rand.NextBool(10))
			{
				Vector2 val4 = Vector2.UnitY.RotatedBy(j * (float)Math.PI).RotatedBy(Projectile.rotation);
				Dust obj = Main.dust[Dust.NewDust(Projectile.Center, 0, 0, DustID.RainbowMk2, 0f, 0f, 225, newColor, 1.5f)];
				obj.noGravity = true;
				obj.noLight = true;
				obj.scale = Projectile.Opacity * Projectile.localAI[0];
				obj.position = Projectile.Center;
				obj.velocity = val4 * 2.5f;
			}
		}
		for (int k = 0; k < 2; k++)
		{
			if (Main.rand.NextBool(10))
			{
				Vector2 val5 = Vector2.UnitY.RotatedBy(k * (float)Math.PI);
				Dust obj2 = Main.dust[Dust.NewDust(Projectile.Center, 0, 0, DustID.RainbowMk2, 0f, 0f, 225, newColor, 1.5f)];
				obj2.noGravity = true;
				obj2.noLight = true;
				obj2.scale = Projectile.Opacity * Projectile.localAI[0];
				obj2.position = Projectile.Center;
				obj2.velocity = val5 * 2.5f;
			}
		}
		if (Projectile.localAI[1] < 33f || Projectile.localAI[1] > 87f)
		{
			Projectile.scale = Projectile.Opacity / 2f * Projectile.localAI[0];
		}
		Projectile.velocity = Vector2.Zero;
		Projectile.localAI[1] += 1f;
		if (Projectile.localAI[1] == 60f && Projectile.owner == Main.myPlayer)
		{
			int num4 = 40;
			if (Main.expertMode)
			{
				num4 = 35;
			}
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileID.SandnadoHostile, num4, 3f, Projectile.owner, 0f, 0f);
		}
		if (Projectile.localAI[1] >= 120f)
		{
			Projectile.Kill();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D val = TextureAssets.Projectile[ProjectileID.SandnadoHostileMark].Value;
		Vector2 val2 = Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
		Rectangle val3 = val.Frame(1, 1, 0, Projectile.frame);
		Main.spriteBatch.Draw(val, val2, (Rectangle?)val3, Projectile.GetAlpha(lightColor), 0f, val3.Size() / 2f, new Vector2(1f, 8f) * Projectile.scale, 0, 0f);
		Color alpha = Projectile.GetAlpha(lightColor);
		Vector2 val4 = val3.Size() / 2f;
		Color val5 = Main.hslToRgb(0.136f, 1f, 0.5f).MultiplyRGBA(Color.White);
		Main.spriteBatch.Draw(val, val2, (Rectangle?)val3, val5, 0f, val4, new Vector2(1f, 5f) * Projectile.scale * 2f, 0, 0f);
		Main.spriteBatch.Draw(val, val2, (Rectangle?)val3, alpha, Projectile.rotation, val4, Projectile.scale, 0, 0f);
		Main.spriteBatch.Draw(val, val2, (Rectangle?)val3, alpha, 0f, val4, new Vector2(1f, 8f) * Projectile.scale, 0, 0f);
		return false;
	}
}
