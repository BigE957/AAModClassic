using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRetriever.BossStandard;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic._Unreleased.Content.Parthenan.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossRetriever
{
    [AutoloadBossHead]
    public class Retriever : ModNPC
    {
        public static Asset<Texture2D> Glowmask1;
        public static Asset<Texture2D> Glowmask2;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Retriever");
            Main.npcFrameCount[NPC.type] = 14;

            Glowmask1 = ModContent.Request<Texture2D>(Texture + "_Glow1");
            Glowmask2 = ModContent.Request<Texture2D>(Texture + "_Glow2");
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 30000;
            NPC.damage = 80;
            NPC.defense = 30;
            NPC.buffImmune[BuffID.Ichor] = true;
            NPC.value = Item.buyPrice(0, 10, 0, 0);
            NPC.knockBackResist = 0f;
            NPC.width = 92;
            NPC.height = 54;
            NPC.friendly = false;
            NPC.npcSlots = 1f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            //TODO
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.netAlways = true;
            Music = MusicManagementSystem.MusicSlots["Siege"];
            SpawnModBiomes = [ModContent.GetInstance<ParthenanBiome>().Type];
        }

        public float[] customAI = new float[3];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(customAI[0]);
                writer.Write(customAI[1]);
                writer.Write(customAI[2]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                customAI[0] = reader.ReadSingle();
                customAI[1] = reader.ReadSingle();
                customAI[2] = reader.ReadSingle();
            }
        }

        public Color color;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = Glowmask1.Value;
            Texture2D glowTex1 = Glowmask2.Value;
            color = BaseUtility.MultiLerpColor(((int)(Main.GlobalTimeWrappedHourly * 60)) % 100 / 100f, drawColor, drawColor, Color.Violet, drawColor, Color.Violet, drawColor);
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(glowTex, NPC.Center - screenPos, NPC.frame, color, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(glowTex1, NPC.Center - screenPos, NPC.frame, Color.White, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            return false;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                StormingSiegeSystem.KillSiegeMech(0);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RetrieverGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RetrieverGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RetrieverGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RetrieverGore4").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RetrieverGore5").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RetrieverGore6").Type, 1f);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<RetrieverTreasureBag>()));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RetrieverTrophy>(), 10));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RetrieverMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.SoulofSight, 1, 20, 40));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FulguriteBar>(), 1, 30, 64));

            npcLoot.Add(notExpertRule);
        }

        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;   //boss drops
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);  //boss life scale in expertmode
            NPC.damage = (int)(NPC.damage * 0.8f);  //boss damage increase in expermode
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }
        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            spriteEffects = NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

		public Vector2 offsetBasePoint = new Vector2(240, 0);
		
        public float moveSpeed = 10f;

        public override void AI()
        {
            Player targetPlayer = Main.player[NPC.target];
            color = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, BaseDrawing.GetLightColor(NPC.position), BaseDrawing.GetLightColor(NPC.position), Color.Violet, BaseDrawing.GetLightColor(NPC.position), Color.Violet, BaseDrawing.GetLightColor(NPC.position));

            Lighting.AddLight((int)(NPC.Center.X + NPC.width / 2) / 16, (int)(NPC.position.Y + NPC.height / 2) / 16, color.R / 255, color.G / 255, color.B / 255);

            if (Main.player[NPC.target].dead || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f)
            {
                NPC.TargetClosest();
                if (Main.player[NPC.target].dead || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f)
                {
                    NPC.active = false;
                    return;
                }
            }       

            if (Main.dayTime)
            {
                NPC.velocity.Y -= 4;
                NPC.netUpdate2 = true;
                if (NPC.position.Y + NPC.velocity.Y <= 0f && Main.netMode != NetmodeID.MultiplayerClient) { BaseAI.KillNPC(NPC); NPC.netUpdate2 = true; }
                return;
            }

            bool forceChange = false;

            bool Dive1 = NPC.life < NPC.lifeMax * .8f;
            bool Dive2 = NPC.life < NPC.lifeMax * .5f;
            bool Dive3 = NPC.life < NPC.lifeMax * .2f;
            int DiveSpeed = Dive1 ? 14 : Dive2 ? 17 : 20;
			int ShootLaserRate = 10;
			offsetBasePoint.X = customAI[2];
			
            if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[0] != 2 && NPC.ai[0] != 3)
            {
                int stopValue = 60;
                NPC.ai[3]++;
                if (NPC.ai[3] > stopValue) NPC.ai[3] = stopValue;
                forceChange = NPC.ai[3] >= stopValue;
            }
            if (NPC.ai[0] == 1) //move to starting charge position
            {
                moveSpeed = 11f;
                Vector2 point = targetPlayer.Center + offsetBasePoint + new Vector2(0f, -250f);
                MoveToPoint(point);
                if (Main.netMode != NetmodeID.MultiplayerClient && (Vector2.Distance(NPC.Center, point) < 10f || forceChange))
                {
                    NPC.ai[0] = 2;
                    NPC.ai[1] = targetPlayer.Center.X;
                    NPC.ai[2] = targetPlayer.Center.Y;
                    NPC.ai[3] = 0;
                    NPC.netUpdate = true;
                }
                BaseAI.LookAt(targetPlayer.Center, NPC, 0, 0f, 0.1f, false);
                NPC.direction = NPC.Center.X > targetPlayer.Center.X ? -1 : 1;
            }
            else if (NPC.ai[0] == 2) //dive down
            {
                moveSpeed = DiveSpeed;
                Vector2 targetCenter = new Vector2(NPC.ai[1], NPC.ai[2]);
                Vector2 point = targetCenter - offsetBasePoint + new Vector2(0f, 250f);
                MoveToPoint(point);
                if (Main.netMode != NetmodeID.MultiplayerClient && Vector2.Distance(NPC.Center, point) < 10f)
                {
                    NPC.ai[0] = Dive1 ? 3 : 0;
                    NPC.ai[1] = Dive1 ? targetPlayer.Center.X : 0;
                    NPC.ai[2] = Dive1 ? targetPlayer.Center.Y : 0;
                    NPC.ai[3] = 0;
                    NPC.netUpdate = true;
                }
                BaseAI.Look(NPC, 0, 0f, 0.1f, false);
            }
            else if (NPC.ai[0] == 3) //dive up
            {
                moveSpeed = DiveSpeed;
                Vector2 targetCenter = new Vector2(NPC.ai[1], NPC.ai[2]);
                Vector2 point = targetCenter + offsetBasePoint + new Vector2(0f, -250f);
                MoveToPoint(point);
                if (Main.netMode != NetmodeID.MultiplayerClient && Vector2.Distance(NPC.Center, point) < 10f)
                {
                    NPC.ai[0] = Dive2 ? 4 : 0;
                    NPC.ai[1] = Dive2 ? targetPlayer.Center.X : 0;
                    NPC.ai[2] = Dive2 ? targetPlayer.Center.Y : 0;
                    NPC.ai[3] = 0;
                    NPC.netUpdate = true;
                }
                BaseAI.Look(NPC, 0, 0f, 0.1f, false);
            }
            else if (NPC.ai[0] == 4) //dive down
            {
                moveSpeed = DiveSpeed;
                Vector2 targetCenter = new Vector2(NPC.ai[1], NPC.ai[2]);
                Vector2 point = targetCenter + offsetBasePoint + new Vector2(0f, -250f);
                MoveToPoint(point);
                if (Main.netMode != NetmodeID.MultiplayerClient && Vector2.Distance(NPC.Center, point) < 10f)
                {
					NPC.ai[0] = Dive3 ? 5 : 0;
                    NPC.ai[1] = Dive3 ? targetPlayer.Center.X : 0;
                    NPC.ai[2] = Dive3 ? targetPlayer.Center.Y : 0;
                    NPC.ai[3] = 0;
					NPC.netUpdate2 = true;
                }
                BaseAI.Look(NPC, 0, 0f, 0.1f, false);
            }
            else if (NPC.ai[0] == 5) //dive up
            {
                moveSpeed = DiveSpeed;
                Vector2 targetCenter = new Vector2(NPC.ai[1], NPC.ai[2]);
                Vector2 point = targetCenter + offsetBasePoint + new Vector2(0f, -250f);
                MoveToPoint(point);
                if (Main.netMode != NetmodeID.MultiplayerClient && Vector2.Distance(NPC.Center, point) < 10f)
                {
					NPC.ai[0] = 0;
					NPC.ai[1] = 0;
					NPC.ai[2] = 0;
					NPC.ai[3] = 0;
					NPC.netUpdate = true;
                }
                BaseAI.Look(NPC, 0, 0f, 0.1f, false);
            }
            else if (NPC.ai[0] == 6) //shoot lasers right
            {
                moveSpeed = 11f;
                Vector2 point = targetPlayer.Center + offsetBasePoint + new Vector2(0f, -250f);
                MoveToPoint(point);
                BaseAI.LookAt(targetPlayer.Center, NPC, 0, 0f, 0.1f, false);
                NPC.direction = NPC.Center.X > targetPlayer.Center.X ? -1 : 1;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
					customAI[0]++;
					if(customAI[0] > 200)
					{
						NPC.ai[0] = 0;
						NPC.ai[1] = 0;
						NPC.ai[2] = 0;
						NPC.ai[3] = 0;
						customAI[0] = 0;
						NPC.netUpdate = true;						
					}
					if(Vector2.Distance(NPC.Center, point) < 10f || customAI[0] > 50)
					{
						BaseAI.ShootPeriodic(NPC, targetPlayer.position, targetPlayer.width, targetPlayer.height, ModContent.ProjectileType<Retriever_Shot>(), ref customAI[1], ShootLaserRate, NPC.damage / (Main.expertMode ? 2 : 4), 12f, false);
					}
                }
            }
            else if (NPC.ai[0] == 7) //shoot lasers left
            {
                moveSpeed = 11f;
                Vector2 point = targetPlayer.Center + offsetBasePoint + new Vector2(0f, -250f);
                MoveToPoint(point);
                BaseAI.LookAt(targetPlayer.Center, NPC, 0, 0f, 0.1f, false);
                NPC.direction = NPC.Center.X > targetPlayer.Center.X ? -1 : 1;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
					customAI[0]++;
					if(customAI[0] > 200)
					{
						NPC.ai[0] = 0;
						NPC.ai[1] = 0;
						NPC.ai[2] = 0;
						NPC.ai[3] = 0;
						customAI[0] = 0;
						NPC.netUpdate = true;						
					}	
					if(Vector2.Distance(NPC.Center, point) < 10f)
					{						
						BaseAI.ShootPeriodic(NPC, targetPlayer.position, targetPlayer.width, targetPlayer.height, ModContent.ProjectileType<Retriever_Shot>(), ref customAI[1], ShootLaserRate, NPC.damage / (Main.expertMode ? 2 : 4), 12f, false);
					}
                }
            }				
            else //standard movement
            {
                moveSpeed = 8;
                Vector2 point = targetPlayer.Center + offsetBasePoint;
                MoveToPoint(point);
                if (Main.netMode != NetmodeID.MultiplayerClient && (Vector2.Distance(NPC.Center, point) < 50f || forceChange))
                {
                    NPC.ai[1]++;
                    if (NPC.ai[1] > 150)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            offsetBasePoint.X = 240;
                        }
                        else
                        {
                            offsetBasePoint.X = -240;
                        }
						customAI[2] = offsetBasePoint.X;
						if(Main.rand.Next(3) == 0) //lasers
						{
							NPC.ai[0] = offsetBasePoint.X < 0 ? 7 : 6;
							NPC.ai[1] = 0;
							NPC.ai[2] = 0;
							NPC.ai[3] = 0;
							NPC.netUpdate2 = true;						
						}else
						{
							NPC.ai[0] = 1;
							NPC.ai[1] = 0;
							NPC.ai[2] = 0;
							NPC.ai[3] = 0;
							NPC.netUpdate2 = true;
						}
                    }
                }
                BaseAI.LookAt(targetPlayer.Center, NPC, 0, 0f, 0.1f, false);
                NPC.direction = NPC.Center.X > targetPlayer.Center.X ? -1 : 1;
            }
        }
		
        public override void FindFrame(int frameHeight)
        {
            if (NPC.ai[0] == 6 || NPC.ai[0] == 7) //firing lasers
            {
                NPC.frameCounter++;
                if (NPC.frameCounter >= 4)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                    if (NPC.frame.Y > frameHeight * 13)
                    {
                        NPC.frame.Y = frameHeight * 10;
                    }
                }				
            }
            else
            {
                NPC.frameCounter++;
                if (NPC.frameCounter >= 10)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                }
				if (NPC.frame.Y > frameHeight * 3)
				{
					NPC.frameCounter = 0;
					NPC.frame.Y = 0;
				}				
            }

        }
		

        public void FindFrameOld(int frameHeight)
        {
            if (customAI[0] <= 300)
            {
                if (customAI[0] >= 293)
                {
                    NPC.frame.Y = frameHeight * 5;
                }
                else if (customAI[0] >= 286)
                {
                    NPC.frame.Y = frameHeight * 6;
                }
                else if (customAI[0] >= 279)
                {
                    NPC.frame.Y = frameHeight * 7;
                }
                else if (customAI[0] >= 272)
                {
                    NPC.frame.Y = frameHeight * 8;
                }
                else if (customAI[0] >= 265)
                {
                    NPC.frame.Y = frameHeight * 9;
                }
                else if (customAI[0] >= 258)
                {
                    NPC.frame.Y = frameHeight * 10;
                }
                else if (customAI[0] >= 251)
                {
                    NPC.frame.Y = frameHeight * 11;
                }
                else if (customAI[0] >= 60)
                {
                    NPC.frameCounter++;
                    if (NPC.frameCounter >= 7)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y += frameHeight;
                    }
                    if (NPC.frame.Y > frameHeight * 13)
                    {
                        NPC.frame.Y = frameHeight * 11;
                    }
                }
                else if (customAI[0] >= 59)
                {
                    NPC.frame.Y = frameHeight * 10;
                }
                else if (customAI[0] == 1)
                {
                    NPC.frame.Y = frameHeight * 7;
                }
            }
            else
            {

                NPC.frameCounter++;
                if (NPC.frameCounter >= 10)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                    if (NPC.frame.Y > frameHeight * 3)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                    }
                }
            }

        }


        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
        {
            if (moveSpeed == 0f || NPC.Center == point) return; //don't move if you have no move speed
			float moveSpd = moveSpeed;			
            Vector2 dist = point - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
			if(length < 50f)
				moveSpd /= 2f;
            if (length < moveSpd)
            {
				moveSpd = length;
            }
            NPC.velocity = length <= 5f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= moveSpd;
        }
    }
}
