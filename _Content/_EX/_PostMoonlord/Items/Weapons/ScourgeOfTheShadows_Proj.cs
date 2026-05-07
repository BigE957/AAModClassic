using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class ScourgeOfTheShadows_Proj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.alpha = 255;
			Projectile.width = 34;
			Projectile.height = 34;
			Projectile.friendly = true;
			Projectile.penetrate = 5;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.ignoreWater = true;
			Projectile.extraUpdates = 1;
			Projectile.timeLeft = 300;
        }
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Projectile.velocity.X != oldVelocity.X)
			{
				Projectile.velocity.X = -oldVelocity.X;
			}
			if (Projectile.velocity.Y != oldVelocity.Y)
			{
				Projectile.velocity.Y = -oldVelocity.Y;
				
			}
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0)
			{
				Projectile.Kill();
			}
			return false;
		}
		
        public override void AI()
        {
			if (Projectile.alpha <= 200)
			{
				int num3;
				for (int num20 = 0; num20 < 4; num20 = num3 + 1)
				{
					float num21 = Projectile.velocity.X / 4f * num20;
					float num22 = Projectile.velocity.Y / 4f * num20;
					int num23 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.ScourgeOfTheCorruptor, 0f, 0f, 0);
					Main.dust[num23].position.X = Projectile.Center.X - num21;
					Main.dust[num23].position.Y = Projectile.Center.Y - num22;
					Dust dust = Main.dust[num23];
					dust.velocity *= 0f;
					Main.dust[num23].scale = 0.7f;
					num3 = num20;
				}
			}
			Projectile.alpha -= 50;
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 0.785f;
			
			if (Main.rand.NextBool(30))
			{
				for (int num627 = 0; num627 < 2; num627++)
				{
					float num628 = Main.rand.Next(-35, 36) * 0.02f;
					float num629 = Main.rand.Next(-35, 36) * 0.02f;
					num628 *= 10f;
					num629 *= 10f;
					int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, num628, num629, ProjectileID.TinyEater, Projectile.damage, (int)(Projectile.knockBack * 0.35), Main.myPlayer, 0f, 0f);
					Main.projectile[p].timeLeft = 180;
				}
			}
        }
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.immune[Projectile.owner] = 1;
			Projectile.Kill();
		}
		
		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.NPCHit1, Projectile.position);
			int num3;
			for (int num622 = 0; num622 < 20; num622 = num3 + 1)
			{
				int num623 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.ScourgeOfTheCorruptor, 0f, 0f, 0);
				Dust dust = Main.dust[num623];
				dust.scale *= 1.1f;
				Main.dust[num623].noGravity = true;
				num3 = num622;
			}
			for (int num624 = 0; num624 < 30; num624 = num3 + 1)
			{
				int num625 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.ScourgeOfTheCorruptor, 0f, 0f, 0);
				Dust dust = Main.dust[num625];
				dust.velocity *= 2.5f;
				dust = Main.dust[num625];
				dust.scale *= 0.8f;
				Main.dust[num625].noGravity = true;
				num3 = num624;
			}
			if (Projectile.owner == Main.myPlayer)
			{
				int num626 = 2;
				if (Main.rand.NextBool(10))
				{
					num626++;
				}
				if (Main.rand.NextBool(10))
				{
					num626++;
				}
				if (Main.rand.NextBool(10))
				{
					num626++;
				}
				for (int num627 = 0; num627 < num626; num627 = num3 + 1)
				{
					float num628 = Main.rand.Next(-35, 36) * 0.02f;
					float num629 = Main.rand.Next(-35, 36) * 0.02f;
					num628 *= 10f;
					num629 *= 10f;
					int p = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X, Projectile.position.Y, num628, num629, ProjectileID.TinyEater, Projectile.damage*3, (int)(Projectile.knockBack * 0.35), Main.myPlayer, 0f, 0f);
					num3 = num627;
					Main.projectile[p].timeLeft = 240;
				}
			}
		}
    }
}