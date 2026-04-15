using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic.Base.BaseMod.Base;
using ReLogic.Content;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.Weapons
{
	public class GalacticStormspike_Stormray : AAProjectile
	{

        public override string Texture { get { return "AAModClassic/BlankTex"; } }

        public override void SetStaticDefaults()
		{
            //TODOSOC
            //displayName = "Stormray";

            mainTex = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/_PostMoonLord/Items/SoulOfCthulhu/Weapons/GalacticStormspike_StormShockChainEnd3");
            chainTex = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/_PostMoonLord/Items/SoulOfCthulhu/Weapons/GalacticStormspike_StormShockChain");
            chainEndTex = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/_PostMoonLord/Items/SoulOfCthulhu/Weapons/GalacticStormspike_StormShockChainEnd");
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
		public static Asset<Texture2D> mainTex;
		public static Asset<Texture2D> chainTex;
		public static Asset<Texture2D> chainEndTex;

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
			for(int nextID = 0; nextID < targetVec.Length - 1; nextID++)
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
            if (!Main.instance.IsActive)
                return false;

            // Switch to additive blending so the bolt glows correctly
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

            //Main.NewText($"Center: {Projectile.Center} | endPos: {endPos} | dist: {Vector2.Distance(Projectile.Center, endPos)}");

            drawDelay = Math.Max(0, drawDelay - 1);
            if (drawDelay <= 0)
            {
                DrawArc(Main.spriteBatch, new Texture2D[] { chainEndTex.Value, chainTex.Value, mainTex.Value }, Projectile.Center, endPos, false);
                for (int m = 0; m < targetPos.Length; m++)
                {
                    DrawArc(Main.spriteBatch, new Texture2D[] { chainEndTex.Value, chainTex.Value, mainTex.Value }, targetPosStart[m], targetPos[m], true);
                }
            }

            // Restore default blend state
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

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
            int iters = 0;
            int maxIters = 1000;

            while (Way < length)
            {
                Vector2 dir = endPos - currentPoint;
                dir.Normalize();

                Vector2 vstart = currentPoint;
                Vector2 vend = currentPoint + dir * Jump;
                vend = BaseUtility.RotateVector(vstart, vend, (float)(Math.PI / (5f + Main.rand.Next(5))) * 0.35f * (Main.rand.NextBool(2) ? -1f : 1f));

                if (targetPosStart.Length > 0 && subJump > 32f)
                {
                    for (int m = nextID; m < targetPosStart.Length; m++)
                    {
                        if (Vector2.Distance(vend, targetPosStart[m]) < 30f)
                        {
                            vend = targetPosStart[m];
                            nextID = m;
                            break;
                        }
                    }
                }

                Texture2D[] textures = new Texture2D[] { null, texs[1], texs[0] };
                if (Way + Jump >= length)
                {
                    textures[2] = texs[2];
                    vend = endPos;
                }

                DrawArcSegment(sb, textures, vstart, vend, npcHit);

                Way += Jump;
                currentPoint = vend;
                subJump += Jump;
                BaseDrawing.AddLight(vend, boltColor, vend == endPos ? 1f : 2f);

                iters++;
                if (iters > maxIters)
                    break;
            }
        }

        public void DrawArcSegment(SpriteBatch sb, Texture2D[] textures, Vector2 start, Vector2 end, bool npcHit = false)
        {
            bool drawEndsUnder = true;
            Color overrideColor = Color.White;
            float scale = 1f + (npcHit ? -0.5f : 0.2f);
            float Jump = Math.Max(1f, textures[1].Height * scale - 2f);

            Vector2 dir = end - start;
            dir.Normalize();
            float length = Vector2.Distance(start, end);
            float Way = 0f;
            float rotation = BaseUtility.RotationTo(start, end) - 1.57f;
            int iters = 0;
            int maxIters = 1000;

            if (length <= 0f) return;

            float texWidth = textures[1].Width;
            float texHeight = textures[1].Height;
            // texCenter is in texture space (unscaled), scale is applied in the Draw call
            Vector2 texCenter = new(texWidth / 2f, texHeight / 2f);

            while (Way < length)
            {
                // v is screen-space position of the texture center — matches what InDrawZone expects
                Vector2 v = (start + dir * Way) - Main.screenPosition;

                void drawEnds()
                {
                    if (textures[0] != null && Way == 0f)
                    {
                        Vector2 origin0 = new Vector2(textures[0].Width / 2f, textures[0].Height / 2f);
                        Vector2 pos0 = start - Main.screenPosition;
                        sb.Draw(textures[0], pos0, null, overrideColor, rotation, origin0, scale, SpriteEffects.None, 0f);
                    }
                    if (textures[2] != null && Way + Jump >= length)
                    {
                        Vector2 origin2 = new Vector2(textures[2].Width / 2f, textures[2].Height / 2f);
                        Vector2 pos2 = end - Main.screenPosition;
                        sb.Draw(textures[2], pos2, null, overrideColor, rotation, origin2, scale, SpriteEffects.None, 0f);
                    }
                }

                if (true)//BaseDrawing.InDrawZone(v, true)) // screen-space, matches original
                {
                    if (drawEndsUnder)
                        drawEnds();

                    sb.Draw(textures[1], v, null, overrideColor, rotation, texCenter, scale, SpriteEffects.None, 0f);
                    if (Main.rand.NextBool(15))
                    {
                        int dustID = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.CthulhuDust>(), Projectile.velocity.X, Projectile.velocity.Y, 80, default);
                        Main.dust[dustID].rotation = Main.rand.Next(5) * (float)(Math.PI / 8f);
                        Main.dust[dustID].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble())) * 3f;
                    }

                    if (!drawEndsUnder)
                        drawEnds();
                }

                Way += Jump;
                iters++;
                if (iters > maxIters) break;
            }
        }
    }
}
