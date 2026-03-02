using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class FerretNote : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Note of Furet");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 150;
            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
			HandleTargetingMovement(0.025f, 0.05f, Projectile.velocity.Length(), 9f);
			Projectile.frame = Projectile.whoAmI % 4;
			Projectile.rotation = Projectile.velocity.X * 0.025f;

            //int num557 = 8;
            //dust!
            //int dustId = Dust.NewDust(new Vector2(projectile.position.X + (float)num557, projectile.position.Y + (float)num557), projectile.width - num557 * 2, projectile.height - num557 * 2, 6, 0f, 0f, 0);
            //Main.dust[dustId].noGravity = true;
            //int dustId3 = Dust.NewDust(new Vector2(projectile.position.X + (float)num557, projectile.position.Y + (float)num557), projectile.width - num557 * 2, projectile.height - num557 * 2, 6, 0f, 0f, 0);
            //Main.dust[dustId3].noGravity = true;
        }

		public float maxDistToAttack = 3000f;
		public int target = -1;
		public int targetDelay = 8;
		public void HandleTargetingMovement(float rotScalar = 0.1f, float entVelScalar = 0.25f, float newVelSpeed = 11f, float maxSpeed = 11f)
		{
			Target();
			if (target != -1)
			{
				Entity ent = Main.npc[target];
				Projectile.velocity += BaseUtility.RotateVector(default, new Vector2(maxSpeed, 0f), BaseUtility.RotationTo(Projectile.Center, ent.Center)) * rotScalar;
				if(Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) > maxSpeed){ Projectile.velocity.Normalize(); Projectile.velocity *= maxSpeed; }
				Projectile.position += ent.velocity * entVelScalar;
			}	
		}

		public void Target()
		{
			targetDelay = Math.Max(0, targetDelay - 1);
			if (target != -1 && !CanTarget(Main.npc[target])) { target = -1; }
			if (target == -1 && targetDelay == 0 && Projectile.timeLeft % 20 == 0)
			{
				Vector2 startPos = Projectile.Center;
				int[] npcs = BaseAI.GetNPCs(startPos, -1, maxDistToAttack);
				if (npcs.Length > 0)
				{
					float prevDist = maxDistToAttack;
					foreach (int i in npcs)
					{
						NPC npc = Main.npc[i];
						float dist = Vector2.Distance(startPos, npc.Center);
						if (CanTarget(npc) && dist < prevDist) { target = npc.whoAmI; prevDist = dist; }
					}
				}
			}
		}	

		public bool CanTarget(NPC npc)
		{
			return npc.active && npc.life > 0 && !npc.friendly && !npc.dontTakeDamage && npc.lifeMax > 5;
		}	

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 1000);
        }

		public override Color? GetAlpha(Color lightColor)
		{
			int percentile = (Projectile.owner + Projectile.whoAmI) % 4;
			switch(percentile)
			{
				case 0: return new Color(255, 0, 0, 150);
				case 1: return new Color(107, 40, 75, 150);
				case 2: return new Color(120, 0, 0, 150);
				default: return new Color(175, 20, 20, 150);				
			}
		}

        public override void OnKill(int timeLeft)
        {
            for (int m = 0; m < 10; m++)
            {
                int dustID = Dust.NewDust(Projectile.Center, Projectile.width, 1, ModContent.DustType<Dusts.AkumaDustLight>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[dustID].noGravity = true;
                Main.dust[dustID].velocity *= 2f;
                dustID = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaDustLight>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[dustID].velocity *= 2f;
            }
        }		
    }
}