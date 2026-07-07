using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using System;
using Terraria;

using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons
{
    public class Sunstorm_Fireball : AAProjectile
	{
		float rot = 0f;
		float rotInit = -1f;
		bool released = false;
		
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 4;
		}

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;		
        }

		public void SetRot()
		{
			float oldInit = rotInit;
			int[] projs = BaseAI.GetProjectiles(Main.player[Projectile.owner].Center, Projectile.type, Projectile.owner, 200f);
			rotInit = projs.Length == 0 ? 0f : (float)Math.PI * 2f / projs.Length;

			if (rotInit != oldInit)
			{
				int projSlot = 0;
				for(int m = 0; m < projs.Length; m++)
				{
					if (projs[m] == Projectile.identity) { projSlot = m; }
				}
				rot = rotInit * (projSlot + 1f);
			}
		}

        public override void AI()
        {
			Projectile.frameCounter++;
            if (Projectile.frameCounter >= 8)
            {
                Projectile.frameCounter = 0;
                Projectile.frame += 1;
            }
            if (Projectile.frame > 3)
            {
                Projectile.frame = 0;
            }
			
			Player player = Main.player[Projectile.owner];
			if (player == Main.player[Projectile.owner])
			{
				if (player.altFunctionUse == 2)
				{
					released = true;
					float num1 = 12f;
					Vector2 vector2 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
					float f1 = Main.mouseX + Main.screenPosition.X - vector2.X;
					float f2 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
					if (player.gravDir == -1.0)
						f2 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
					float num4 = (float)Math.Sqrt(f1 * (double)f1 + f2 * (double)f2);
					float num5;
					if (float.IsNaN(f1) && float.IsNaN(f2) || f1 == 0.0 && f2 == 0.0)
					{
						f1 = Projectile.direction;
						f2 = 0.0f;
						num5 = num1;
					}
					else
						num5 = num1 / num4;
					float SpeedX = f1 * num5;
					float SpeedY = f2 * num5;
					Projectile.velocity.X = SpeedX;
					Projectile.velocity.Y = SpeedY;
				}
			}
			Projectile.ai[0]++;
			if (Projectile.ai[0] < 30 && !released)
            {
				if (Projectile.active) { SetRot(); }
				BaseAI.AIRotate(Projectile, ref Projectile.rotation, ref rot, player.Center, true, 60f, 20f, 0.07f, true);
			}
			if (Projectile.ai[0] >= 30)
            {
				int foundTarget1 = HomeOnTarget();
				if (!released)
				{
					if (foundTarget1 == -1)
					{
						if (Projectile.active) { SetRot(); }
						BaseAI.AIRotate(Projectile, ref Projectile.rotation, ref rot, player.Center, true, 60f, 20f, 0.07f, true);
					}
				}
			}
            if (Projectile.position.HasNaNs())
            {
                Projectile.Kill();
                return;
            }
            bool flag5 = WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.position.X / 16, (int)Projectile.position.Y / 16));
            Dust dust19 = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default, 1f)];
            dust19.position = Projectile.Center;
            dust19.velocity = Vector2.Zero;
            dust19.noGravity = true;
            Dust dust18 = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default, 1f)];
            dust18.position = Projectile.Center;
            dust18.velocity = Vector2.Zero;
            dust18.noGravity = true;
            if (flag5)
            {
                dust19.noLight = true;
                dust18.noLight = true;
            }
            if (Projectile.ai[1] == -1f)
            {
                Projectile.velocity = Vector2.Zero;
                Projectile.tileCollide = false;
                Projectile.penetrate = -1;
                Projectile.position = Projectile.Center;
                Projectile.width = Projectile.height = 140;
                Projectile.Center = Projectile.position;
                return;
            }
            if (Projectile.ai[0] > 30)
            {
                Projectile.ai[0] = 30; 

                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    NPC n = Main.npc[foundTarget];
                    Vector2 desiredVelocity = Projectile.DirectionTo(n.Center) * 30;
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 1f / 30);
                }
            }
            if (Projectile.numUpdates == 0)
            {
                int num185 = -1;
                float num186 = 60f;
                for (int num187 = 0; num187 < 200; num187++)
                {
                    NPC nPC2 = Main.npc[num187];
                    if (nPC2.CanBeChasedBy(this, false))
                    {
                        float num188 = Projectile.Distance(nPC2.Center);
                        if (num188 < num186 && Collision.CanHitLine(Projectile.Center, 0, 0, nPC2.Center, 0, 0))
                        {
                            num186 = num188;
                            num185 = num187;
                        }
                    }
                }
                if (num185 != -1)
                {
                    Projectile.ai[1] = -1f;
                    Projectile.netUpdate = true;
                    return;
                }
				if (num185 == -1 && Projectile.ai[1] == -1f)
                {
                    Projectile.Kill();
                }
            }
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 300;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(Projectile) && (!n.wet || homingCanAimAtWetEnemies))
                {
                    float distance = Projectile.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            Projectile.Distance(Main.npc[selectedTarget].Center) > distance) 
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[1] = -1f;
            Projectile.netUpdate = true;
        }

		public override void OnKill(int timeLeft)
		{
			int[] projs = BaseAI.GetProjectiles(Projectile.Center, Projectile.type, Projectile.owner, 200f);
			
			bool flag = WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.position.X / 16, (int)Projectile.position.Y / 16));

            for (int num58 = 0; num58 < 4; num58++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 100, default, 1.5f);
            }
            for (int num59 = 0; num59 < 4; num59++)
            {
                int num60 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default, 2.5f);
                Main.dust[num60].noGravity = true;
                Main.dust[num60].velocity *= 3f;
                if (flag)
                {
                    Main.dust[num60].noLight = true;
                }
                num60 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 100, default, 1.5f);
                Main.dust[num60].velocity *= 2f;
                Main.dust[num60].noGravity = true;
                if (flag)
                {
                    Main.dust[num60].noLight = true;
                }
            }
		}
	}
}