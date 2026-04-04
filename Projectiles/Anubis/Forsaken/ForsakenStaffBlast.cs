using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Anubis.Forsaken
{
    class ForsakenStaffBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Forsaken Staff Blast");
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 20;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, (255 - Projectile.alpha) * 1f / 255f, (255 - Projectile.alpha) * 0.7f / 255f, (255 - Projectile.alpha) * 0f / 255f);
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            for (int num339 = 0; num339 < 2; num339++)
            {
                Dust dust1;
                dust1 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.ForsakenDust>(), 0, 0, 0, Color.White, 1.5f)];
                dust1.noGravity = true;
            }
			if (Projectile.localAI[0] == 0f)
			{
                AdjustMagnitude(ref Projectile.velocity);
				Projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 200f;
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

		private static void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 6f)
			{
				vector *= 9f / magnitude;
			}
		}

        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            for (int num506 = 0; num506 < 15; num506++)
            {
                Dust dust1;
                Vector2 position = Projectile.position;
                dust1 = Main.dust[Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.ForsakenDust>(), 0, 0, 0, Color.White, 1f)];
                dust1.noGravity = true;
            }
			for (int h = 0; h < 3; h++)
			{
				Vector2 vel = new Vector2(0, -1);
				float rand = Main.rand.NextFloat() * 6.283f;
				vel = vel.RotatedBy(rand);
				vel *= 8f;
				Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, vel.X, vel.Y, ModContent.ProjectileType<ForsakenFrag>(), Projectile.damage, 0, Main.myPlayer);
			}
        }
    }
}