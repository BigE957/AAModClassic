using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAModClassic.Projectiles.Anubis.Forsaken
{
    public class EyeOfForsaken : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 42;
			Projectile.tileCollide = false;
            Projectile.timeLeft = 900;
            Projectile.ignoreWater = true;
            Projectile.sentry = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eye of the Forsaken");
		}
	
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, Color.DarkSeaGreen.R / 255, Color.DarkSeaGreen.G / 255, Color.DarkSeaGreen.B / 255);
            Player player = Main.player[Projectile.owner];
			Projectile.Center = player.Center;
			Projectile.position.Y = player.Center.Y-90;
			Projectile.spriteDirection = player.direction;
			if (player.dead || !player.HasBuff(Mod.Find<ModBuff>("EyeOfForsaken").Type))
			{
				Projectile.Kill();
			}
			for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];
 
                float shootToX = target.position.X + target.width * 0.5f - Projectile.Center.X;
                float shootToY = target.position.Y + target.height * 0.5f - Projectile.Center.Y;
                float distance = (float)Math.Sqrt(shootToX * shootToX + shootToY * shootToY);

                if (distance < 600f && target.catchItem == 0 && !target.friendly && target.active && target.type != NPCID.TargetDummy)
                {
                    if (Projectile.ai[0] > 30f) // Time in (60 = 1 second) 
                    {
                        distance = 1.6f / distance;

                        shootToX *= distance * 3;
                        shootToY *= distance * 3;
                        int id = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, shootToX*4, shootToY*4, Mod.Find<ModProjectile>("ForsakenFrag").Type, Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                        Main.projectile[id].minion = true;
                        Projectile.ai[0] = 0f;
                    }
                }
            }
            Projectile.ai[0] += 1f;
		}
	}
}