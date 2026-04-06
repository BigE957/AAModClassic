using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.Weapons
{
	public class GalacticStormspike_Stormray : AAProjectile
	{

        public override string Texture { get { return "AAModClassic/BlankTex"; } }

        public override void SetStaticDefaults()
		{
            //TODOSOC
			//displayName = "Stormray";
		}		

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 200;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.damage = 1;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
        }

		public static Color boltColor = new Color(60, 119, 60, 220);
		public static Texture2D mainTex;
		public static Texture2D chainTex;
		public static Texture2D chainEndTex;

		public int maxTargets = 32;
		public Vector2 endPos;
		public Vector2[] targetPosStart = new Vector2[0];
		public Vector2[] targetPos = new Vector2[0];
		public int lifeTimer = 12;
		public float minRange = 0f;
		public float maxRange = 100f;
		public float maxDistance = 1000f;
		public bool hasVel = false;
		public float velRot = 0f;
		public Vector2 vel;
		public int drawDelay = 2;

		public override void AI()
		{
			lifeTimer--; if (!Main.player[Projectile.owner].active || Main.player[Projectile.owner].dead || lifeTimer <= 0) { Projectile.Kill(); return; }
			Projectile.Center = GetOwnerCenter();
			if (!hasVel) { hasVel = true; vel = Projectile.velocity; velRot = BaseUtility.RotationTo(Projectile.Center, Projectile.Center + Projectile.velocity); Projectile.velocity = default; }
			endPos = BaseAI.TracePlayer(Projectile.Center, maxDistance, velRot, Projectile.owner, false, true, false);
			Vector2 damagePos = Projectile.Center;
			int count = (int)Vector2.Distance(damagePos, endPos) / 32;
			Vector2 distVec = vel; distVec.Normalize(); distVec *= 32f;
			List<Vector2> targets = new List<Vector2>(), targetsStart = new List<Vector2>();
			for (int m = 0; m < count; m++)
			{
				Vector2[] targets2 = FindAndHitTargets(damagePos); 
				Vector2[] targetsStart2 = new Vector2[targets2.Length];
				for(int n = 0; n < targetsStart2.Length; n++){ targetsStart2[n] = damagePos; }
				targets.AddRange(targets2); targetsStart.AddRange(targetsStart2);
				damagePos += distVec;
			}
			targetPos = targets.ToArray(); targetPosStart = targetsStart.ToArray();
			CleanupPoints(targetPos, targetPosStart);
		}

		public void CleanupPoints(Vector2[] targetVec, Vector2[] startVec)
		{
			List<Vector2> vecList = new List<Vector2>(), startList = new List<Vector2>();
			int nextID = 0;
			while (nextID < targetVec.Length - 1)
			{
				for (int m = nextID + 1; m < targetVec.Length; m++)
				{
					if (m == targetVec.Length - 1 || targetVec[m - 1] != targetVec[m])
					{
						int id = m == targetVec.Length - 1 ? m : m - 1;
						vecList.Add(targetVec[id]); startList.Add(startVec[id]); nextID = m; break;
					}
				}
			}
			targetPos = vecList.ToArray(); targetPosStart = startList.ToArray();
		}

		public Vector2 GetOwnerCenter()
		{
			return Main.player[Projectile.owner].Center + BaseUtility.RotateVector(default, new Vector2(ModContent.GetModItem(ModContent.ItemType<GalacticStormspike>()).Item.width, 0f), velRot);
		}

		public Vector2[] FindAndHitTargets(Vector2 startPos)
		{
			List<Entity> list = new List<Entity>();
			List<Vector2> list1 = new List<Vector2>();
			List<Vector2> list2 = new List<Vector2>();
			if (Main.myPlayer == Projectile.owner && Main.player[Projectile.owner].wet && !Main.player[Projectile.owner].immune) 
			{
				list.Add(Main.player[Projectile.owner]); list1.Add(Main.player[Projectile.owner].Center);
			}
			int[] players = BaseAI.GetPlayers(startPos, new int[]{ Projectile.owner }, true, maxRange);
			foreach (int i1 in players)
			{
				if (list1.Count >= maxTargets) { break; }
				Player player = Main.player[i1];
				if (CanTarget(player))
				{
					if (Vector2.Distance(player.Center, startPos) > 15f) list1.Add(player.Center);
					list.Add(player);
				}
			}
			int[] npcs = BaseAI.GetNPCs(startPos, -1, default, maxRange);
			foreach (int i in npcs)
			{
				if (list1.Count >= maxTargets) { break; }
				NPC npc = Main.npc[i];
				if (CanTarget(npc))
				{
					if (Vector2.Distance(npc.Center, startPos) > 15f) list1.Add(npc.Center);
					list.Add(npc);
				}
			}
			Vector2 oldPos = Projectile.position;
			foreach (Entity codable in list)
			{
				if (codable is NPC)
				{
					NPC npc = (NPC)codable;
					if (Projectile.owner == Main.myPlayer && npc.immune[Projectile.owner] <= 0)
					{
						Projectile.position = npc.position; Projectile.Damage(); Projectile.position = oldPos;
					}
				}else
				if (codable is Player)
				{
					Player player = (Player)codable;
					if (player.whoAmI == Main.myPlayer && !player.immune)
					{
						if (player.whoAmI == Projectile.owner) { Projectile.friendly = false; Projectile.hostile = true; }
						Projectile.position = player.position; Projectile.Damage(); Projectile.position = oldPos;
						if (player.whoAmI == Projectile.owner) { Projectile.friendly = true; Projectile.hostile = false; }
					}
				}
			}
			return list1.ToArray();
		}

		public bool CanTarget(Entity codable)
		{
			if (codable == null) return false;
			if (codable is NPC)
			{
				NPC npc = (NPC)codable;
				return !npc.friendly && !npc.dontTakeDamage && (npc.lifeMax == 1 || npc.lifeMax > 5);
			}else
			if (codable is Player)
			{
				Player player = (Player)codable;
				return !player.immune && player.hostile && (Main.player[Projectile.owner].team == 0 || player.team == 0 || Main.player[Projectile.owner].team != player.team);
			}
			return false;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			if(!Main.instance.IsActive) { return false; }
			if (mainTex == null)
			{
				//TODOSOC needs more descriptive names 
				mainTex = Mod.GetTexture("_Unreleased/Projectiles/SoC/StormShockChainEnd3");
				chainTex = Mod.GetTexture("_Unreleased/Projectiles/SoC/StormShockChain");
				chainEndTex = Mod.GetTexture("_Unreleased/Projectiles/SoC/StormShockChainEnd");
			}
			drawDelay = Math.Max(0, drawDelay -1);
			if (drawDelay <= 0)
			{
				DrawArc(Main.spriteBatch, new Texture2D[] { chainEndTex, chainTex, mainTex }, Projectile.Center, endPos, false);
				for (int m = 0; m < targetPos.Length; m++)
				{
					Vector2 target = targetPos[m];
					DrawArc(Main.spriteBatch, new Texture2D[] { chainEndTex, chainTex, mainTex }, targetPosStart[m], target, true);
				}				
			}
			return false;
		}

		public void DrawArc(SpriteBatch sb, Texture2D[] texs, Vector2 startPos, Vector2 endPos, bool npcHit = false)
		{
			float Jump = texs[1].Height * 5;
			float subJump = 0f;
			float length = Vector2.Distance(startPos, endPos);
			float Way = 0f;
			int nextID = 0;
			Vector2 currentPoint = startPos;
			while (Way < length)
			{
				Vector2 dir = endPos - currentPoint; dir.Normalize();
				Vector2 vstart = currentPoint;
				Vector2 vend = currentPoint + dir * Jump; vend = BaseUtility.RotateVector(vstart, vend, (float)(Math.PI / (5f + Main.rand.Next(5))) * 0.35f * (Main.rand.Next(2) == 0 ? -1f : 1f));
				if (targetPosStart.Length > 0 && subJump > 32f)
				{
					for (int m = nextID; m < targetPosStart.Length; m++)
					{
						if (Vector2.Distance(vend, targetPosStart[m]) < 30f) 
						{
							vend = targetPosStart[m]; nextID = m; break;
						}
					}
				}
				Texture2D[] textures = new Texture2D[] { null, texs[1], texs[0] };
				if (Way + Jump >= length) { textures[2] = texs[2]; vend = endPos; }
				DrawArcSegment(sb, textures, vstart, vend, npcHit);
				Way += Jump; currentPoint = vend;
				subJump += Jump;
				BaseDrawing.AddLight(vend, boltColor, vend == endPos ? 1f : 2f);
			}
		}

		public void DrawArcSegment(SpriteBatch sb, Texture2D[] textures, Vector2 start, Vector2 end, bool npcHit = false)
		{
			bool drawEndsUnder = true;
			Color boltColor = new Color(160, 219, 249);
			Color? overrideColor = new Color(255, 255, 255, 0);
			float scale = 1f + (npcHit ? -0.5f : 0.2f);
			float Jump = (textures[1].Height - 2f) * scale;
			Vector2 dir = end - start;
			dir.Normalize();
			float length = Vector2.Distance(start, end);
			float Way = 0f;
			float rotation = BaseUtility.RotationTo(start, end) - 1.57f;
			while (Way < length)
			{
				Action drawEnds = () =>
				{
					if (textures[0] != null && Way == 0f)
					{
						float texWidth2 = textures[0].Width;
						float texHeight2 = textures[0].Height;
						Vector2 texCenter2 = new Vector2(texWidth2 / 2f, texHeight2 / 2f) * scale;
						Vector2 v2 = start - Main.screenPosition + texCenter2;
						Color lightColor2 = (Color)overrideColor;
						sb.Draw(textures[0], v2 - texCenter2, new Rectangle(0, 0, (int)texWidth2, (int)texHeight2), lightColor2, rotation, texCenter2, scale, SpriteEffects.None, 0f);
					}
					if (textures[2] != null && Way + Jump >= length)
					{
						float texWidth2 = textures[2].Width;
						float texHeight2 = textures[2].Height;
						Vector2 texCenter2 = new Vector2(texWidth2 / 2f, texHeight2 / 2f) * scale;
						Vector2 v2 = end - Main.screenPosition + texCenter2;
						Color lightColor2 = (Color)overrideColor;
						sb.Draw(textures[2], v2 - texCenter2, new Rectangle(0, 0, (int)texWidth2, (int)texHeight2), lightColor2, rotation, texCenter2, scale, SpriteEffects.None, 0f);
					}
				};
				float texWidth = textures[1].Width;
				float texHeight = textures[1].Height;
				Vector2 texCenter = new Vector2(texWidth / 2f, texHeight / 2f) * scale;
				Vector2 v = start + dir * Way - Main.screenPosition + texCenter;			
				if(BaseDrawing.InDrawZone(v, true))
				{
					Color lightColor = (Color)overrideColor;						
					if (drawEndsUnder) { drawEnds(); }
					sb.Draw(textures[1], v - texCenter, new Rectangle(0, 0, (int)texWidth, (int)texHeight), lightColor, rotation, texCenter, scale, SpriteEffects.None, 0f);
					if (Main.rand.Next(15) == 0)
					{
						v += Main.screenPosition;
                        int dustID = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.CthulhuDust>(), Projectile.velocity.X, Projectile.velocity.Y, 80, default);
						Main.dust[dustID].rotation = Main.rand.Next(5) * (float)(Math.PI / 8f);
						Main.dust[dustID].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
						Main.dust[dustID].velocity *= 3f;
					}
					if (!drawEndsUnder) { drawEnds(); }
				}
				Way += Jump;
			}
		}
	}
}
