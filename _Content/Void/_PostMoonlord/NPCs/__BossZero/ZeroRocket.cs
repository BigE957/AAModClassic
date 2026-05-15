using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero
{
    public class ZeroRocket : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Void Rocket");
            Main.projFrames[Projectile.type] = 3;
        }

		public override void SetDefaults()
		{
            Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 180;
		}

        public override void AI()
        {
            if (++Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 3)
                {
                    Projectile.frame = 0;
                }
            }
            Lighting.AddLight(Projectile.Center, (255 - Projectile.alpha) * 0.9f / 255f, (255 - Projectile.alpha) * 0f / 255f, (255 - Projectile.alpha) * 0.4f / 255f);

            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            Projectile.localAI[1]++;
            if (Projectile.localAI[1] >= 60)
            {

                if (Projectile.localAI[0] == 0f)
                {
                    AdjustMagnitude(ref Projectile.velocity);
                    Projectile.localAI[0] = 1f;
                }
                Vector2 move = Vector2.Zero;
                float distance = 800f;
                bool target = false;
                for (int k = 0; k < 200; k++)
                {
                    if (Main.player[k].active)
                    {
                        Vector2 newMove = Main.player[k].Center - Projectile.Center;
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
        }

        int a = 0;

        public override void PostAI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) a++;
            if (a == 40)
            {
                Projectile.tileCollide = true;
                Projectile.netUpdate = true;
            }
            if (a < 40)
            {
                Projectile.tileCollide = false;
            }
        }

        private static void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 13f)
            {
                vector *= 12f / magnitude;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.Kill();
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int i = 0; i < 20; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.LifeDrain, 0f, 0f, 100, default, 1.2f);
                Main.dust[dustIndex].velocity *= 1.9f;
            }
            for (int i = 0; i < 10; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.2f);
                Main.dust[dustIndex].velocity *= 1.6f;
            }
            for (int i = 0; i < 5; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
        }
	}
}
