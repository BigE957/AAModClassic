using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria;
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic._Content._Dev.__Hardmode.Items.Weapons;
using AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahRabbitA;
using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero.Awakened;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Accessories
{
	public class TheBookOfTheLaw_ClawSlash : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Claw Slash");
			Main.projFrames[Projectile.type] = 28;
		}
		public override void SetDefaults()
		{
			Projectile.width = 68;
			Projectile.height = 64;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.ownerHitCheck = true;
			Projectile.timeLeft = 30;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 12;
		}

		public override Color? GetAlpha(Color lightColor)
        {
            return Color.IndianRed;
        }

		public override void AI()
        {
			Player player = Main.LocalPlayer;
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			float position1 = Main.mouseX + Main.screenPosition.X - vector.X;
			float position2 = Main.mouseY + Main.screenPosition.Y - vector.Y;
			Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f;
			if(player.direction == -1)
			{
				Projectile.rotation = (float)Math.Atan2(position2 * player.direction, position1 * player.direction) - player.fullRotation + MathHelper.ToRadians(180f);
			}
			else
			{
				Projectile.rotation = (float)Math.Atan2(position2 * player.direction, position1 * player.direction) + player.fullRotation;
			}
			int num1 = Projectile.frame + 1;
			Projectile.frame = num1;
			if (num1 >= Main.projFrames[Projectile.type])
			{
				Projectile.frame = 0;
			}
			Projectile.soundDelay--;
			if (Projectile.soundDelay <= 0)
			{
				SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
				Projectile.soundDelay = 12;
			}
			if(Main.mouseLeft)
			{
				float scaleFactor6 = 15f;
				Vector2 vector20 = Main.MouseWorld - player.RotatedRelativePoint(player.MountedCenter, true);
				vector20.Normalize();
				if (vector20.HasNaNs())
				{
					vector20 = Vector2.UnitX * player.direction;
				}
				vector20 *= scaleFactor6;
				if (vector20.X != Projectile.velocity.X || vector20.Y != Projectile.velocity.Y)
				{
					Projectile.netUpdate = true;
				}
				Projectile.velocity = vector20;
			}
			else
			{
				Projectile.Kill();
			}
			Vector2 vector21 = Projectile.Center + Projectile.velocity * 3f;
			Lighting.AddLight(vector21, 0.8f, 0.8f, 0.8f);
			if (Main.rand.NextBool(3))
			{
				int num2 = Dust.NewDust(vector21 - Projectile.Size / 2f, Projectile.width, Projectile.height, DustID.WhiteTorch, Projectile.velocity.X, Projectile.velocity.Y, 100, default, 2f);
				Main.dust[num2].noGravity = true;
				Main.dust[num2].position -= Projectile.velocity;
			}
			player.ChangeDir(Main.projectile[Projectile.whoAmI].direction);

			for(int i=0; i < 200; i++)
			{
				if(Projectile.Hitbox.Intersects(Main.npc[i].Hitbox))
				{
					Main.npc[i].immune[Projectile.owner] = 0;
				}
			}
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			
			Player player = Main.LocalPlayer;
            //damage = (int)((player.GetModPlayer<InvokerPlayer>().DarkCaligula? 1000 : 500) * (player.minionDamage + player.allDamage));
            modifiers.SetCrit();
			if(player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().DarkCaligula)
			{
                string Lifelength = target.lifeMax + "";
				int regen = Main.rand.NextBool(2) ? 2*(Lifelength.Length + 1) : Lifelength.Length + 1;
				player.statLife += regen;
				player.HealEffect(regen, true);
				if (player.statLife > player.statLifeMax2)
				{
					player.statLife = player.statLifeMax2;
				}
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.immune[Projectile.owner] = 5;
		}
	}
}