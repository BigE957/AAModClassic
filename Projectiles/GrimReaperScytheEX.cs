using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;

namespace AAMod.Projectiles
{
    public class GrimReaperScytheEX : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Grim Reaper Scythe EX");
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(274);
			Projectile.width = 60;
			Projectile.height = 52;
			Projectile.penetrate = 12;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 5;
			AIType = 274;
		}
		
		public override void AI()
		{
			if (Projectile.localAI[0] == 0f)
			{
				AdjustMagnitude(ref Projectile.velocity);
				Projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 400f;
			bool target = false;
			for (int k = 0; k < 200; k++)
			{
				if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5 && Main.npc[k].type != 488)
				{
					Vector2 newMove = Main.npc[k].Center - Projectile.Center;
					float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
			}
			if (target)
			{
				AdjustMagnitude(ref move);
				Projectile.velocity = (10 * Projectile.velocity + move) / 11f;
				AdjustMagnitude(ref Projectile.velocity);
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 6f)
			{
				vector *= 10f / magnitude;
			}
		}
		
		public override void OnKill(int timeLeft)
		{
			for (int num298 = 0; num298 < 30; num298++)
			{
				Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 184, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
			}
		}
	}
}