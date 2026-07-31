using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Accessories
{
    public class ArtifactOfJudgement_EyeOfJudgement : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eye of Judgement");
			Projectile.light = 0.5f;
		}

		public override void SetDefaults()
		{
			Projectile.width = 56;
			Projectile.height = 42;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 900;
			Projectile.tileCollide = false;
			Projectile.hostile = false;
			Projectile.friendly = false;
			Projectile.extraUpdates = 0;
			Projectile.sentry = true;
		}
		
		public override void AI()
        {
            Lighting.AddLight(Projectile.Center, Color.Gold.R / 255f, Color.Gold.G / 255f, Color.Gold.B / 255f);
            Player player = Main.player[Projectile.owner];
			Projectile.Center = player.Center;
			Projectile.position.Y = player.Center.Y-90;
			Projectile.spriteDirection = player.direction;
			if (player.dead || !player.HasBuff(ModContent.BuffType<ArtifactOfJudgement_Buff>()))
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
