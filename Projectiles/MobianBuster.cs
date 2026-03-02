using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAMod.Projectiles
{
    public class MobianBuster : ModProjectile
    {
        public int counter = 0;
		public int chargeLevel = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("MobianBuster");
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 74;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
        }

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
			if (Projectile.ai[1] == 1f && Projectile.ai[0] != 1f)
			{
				Vector2 vector2 = Vector2.UnitX * 24f;
				vector2 = vector2.RotatedBy(Projectile.rotation - 1.57079637f, default);
				Vector2 value = Projectile.Center + vector2;
				for (int i = 0; i < 3; i++)
				{
					int num5 = Dust.NewDust(value - Vector2.One * 8f, 16, 16, 55, Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f, 100);
					Main.dust[num5].position.Y -= 0.3f;
					Main.dust[num5].velocity *= 0.66f;
					Main.dust[num5].noGravity = true;
					Main.dust[num5].scale = 1.4f;
				}
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

            if (counter >= 90)
            {
                SoundEngine.PlaySound(SoundID.Item93, Projectile.position);
                chargeLevel = 3;
            }

            else if (counter >= 90)
            {
                SoundEngine.PlaySound(SoundID.Item101, Projectile.position);
                chargeLevel = 2;
            }

            else if (counter >= 30)
            {
                SoundEngine.PlaySound(SoundID.Item13, Projectile.position);
                chargeLevel = 1;
            }

            else if(counter >= 30)
            {
                SoundEngine.PlaySound(SoundID.Item13, Projectile.position);
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
				float num1 = 12f;
				Vector2 vector2 = new Vector2(player.position.X + player.width * 0.5f, player.position.Y + player.height * 0.5f);
				float f1 = Main.mouseX + Main.screenPosition.X - vector2.X;
				float f2 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
				if (player.gravDir == -1.0)
					f2 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
				float num4 = (float)Math.Sqrt(f1 * (double)f1 + f2 * (double)f2);
				float num5;
				if (float.IsNaN(f1) && float.IsNaN(f2) || f1 == 0.0 && f2 == 0.0)
				{
					f1 = player.direction;
					f2 = 0.0f;
					num5 = num1;
				}
				else
					num5 = num1 / num4;
				float SpeedX = f1 * num5;
				float SpeedY = f2 * num5;
                switch (chargeLevel)
                {
                    case 0:
						SoundEngine.PlaySound(SoundID.Item89, Projectile.position);
						Projectile.NewProjectile(vector2.X, vector2.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("MobianShot").Type, Projectile.damage, 1f, player.whoAmI);
						break;
					case 1:
						SoundEngine.PlaySound(SoundID.Item89, Projectile.position);
                        Projectile.NewProjectile(vector2.X, vector2.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("MobianShot").Type, Projectile.damage*2, 1f, player.whoAmI);
						break;
					case 2:
						SoundEngine.PlaySound(SoundID.Item88, Projectile.position);
                        Projectile.NewProjectile(vector2.X, vector2.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("MobianShot").Type, Projectile.damage*2, 1f, player.whoAmI);
						break;
					case 3:
						SoundEngine.PlaySound(SoundID.Item88, Projectile.position);
                        Projectile.NewProjectile(vector2.X, vector2.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("MobianShotCharged").Type, Projectile.damage*8, 1f, player.whoAmI);
						break;
                }
            }
        }
    }
}
