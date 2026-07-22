using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Ammo;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons
{
    public class RadiantDawn_Holdout : ModProjectile
    {
        public int counter = 0;
		public int chargeLevel = 0;

        public static Asset<Texture2D> Glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Radiant Dawn");

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 32;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
        }

        public Color GlowColor = AAColor.Akuma;

        public override void AI()
        {
			Player player = Main.player[Projectile.owner];
			
			float num = 1.57079637f;
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			Projectile.ai[0] += 1f;
			int num2 = 0;
			if (Projectile.ai[0] >= 30f)
			{
				num2++;
			}
			if (Projectile.ai[0] >= 60f)
			{
				num2++;
			}
			if (Projectile.ai[0] >= 90f)
			{
				num2++;
			}
			int num3 = 24;
			int num4 = 6;
			Projectile.ai[1] += 1f;
			bool flag = false;
			if (Projectile.ai[1] >= num3 - num4 * num2)
			{
				Projectile.ai[1] = 0f;
				flag = true;
			}
			if (flag && Main.myPlayer == Projectile.owner)
			{
				if (player.channel && !player.noItems && !player.CCed)
				{
					float scaleFactor = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
					Vector2 vector3 = vector;
					Vector2 value2 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - vector3;
					if (player.gravDir == -1f)
					{
						value2.Y = Main.screenHeight - Main.mouseY + Main.screenPosition.Y - vector3.Y;
					}
					Vector2 vector4 = Vector2.Normalize(value2);
					if (float.IsNaN(vector4.X) || float.IsNaN(vector4.Y))
					{
						vector4 = -Vector2.UnitY;
					}
					vector4 *= scaleFactor;
					if (vector4.X != Projectile.velocity.X || vector4.Y != Projectile.velocity.Y)
					{
						Projectile.netUpdate = true;
					}
					Projectile.velocity = vector4;
					float scaleFactor2 = 14f;
					int num7 = 7;
				
					vector3 = Projectile.Center + new Vector2(Main.rand.Next(-num7, num7 + 1), Main.rand.Next(-num7, num7 + 1));
					Vector2 vector5 = Vector2.Normalize(Projectile.velocity) * scaleFactor2;
					vector5 = vector5.RotatedBy(Main.rand.NextDouble() * 0.19634954631328583 - 0.098174773156642914, default);
					if (float.IsNaN(vector5.X) || float.IsNaN(vector5.Y))
					{
						vector5 = -Vector2.UnitY;
					}
				}
			}
			Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f;
			Projectile.rotation = Projectile.velocity.ToRotation() + num;
			Projectile.spriteDirection = Projectile.direction;
			Projectile.timeLeft = 2;
			player.ChangeDir(Projectile.direction);
			player.heldProj = Projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * Projectile.direction, Projectile.velocity.X * Projectile.direction);

			counter++;

            if (counter >= 60)
            {
                GlowColor = AAColor.AkumaA;
                chargeLevel = 2;
            }
            else if (counter >= 40)
            {
                GlowColor = Color.Goldenrod;
                chargeLevel = 1;
            }
            else if (counter >= 20)
            {
                chargeLevel = 0;
            }

            if (!player.channel)
			{
				Projectile.Kill();
			}
        }

        public override void OnKill(int timeLeft)
        {
			Player player = Main.player[Projectile.owner];
            if (Projectile.owner == Main.myPlayer)
            {
				int type = 0;
				for (int i = 54; i < 58; i++)
				{
					if (player.inventory[i].ammo == AmmoID.Arrow && player.inventory[i].stack > 0)
					{
						type = player.inventory[i].shoot;
						if (player.inventory[i].consumable)
							player.inventory[i].stack--;
						break;
					}
				}
				int num122 = 1;
				switch (chargeLevel)
				{
					case 0:
						SoundEngine.PlaySound(SoundID.Item5, Projectile.Center);
						num122 = 1;
						break;
					case 1:
						SoundEngine.PlaySound(SoundID.Item5, Projectile.Center);
						num122 = 3;
						break;
					case 2:
						SoundEngine.PlaySound(SoundID.Item5, Projectile.Center);
						num122 = 6;
						break;
				}
				float num121 = 0.314159274f;
				Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
				float num82 = Main.mouseX + Main.screenPosition.X - vector2.X;
				float num83 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
				
				Vector2 vector21 = new Vector2(player.position.X + player.width * 0.5f, player.position.Y + player.height * 0.5f);
				float f1 = Main.mouseX + Main.screenPosition.X - vector21.X;
				float f2 = Main.mouseY + Main.screenPosition.Y - vector21.Y;
				if (player.gravDir == -1.0)
					f2 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector21.Y;
				float num4 = (float)Math.Sqrt(f1 * (double)f1 + f2 * (double)f2);
				float num5;
				if (float.IsNaN(f1) && float.IsNaN(f2) || f1 == 0.0 && f2 == 0.0)
				{
					f1 = player.direction;
					f2 = 0.0f;
					num5 = num121;
				}
				else
					num5 = num121 / num4;
				float SpeedX = f1 * num5;
				float SpeedY = f2 * num5;
				
				
				Vector2 vector14 = new Vector2(SpeedX, SpeedY);
				vector14.Normalize();
				vector14 *= 40f;
				bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector14, 0, 0);
                if (chargeLevel >= 2)
                {
                    type = ModContent.ProjectileType<DaybreakArrow_Proj>();
                }
                for (int num123 = 0; num123 < num122; num123++)
                {
                    float num124 = num123 - (num122 - 1f) / 2f;
                    Vector2 vector15 = vector14.RotatedBy(num121 * num124, default);
                    if (!flag11)
                    {
                        vector15 -= vector14;
                    }
                    int num125 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), vector2.X + vector15.X, vector2.Y + vector15.Y, num82, num83, type, Projectile.damage, 1f, player.whoAmI, 0.0f, 0.0f);
                    Main.projectile[num125].noDropItem = true;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D glowTex = Glowmask.Value;
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Width(), TextureAssets.Projectile[Projectile.type].Height());
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.direction, 1, frame, lightColor, true);
            BaseDrawing.DrawTexture(Main.spriteBatch, glowTex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.direction, 1, frame, GlowColor, true);
            return false;
        }
    }
}
