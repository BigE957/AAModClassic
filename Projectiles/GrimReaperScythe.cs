using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using Terraria.ID;

namespace AAModClassic.Projectiles
{
    public class GrimReaperScythe : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Grim Reaper Scythe");
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(274);
			Projectile.width = 60;
			Projectile.height = 52;
			Projectile.penetrate = 10;
			AIType = ProjectileID.DeathSickle;
		}
		
		public override void AI()
		{
			if (Projectile.localAI[0] == 0f)
			{
				AdjustMagnitude(ref Projectile.velocity);
				Projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 300f;
			bool target = false;
			for (int k = 0; k < 200; k++)
			{
				if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5 && Main.npc[k].type != NPCID.TargetDummy)
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
				vector *= 9f / magnitude;
			}
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			target.immune[Projectile.owner] = 6;
		}
		
		public override void OnKill(int timeLeft)
		{
			for (int num298 = 0; num298 < 30; num298++)
			{
				Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.ScourgeOfTheCorruptor, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
			}
		}
	}
}