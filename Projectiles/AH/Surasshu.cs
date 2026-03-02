using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.AH
{
    public class Surasshu : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Surasshu");
			Main.projFrames[Projectile.type] = 28;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 136;
            Projectile.height = 66;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        public override void AI()
        {
        	Player player = Main.player[Projectile.owner];
			float num = 0f;
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			if (Projectile.spriteDirection == -1)
			{
				num = 3.14159274f;
			}
			if (++Projectile.frame >= Main.projFrames[Projectile.type])
			{
				Projectile.frame = 0;
			}
			Projectile.soundDelay--;
			if (Projectile.soundDelay <= 0)
			{
				SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
				Projectile.soundDelay = 24;
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
			Lighting.AddLight(vector14, 1f, 0.2f, 2f);
			if (Main.rand.Next(3) == 0)
			{
				int num30 = Dust.NewDust(vector14 - Projectile.Size / 2f, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AbyssDust>(), Projectile.velocity.X, Projectile.velocity.Y, 100);
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
        
        public override Color? GetAlpha(Color lightColor)
        {
            return lightColor;
        }
    }
}