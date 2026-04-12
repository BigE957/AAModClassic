using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class Voidslash : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Valkyrie Slash");
			Main.projFrames[Projectile.type] = 28;
		}
    	
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Arkhalis);
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
			Projectile.netUpdate = true;
        }

        public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			float num = 0f;
			if (Projectile.spriteDirection == -1)
			{
				num = 3.14159274f;
			}
			if (++Projectile.frameCounter > 2)
			{
				Projectile.frameCounter = 0;
				Projectile.frame++;
			}
			if (Projectile.frame >= Main.projFrames[Projectile.type])
			{
				Projectile.frame = 0;
			}
			Projectile.soundDelay--;
			if (Projectile.soundDelay <= 0)
			{
				SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
				Projectile.soundDelay = 12;
			}
			if (Main.myPlayer == Projectile.owner)
			{
				if (player.channel && !player.noItems && !player.CCed)
				{
					float scaleFactor6 = 1f;
					if (player.inventory[player.selectedItem].shoot == Projectile.type)
					{
						scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
					}
					Vector2 vector13 = Main.MouseWorld - vector;
					vector13.Normalize();
					if (vector13.HasNaNs())
					{
						vector13 = Vector2.UnitX * player.direction;
					}
					vector13 *= scaleFactor6;
					if (vector13.X != Projectile.velocity.X || vector13.Y != Projectile.velocity.Y)
					{
						Projectile.netUpdate = true;
					}
					Projectile.velocity = vector13;
				}
				else
				{
					Projectile.Kill();
				}
			}
			Vector2 vector14 = Projectile.Center + Projectile.velocity * 3f;
			Lighting.AddLight(vector14, 0.5f, 0.3f, 0.32f);
			if (Main.rand.NextBool(3))
			{
				int num30 = Dust.NewDust(vector14 - Projectile.Size / 2f, Projectile.width, Projectile.height, DustID.WhiteTorch, Projectile.velocity.X, Projectile.velocity.Y, 100, Color.Red, 2f);
				Main.dust[num30].noGravity = true;
				Main.dust[num30].position -= Projectile.velocity;
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
		}

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return projHitbox.Intersects(targetHitbox);
        }

        public override Color? GetAlpha(Color lightColor)
		{
			Color value = Color.Lerp(lightColor, Color.White, 0.85f);
			value.A = 128;
			return value * (1f - Projectile.alpha / 255f);
		}
    }
}