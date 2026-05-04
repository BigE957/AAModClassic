using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossOrthrusX.BossStandard;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.BossStandard;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.Pets;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossOrthrusX
{
    [AutoloadBossHead]
    public class OrthrusX : YamataBoss
	{
        public NPC Head1;
        public NPC Head2;
        public int[] Heads = null;
        public bool HeadsSpawned = false;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == 2 || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadSingle();
                internalAI[1] = reader.ReadSingle();
            }
        }

        public override void SetStaticDefaults()
        {
            displayName = "Orthrus X";
            Main.npcFrameCount[NPC.type] = 12;
        }

        public override void SetDefaults()
        {
            NPC.npcSlots = 100;
            NPC.width = 96;
            NPC.height = 78;
            NPC.aiStyle = -1;
            NPC.damage = 0;
            NPC.defense = 99999999;
            NPC.lifeMax = 28000;
            NPC.value = Item.sellPrice(0, 10, 0, 0);
            //TODO
            //NPC.HitSound = new LegacySoundStyle(3, 4, SoundType.Sound);
            //NPC.DeathSound = new LegacySoundStyle(4, 14, SoundType.Sound);
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.netAlways = true;
            NPC.frame = BaseDrawing.GetFrame(frameCount, fWidth, fHeight, 0, 2);
            //bossBag = mod.ItemType("OrthrusBag");
            NPC.noTileCollide = false;
            //TODO
            //music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Siege");
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale *= 2;
            return true;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                //TODO
                //Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/OrthrusBodyGore1"), 1f);
                //Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/OrthrusBodyGore2"), 1f);
                //Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/OrthrusBodyGore3"), 1f);
                //Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/OrthrusBodyGore4"), 1f);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<OrthrusXTreasureBag>()));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OrthrusXTrophy>(), 10));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(7, ModContent.ItemType<BlueOrthrusXMask>(), ModContent.ItemType<RedOrthrusXMask>()));

            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.SoulofMight, 1, 20, 40));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FulguriteBar>(), 1, 30, 64));

            npcLoot.Add(notExpertRule);
        }

        public Player playerTarget = null;
        public static int AISTATE_TURRET = 0, AISTATE_FLY = 1, AISTATE_RUNAWAY = 2;
        public float[] internalAI = new float[2];

        //clientside stuff
		public int fWidth = 200;
		public int fHeight = 102;

        public Color color;

		public void HandleHeads()
		{
			if(Main.netMode != 1)
			{
				if(!HeadsSpawned)
				{
					Head1 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<OrthrusHead1>(), 0)];
					Head1.ai[0] = NPC.whoAmI;
					Head2 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<OrthrusHead2>(), 0)];				
					Head2.ai[0] = NPC.whoAmI;
					
					Head1.netUpdate = true;
					Head2.netUpdate = true;
					HeadsSpawned = true;
				}
			}else
			{
				if(!HeadsSpawned)
				{
					int[] npcs = BaseAI.GetNPCs(NPC.Center, -1, default(int[]), 200f, null);
					if (npcs != null && npcs.Length > 0)
					{
						foreach (int npcID in npcs)
						{
							NPC npc2 = Main.npc[npcID];
							if (npc2 != null)
							{
								if(Head1 == null && npc2.type == Mod.Find<ModNPC>("OrthrusHead1").Type && npc2.ai[0] == NPC.whoAmI)
								{
									Head1 = npc2;
								}else
								if(Head2 == null && npc2.type == Mod.Find<ModNPC>("OrthrusHead2").Type && npc2.ai[0] == NPC.whoAmI)
								{
									Head2 = npc2;
								}							
							}
						}
					}
					if(Head1 != null && Head2 != null)
					{
						HeadsSpawned = true;
					}
				}
			}
		}
		
		
        public override void AI()
        {
            color = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, BaseDrawing.GetLightColor(NPC.position), BaseDrawing.GetLightColor(NPC.position), Color.Violet, BaseDrawing.GetLightColor(NPC.position), Color.Violet, BaseDrawing.GetLightColor(NPC.position));
            Lighting.AddLight((int)(NPC.Center.X + NPC.width / 2) / 16, (int)(NPC.position.Y + NPC.height / 2) / 16, color.R / 255, color.G / 255, color.B / 255);

            NPC.TargetClosest();
			
			HandleHeads();
			
			
			
           /* if (!HeadsSpawned)
            {
                if (Head1 == null)
                {
                    if (Main.netMode != 1)
                    {
                        Head1 = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("OrthrusHead1"), 0)];
                        Head1.realLife = npc.whoAmI;
                        Head2 = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("OrthrusHead2"), 0)];
                        Head2.realLife = npc.whoAmI;
                    }
                    else
                    {
                        int[] npcs = BaseAI.GetNPCs(npc.Center, -1, default(int[]), 100f, null);
                        if (npcs != null && npcs.Length > 0)
                        {
                            foreach (int npcID in npcs)
                            {
                                NPC npc2 = Main.npc[npcID];
                                if (npc2 != null && npc2.type == mod.NPCType("OrthrusHead1"))
                                {
                                    Head1 = npc2;
                                }
                                if (npc2 != null && npc2.type == mod.NPCType("OrthrusHead2"))
                                {
                                    Head2 = npc2;
                                }
                            }
                        }
                    }
                }
                HeadsSpawned = true;
            }*/

            Player playerTarget = Main.player[NPC.target];

            if (!playerTarget.active || playerTarget.dead || Main.dayTime) //fleeing
			{
                NPC.noTileCollide = true;
                NPC.dontTakeDamage = true;
                NPC.noGravity = true;	
				NPC.noTileCollide = true;
                NPC.velocity.Y -= .05f;
                int SHLOOPX = 34;
                int SHLOOPY = 60;
                if (Head1 != null && Head2 != null)
                {
                    Head1.Center = NPC.Center + new Vector2(SHLOOPX, -SHLOOPY) + NPC.velocity;
                    Head2.Center = NPC.Center + new Vector2(-SHLOOPX, -SHLOOPY) + NPC.velocity;
                }
                if (NPC.position.Y + NPC.velocity.Y <= 0f && Main.netMode != 1) { NPC.active = false; NPC.netUpdate = true; }
                return;
			}
            else
			{	
				if (internalAI[1] == AISTATE_TURRET)
				{
					NPC.noGravity = false;		
					NPC.noTileCollide = false;				
					NPC.velocity.X *= 0.8f;
					if (Math.Abs(playerTarget.Center.X - NPC.Center.X) < 380f) 
					{
						
					}
                    else if(Main.netMode != 1)
					{
						internalAI[1] = AISTATE_FLY;
						NPC.netUpdate = true;
						if(Head1 != null && Head2 != null)
						{
							Head1.ai[1] = AISTATE_FLY;
							Head2.ai[1] = AISTATE_FLY;							 
							Head1.netUpdate = true;
							Head2.netUpdate = true;						
						}
					}
				}
                else if (internalAI[1] == AISTATE_FLY)
				{
                    NPC.noGravity = true;	
					NPC.noTileCollide = true;
					if (Math.Abs(playerTarget.Center.X - NPC.Center.X) > 380f || Collision.SolidCollision(NPC.position, NPC.width, NPC.height)) //make it less then what makes it rise so it doesn't keep locking between them
					{
						playerTarget.Center += new Vector2(0f, -32f);
						for(int m = 0; m < 4; m++)
						{
							BaseAI.AIEye(NPC, ref NPC.ai, false, true, 0.15f, 0.4f, 8f, 2f, 0.5f, 0.5f);
						}
						playerTarget.Center += new Vector2(0f, 32f);						
						int SHLOOPX = 34;
						int SHLOOPY = 60;
                        if (Head1 != null && Head2 != null)
                        {
                            Head1.Center = NPC.Center + new Vector2(SHLOOPX, -SHLOOPY) + NPC.velocity;
                            Head2.Center = NPC.Center + new Vector2(-SHLOOPX, -SHLOOPY) + NPC.velocity;
                        }
                    }
                    else if (Main.netMode != 1) //digs itself out of the ground
					{
						internalAI[1] = AISTATE_TURRET;							
						NPC.netUpdate = true;
						if(Head1 != null && Head2 != null)
						{
							Head1.ai[1] = AISTATE_TURRET;
							Head2.ai[1] = AISTATE_TURRET;							 
							Head1.netUpdate = true;
							Head2.netUpdate = true;						
						}				
					}
				}
            }
            

            if (internalAI[1] == AISTATE_TURRET) //Standing
            {
				NPC.frameCounter++;				
                if (NPC.frameCounter >= 8)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += fHeight;
                    if (NPC.frame.Y > fHeight * 3)
                    {
                        NPC.frame.Y = 0;
                    }
                }
            }
            else //Following
            {
				NPC.frameCounter++;				
                if (NPC.frameCounter >= 5)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += fHeight;
                    if (NPC.frame.Y > fHeight * 7)
                    {
                        NPC.frame.Y = fHeight * 4;
                    }
                }
            }
            for (int m = NPC.oldPos.Length - 1; m > 0; m--)
            {
                NPC.oldPos[m] = NPC.oldPos[m - 1];
            }
            NPC.oldPos[0] = NPC.position;			
        }     

        public void DrawHead(SpriteBatch spriteBatch, string headTexture, string glowMaskTexture, NPC head, Color drawColor, bool leftHead)
        {
            if (head != null && head.active && head.ModNPC != null && head.ModNPC is OrthrusHead1)
            {
                string neckTex = "NPCs/Bosses/Orthrus/OrthrusNeck";
                Texture2D neckTex2D = Mod.GetTexture(neckTex);
                Vector2 neckOrigin = new Vector2(NPC.Center.X, NPC.Center.Y) + new Vector2(leftHead ? -37 : 37, -28);
                Vector2 connector = head.Center - new Vector2(neckTex2D.Width * 0.5f, 0f);
				BaseDrawing.DrawChain(spriteBatch, new Texture2D[] { null, neckTex2D, null }, 0, neckOrigin, connector, neckTex2D.Height - 10f, null, 1f, false, null);					
				spriteBatch.Draw(Mod.GetTexture(headTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, drawColor, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(Mod.GetTexture(glowMaskTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, Color.White, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
			}
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            DrawHead(spriteBatch, "NPCs/Bosses/Orthrus/OrthrusHead1", "NPCs/Bosses/Orthrus/OrthrusHead1_Glow", Head1, drawColor, false);			
            DrawHead(spriteBatch, "NPCs/Bosses/Orthrus/OrthrusHead2", "NPCs/Bosses/Orthrus/OrthrusHead2_Glow", Head2, drawColor, true); 			
			BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position + new Vector2(0f, NPC.gfxOffY), NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, Main.npcFrameCount[NPC.type], NPC.frame, drawColor, false);		         
		    return false;
        }
    }
}