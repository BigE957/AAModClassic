using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Tools
{
    public class Hellfisher_Bob : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BobberHotline);
        }

        public override bool PreDrawExtras()
		{
			Lighting.AddLight(Projectile.Center, 0.216f, 0.081f, 0.047f);
			Player player = Main.player[Projectile.owner];
			if (Projectile.bobber && Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].holdStyle > 0)
			{
				float num = player.MountedCenter.X;
				float num2 = player.MountedCenter.Y;
				num2 += Main.player[Projectile.owner].gfxOffY;
				int type = Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].type;
				float gravDir = Main.player[Projectile.owner].gravDir;
				num += 43 * Main.player[Projectile.owner].direction;
				if (Main.player[Projectile.owner].direction < 0)
				{
					num -= 13f;
				}
				num2 -= 24f * gravDir;
				if (gravDir == -1f)
				{
					num2 -= 12f;
				}
				Vector2 value = new Vector2(num, num2);
				value = Main.player[Projectile.owner].RotatedRelativePoint(value + new Vector2(8f), true) - new Vector2(8f);
				float num3 = Projectile.position.X + Projectile.width * 0.5f - value.X;
				float num4 = Projectile.position.Y + Projectile.height * 0.5f - value.Y;
				Math.Sqrt(num3 * num3 + num4 * num4);
				float rotation = (float)Math.Atan2(num4, num3) - 1.57f;
				bool flag = true;
				if (num3 == 0f && num4 == 0f)
				{
					flag = false;
				}
				else
				{
					float num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
					num5 = 12f / num5;
					num3 *= num5;
					num4 *= num5;
					value.X -= num3;
					value.Y -= num4;
					num3 = Projectile.position.X + Projectile.width * 0.5f - value.X;
					num4 = Projectile.position.Y + Projectile.height * 0.5f - value.Y;
				}
				while (flag)
				{
					float num6 = 12f;
					float num7 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
					float num8 = num7;
					if (float.IsNaN(num7) || float.IsNaN(num8))
					{
						flag = false;
					}
					else
					{
						if (num7 < 20f)
						{
							num6 = num7 - 8f;
							flag = false;
						}
						num7 = 12f / num7;
						num3 *= num7;
						num4 *= num7;
						value.X += num3;
						value.Y += num4;
						num3 = Projectile.position.X + Projectile.width * 0.5f - value.X;
						num4 = Projectile.position.Y + Projectile.height * 0.1f - value.Y;
						if (num8 > 12f)
						{
							float num9 = 0.3f;
							float num10 = Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y);
							if (num10 > 16f)
							{
								num10 = 16f;
							}
							num10 = 1f - num10 / 16f;
							num9 *= num10;
							num10 = num8 / 80f;
							if (num10 > 1f)
							{
								num10 = 1f;
							}
							num9 *= num10;
							if (num9 < 0f)
							{
								num9 = 0f;
							}
							num10 = 1f - Projectile.localAI[0] / 100f;
							num9 *= num10;
							if (num4 > 0f)
							{
								num4 *= 1f + num9;
								num3 *= 1f - num9;
							}
							else
							{
								num10 = Math.Abs(Projectile.velocity.X) / 3f;
								if (num10 > 1f)
								{
									num10 = 1f;
								}
								num10 -= 0.5f;
								num9 *= num10;
								if (num9 > 0f)
								{
									num9 *= 2f;
								}
								num4 *= 1f + num9;
								num3 *= 1f - num9;
							}
						}
						rotation = (float)Math.Atan2(num4, num3) - 1.57f;
						Color color = Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(216, 81, 47, 100));
						Main.spriteBatch.Draw(TextureAssets.FishingLine.Value, new Vector2(value.X - Main.screenPosition.X + TextureAssets.FishingLine.Value.Width * 0.5f, value.Y - Main.screenPosition.Y + TextureAssets.FishingLine.Value.Height * 0.5f), new Rectangle?(new Rectangle(0, 0, TextureAssets.FishingLine.Value.Width, (int)num6)), color, rotation, new Vector2(TextureAssets.FishingLine.Value.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
					}
				}
			}
			return false;
		}
    }
}
