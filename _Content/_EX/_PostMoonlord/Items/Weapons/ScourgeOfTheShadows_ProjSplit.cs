using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class ScourgeOfTheShadows_ProjSplit : ModProjectile
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
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 50;
            }
            else
            {
                Projectile.extraUpdates = 0;
            }
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) - 1.57f;
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= 2)
            {
                Projectile.frame = 0;
            }
            for (int num363 = 0; num363 < 3; num363++)
            {
                float num364 = Projectile.velocity.X / 3f * num363;
                float num365 = Projectile.velocity.Y / 3f * num363;
                int num366 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.CursedTorch, 0f, 0f, 0);
                Main.dust[num366].position.X = Projectile.Center.X - num364;
                Main.dust[num366].position.Y = Projectile.Center.Y - num365;
                Main.dust[num366].velocity *= 0f;
                Main.dust[num366].scale = 0.5f;
            }
            float num367 = Projectile.position.X;
            float num368 = Projectile.position.Y;
            float num369 = 100000f;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] > 30f)
            {
                Projectile.ai[0] = 30f;
                for (int num370 = 0; num370 < 200; num370++)
                {
                    if (Main.npc[num370].CanBeChasedBy(this, false))
                    {
                        float num371 = Main.npc[num370].position.X + Main.npc[num370].width / 2;
                        float num372 = Main.npc[num370].position.Y + Main.npc[num370].height / 2;
                        float num373 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num371) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num372);
                        if (num373 < 800f && num373 < num369 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num370].position, Main.npc[num370].width, Main.npc[num370].height))
                        {
                            num369 = num373;
                            num367 = num371;
                            num368 = num372;
                        }
                    }
                }
            }
            Projectile.friendly = true;
            float num374 = 9f;
            float num375 = 0.2f;
            Vector2 vector27 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
            float num376 = num367 - vector27.X;
            float num377 = num368 - vector27.Y;
            float num378 = (float)Math.Sqrt(num376 * num376 + num377 * num377);
            num378 = num374 / num378;
            num376 *= num378;
            num377 *= num378;
            if (Projectile.velocity.X < num376)
            {
                Projectile.velocity.X = Projectile.velocity.X + num375;
                if (Projectile.velocity.X < 0f && num376 > 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X + num375 * 2f;
                }
            }
            else if (Projectile.velocity.X > num376)
            {
                Projectile.velocity.X = Projectile.velocity.X - num375;
                if (Projectile.velocity.X > 0f && num376 < 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X - num375 * 2f;
                }
            }
            if (Projectile.velocity.Y < num377)
            {
                Projectile.velocity.Y = Projectile.velocity.Y + num375;
                if (Projectile.velocity.Y < 0f && num377 > 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + num375 * 2f;
                    return;
                }
            }
            else if (Projectile.velocity.Y > num377)
            {
                Projectile.velocity.Y = Projectile.velocity.Y - num375;
                if (Projectile.velocity.Y > 0f && num377 < 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - num375 * 2f;
                    return;
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
				int num623 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.CursedTorch, 0f, 0f, 0);
				Dust dust = Main.dust[num623];
				dust.scale *= 1.1f;
				Main.dust[num623].noGravity = true;
				num3 = num622;
			}
			for (int num624 = 0; num624 < 30; num624 = num3 + 1)
			{
				int num625 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.CursedTorch, 0f, 0f, 0);
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
					int p = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X, Projectile.position.Y, num628, num629, ModContent.ProjectileType<ScourgeOfTheShadows_CursedFireball>(), Projectile.damage*3, (int)(Projectile.knockBack * 0.35), Main.myPlayer, 0f, 0f);
					num3 = num627;
					Main.projectile[p].timeLeft = 240;
				}
			}
		}
    }
}