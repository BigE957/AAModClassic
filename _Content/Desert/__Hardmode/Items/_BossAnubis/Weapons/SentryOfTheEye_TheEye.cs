using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Weapons
{
    public class SentryOfTheEye_TheEye : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Eye");
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
        }
	
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, Color.Gold.R / 255f, Color.Gold.G / 255f, Color.Gold.B / 255f);
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

                if (distance < 600f && target.catchItem == 0 && !target.friendly && target.active && target.type != NPCID.TargetDummy && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, target.position, target.width, target.height))
                {
                    if (Projectile.ai[0] > 20f)
                    {
                        distance = 1.6f / distance;

                        shootToX *= distance * 3;
                        shootToY *= distance * 3;
                        int id = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, shootToX*4, shootToY*4, ProjectileID.DD2FlameBurstTowerT3Shot, Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                        Main.projectile[id].minion = true;
                        Projectile.ai[0] = 0f;
                    }
                }
            }
            Projectile.ai[0] += 1f;
		}
	}
}