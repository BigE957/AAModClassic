using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Anubis.Forsaken
{
    public class HorusCane_HorusEye : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Horus Eye");
        }

        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 42;
			Projectile.tileCollide = false;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.ignoreWater = true;
            Projectile.sentry = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
			Projectile.scale = 0.1f;
            Projectile.alpha = 255;
        }
	
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, Color.DarkSeaGreen.R / 255, Color.DarkSeaGreen.G / 255, Color.DarkSeaGreen.B / 255);
            if (Projectile.scale < 1f) Projectile.scale += 0.01f;
            if (Projectile.alpha > 0) Projectile.alpha -= 5;

            if (Projectile.ai[1] == 0)
            {
                Projectile.velocity.Y += 0.005f;
                if (Projectile.velocity.Y > .2f)
                {
                    Projectile.ai[1] = 1f;
                    Projectile.netUpdate = true;
                }
            }
            else
            if (Projectile.ai[1] == 1)
            {
                Projectile.velocity.Y -= 0.005f;
                if (Projectile.velocity.Y < -.2f)
                {
                    Projectile.ai[1] = 0f;
                    Projectile.netUpdate = true;
                }
            }

            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];
 
                float shootToX = target.position.X + target.width * 0.5f - Projectile.Center.X;
                float shootToY = target.position.Y + target.height * 0.5f - Projectile.Center.Y;
                float distance = (float)Math.Sqrt(shootToX * shootToX + shootToY * shootToY);

                if (Projectile.scale >= 1f && distance < 600f && target.catchItem == 0 && !target.friendly && target.active && target.type != NPCID.TargetDummy && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, target.position, target.width, target.height))
                {
                    if (Projectile.ai[0] > 15f) // Time in (60 = 1 second) 
                    {
                        for (int h = 0; h < 5; h++)
						{
							Vector2 vel = new Vector2(0, -1);
							float rand = Main.rand.NextFloat() * 6.283f;
							vel = vel.RotatedBy(rand);
							vel *= 8f;
							Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vel.X, vel.Y, ModContent.ProjectileType<HorusCane_HorusHawk>(), Projectile.damage, 0, Main.myPlayer);
						}
                        Projectile.ai[0] = 0f;
						Projectile.scale = 0.5f;
                    }
                }
            }
            Projectile.ai[0] += 1f;
		}
	}
}