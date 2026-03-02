using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAModClassic.Items.Boss.Zero
{
    public class OmegaVolleyExtraAmmo : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Omega Shoot");     
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        
		}

		public override void SetDefaults()
		{
			Projectile.width = 20;               
			Projectile.height = 20;
			Projectile.scale = 0.5f;              
			Projectile.aiStyle = -1;             
			Projectile.friendly = true;         
			Projectile.hostile = false;         
			Projectile.DamageType = DamageClass.Ranged;           
			Projectile.timeLeft = 600;          
			Projectile.alpha = 0;             
			Projectile.light = 0f;            
			Projectile.ignoreWater = true;          
			Projectile.tileCollide = true;          
			Projectile.extraUpdates = 1;            
			AIType = ProjectileID.Bullet;           
		}

		private int homingtime = 3;
		private int homingDelay = 3;

		public override bool PreDraw(ref Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		public override void AI()
        {
			Player projOwner = Main.player[Projectile.owner];
           	Projectile.direction = projOwner.direction;
           	Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            if (Projectile.spriteDirection == -1)
            {
               Projectile.rotation -= MathHelper.ToRadians(180f);
            }

			float num167 = (float)Math.Sqrt(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y);
			float num168 = Projectile.ai[0];
			if (num168 == 0f)
			{
				Projectile.ai[0] = num167;
				num168 = num167;
			}
			if(homingtime >= 0 && homingDelay < 0)
			{
				float num169 = Projectile.position.X;
				float num170 = Projectile.position.Y;
				float num171 = 300f;
				bool flag4 = false;
				int num172 = 0;
				if (Projectile.ai[1] == 0f)
				{
					int num;
					for (int num173 = 0; num173 < 200; num173 = num + 1)
					{
						if (Main.npc[num173].CanBeChasedBy(this, false) && (Projectile.ai[1] == 0f || Projectile.ai[1] == num173 + 1))
						{
							float num174 = Main.npc[num173].position.X + Main.npc[num173].width / 2;
							float num175 = Main.npc[num173].position.Y + Main.npc[num173].height / 2;
							float num176 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num174) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num175);
							if (num176 < num171 && Collision.CanHit(new Vector2(Projectile.position.X + Projectile.width / 2, Projectile.position.Y + Projectile.height / 2), 1, 1, Main.npc[num173].position, Main.npc[num173].width, Main.npc[num173].height))
							{
								num171 = num176;
								num169 = num174;
								num170 = num175;
								flag4 = true;
								num172 = num173;
							}
						}
						num = num173;
					}
					if (flag4)
					{
						Projectile.ai[1] = num172 + 1;
					}
					flag4 = false;
				}
				if (Projectile.ai[1] > 0f)
				{
					int num177 = (int)(Projectile.ai[1] - 1f);
					if (Main.npc[num177].active && Main.npc[num177].CanBeChasedBy(this, true) && !Main.npc[num177].dontTakeDamage)
					{
						float num178 = Main.npc[num177].position.X + Main.npc[num177].width / 2;
						float num179 = Main.npc[num177].position.Y + Main.npc[num177].height / 2;
						float num180 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num178) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num179);
						if (num180 < 1000f)
						{
							flag4 = true;
							num169 = Main.npc[num177].position.X + Main.npc[num177].width / 2;
							num170 = Main.npc[num177].position.Y + Main.npc[num177].height / 2;
						}
					}
					else
					{
						Projectile.ai[1] = 0f;
					}
				}
				if (!Projectile.friendly)
				{
					flag4 = false;
				}
				if (flag4)
				{
					float num181 = num168;
					Vector2 vector19 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
					float num182 = num169 - vector19.X;
					float num183 = num170 - vector19.Y;
					float num184 = (float)Math.Sqrt(num182 * num182 + num183 * num183);
					num184 = num181 / num184;
					num182 *= num184;
					num183 *= num184;
					int num185 = 8;
					Projectile.velocity.X = (Projectile.velocity.X * (num185 - 1) + num182) / num185;
					Projectile.velocity.Y = (Projectile.velocity.Y * (num185 - 1) + num183) / num185;
				}
				homingtime --;
				homingDelay = -1;
            }
			else if(homingDelay >= 0)
			{
				homingDelay --;
				Vector2 speedkeep = Projectile.velocity;
				speedkeep.Normalize();
				Projectile.velocity = speedkeep * Projectile.ai[0];
			}
			else
			{
				homingtime = 3;
				homingDelay = 10;
				Vector2 speedkeep = Projectile.velocity;
				speedkeep.Normalize();
				Projectile.velocity = speedkeep * Projectile.ai[0];
			}
			return;
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			
			damage = (int)(damage * 1.5f);
		}
	}
}
