using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossOrthrusX.BossStandard;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic._Unreleased.Content.Parthenan.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
using AAModClassic.UI.WorldGen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossOrthrusX
{
    [AutoloadBossHead]
    public class OrthrusXBody : YamataBoss
	{
        public OrthrusXHead HeadBlue;
        public OrthrusXHead HeadRed;
        public int[] Heads = null;
        public bool HeadsSpawned = false;

        public static Asset<Texture2D> Glowmask1;
        public static Asset<Texture2D> Glowmask2;
        public static Asset<Texture2D> NeckTexture;
        public static Asset<Texture2D> HeadTex;
        public static Asset<Texture2D> HeadGlowmask;
        public static Asset<Texture2D> HeadGlowmaskBlue;
        public static Asset<Texture2D> HeadGlowmaskRed;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadSingle();
                internalAI[1] = reader.ReadSingle();
            }
        }

        public override void SetStaticDefaults()
        {
            displayName = "Orthrus X";
            Main.npcFrameCount[NPC.type] = 12;

            Glowmask1 = ModContent.Request<Texture2D>(Texture + "_Glow1");
            Glowmask2 = ModContent.Request<Texture2D>(Texture + "_Glow2");
            NeckTexture = ModContent.Request<Texture2D>(Texture + "_Neck");

            HeadTex = ModContent.Request<Texture2D>(ModContent.GetModNPC(ModContent.NPCType<OrthrusXHead>()).Texture);
            HeadGlowmask = ModContent.Request<Texture2D>(ModContent.GetModNPC(ModContent.NPCType<OrthrusXHead>()).Texture + "_Glow");
            HeadGlowmaskBlue = ModContent.Request<Texture2D>(ModContent.GetModNPC(ModContent.NPCType<OrthrusXHead>()).Texture + "_Glow_Blue");
            HeadGlowmaskRed = ModContent.Request<Texture2D>(ModContent.GetModNPC(ModContent.NPCType<OrthrusXHead>()).Texture + "_Glow_Red");
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
            NPC.value = Item.buyPrice(0, 10, 0, 0);
            //TODO
            //NPC.HitSound = new LegacySoundStyle(3, 4, SoundType.Sound);
            //NPC.DeathSound = new LegacySoundStyle(4, 14, SoundType.Sound);
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.netAlways = true;
            NPC.frame = BaseDrawing.GetFrame(frameCount, fWidth, fHeight, 0, 2);
            NPC.noTileCollide = false;
            Music = MusicManagementSystem.MusicSlots["Siege"];
            SpawnModBiomes = [ModContent.GetInstance<ParthenanBiome>().Type];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.OrthrusX")
            ]);
        }

        public override void BossLoot(ref int potionType)
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
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OrthrusBodyGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OrthrusBodyGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OrthrusBodyGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OrthrusBodyGore4").Type, 1f);
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
            if (HeadBlue is OrthrusXHead)
                HeadsSpawned = false;
            if (HeadBlue == null || HeadRed == null)
                HeadsSpawned = false;

            if (!HeadsSpawned)
            {
                if (HeadBlue == null)
                {
                    NPC npc = NPC.NewNPCDirect(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<OrthrusXHead>(), 0);
                    HeadBlue = npc.ModNPC as OrthrusXHead;
                    HeadBlue.NPC.ai[0] = NPC.whoAmI;
                }
                if (HeadRed == null)
                {
                    NPC npc = NPC.NewNPCDirect(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<OrthrusXHead>(), 0);
                    HeadRed = npc.ModNPC as OrthrusXHead;
                    HeadRed.NPC.ai[0] = NPC.whoAmI;
                    HeadRed.redHead = true;
                }

                HeadBlue.NPC.netUpdate = true;
                HeadRed.NPC.netUpdate = true;
                HeadsSpawned = true;
            }
        }
		
		
        public override void AI()
        {
            color = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, BaseDrawing.GetLightColor(NPC.position), BaseDrawing.GetLightColor(NPC.position), Color.Violet, BaseDrawing.GetLightColor(NPC.position), Color.Violet, BaseDrawing.GetLightColor(NPC.position));
            Lighting.AddLight((int)(NPC.Center.X + NPC.width / 2) / 16, (int)(NPC.position.Y + NPC.height / 2) / 16, color.R / 255, color.G / 255, color.B / 255);

            NPC.TargetClosest();
			
			HandleHeads();

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
                if (HeadBlue != null && HeadRed != null)
                {
                    HeadBlue.NPC.Center = NPC.Center + new Vector2(SHLOOPX, -SHLOOPY) + NPC.velocity;
                    HeadRed.NPC.Center = NPC.Center + new Vector2(-SHLOOPX, -SHLOOPY) + NPC.velocity;
                }
                if (NPC.position.Y + NPC.velocity.Y <= 0f && Main.netMode != NetmodeID.MultiplayerClient) { NPC.active = false; NPC.netUpdate = true; }
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
                    else if(Main.netMode != NetmodeID.MultiplayerClient)
					{
						internalAI[1] = AISTATE_FLY;
						NPC.netUpdate = true;
						if(HeadBlue != null && HeadRed != null)
						{
							HeadBlue.NPC.ai[1] = AISTATE_FLY;
							HeadRed.NPC.ai[1] = AISTATE_FLY;							 
							HeadBlue.NPC.netUpdate = true;
							HeadRed.NPC.netUpdate = true;						
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
                        if (HeadBlue != null && HeadRed != null)
                        {
                            HeadBlue.NPC.Center = NPC.Center + new Vector2(SHLOOPX, -SHLOOPY) + NPC.velocity;
                            HeadRed.NPC.Center = NPC.Center + new Vector2(-SHLOOPX, -SHLOOPY) + NPC.velocity;
                        }
                    }
                    else if (Main.netMode != NetmodeID.MultiplayerClient) //digs itself out of the ground
					{
						internalAI[1] = AISTATE_TURRET;							
						NPC.netUpdate = true;
						if(HeadBlue != null && HeadRed != null)
						{
							HeadBlue.NPC.ai[1] = AISTATE_TURRET;
							HeadRed.NPC.ai[1] = AISTATE_TURRET;							 
							HeadBlue.NPC.netUpdate = true;
							HeadRed.NPC.netUpdate = true;						
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

        public Color purple;

        public void DrawHead(SpriteBatch spriteBatch, Texture2D headTexture, Texture2D glowMaskTexture, NPC head, Color drawColor, bool leftHead)
        {
            if (head != null && head.active && head.ModNPC != null && head.ModNPC is OrthrusXHead)
            {
                Vector2 neckOrigin = new Vector2(NPC.Center.X, NPC.Center.Y) + new Vector2(leftHead ? -37 : 37, -28);
                Vector2 connector = head.Center - new Vector2(NeckTexture.Value.Width * 0.5f, 0f);
                Vector2 dir = neckOrigin.DirectionTo(connector);
                float length = Vector2.Distance(neckOrigin, connector);
                for (int i = 0; i < length; i += (NeckTexture.Value.Height - 10))
                {
                    Vector2 drawPos = neckOrigin + dir * i;
                    spriteBatch.Draw(NeckTexture.Value, drawPos - Main.screenPosition, null, Lighting.GetColor(drawPos.ToTileCoordinates()), dir.ToRotation() - MathHelper.PiOver2, NeckTexture.Size() * 0.5f, 1f, 0, 0);
                }
				spriteBatch.Draw(headTexture, new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, drawColor, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(glowMaskTexture, new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, Color.White, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
                if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                    spriteBatch.Draw(HeadGlowmask.Value, new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, purple, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            purple = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, BaseDrawing.GetLightColor(NPC.position), BaseDrawing.GetLightColor(NPC.position), Color.Violet, BaseDrawing.GetLightColor(NPC.position), Color.Violet, BaseDrawing.GetLightColor(NPC.position));

            if (HeadBlue != null)
                DrawHead(spriteBatch, HeadTex.Value, HeadGlowmaskBlue.Value, HeadBlue.NPC, drawColor, false);
            if (HeadRed != null)
                DrawHead(spriteBatch, HeadTex.Value, HeadGlowmaskRed.Value, HeadRed.NPC, drawColor, true); 			
			BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position + new Vector2(0f, NPC.gfxOffY), NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, Main.npcFrameCount[NPC.type], NPC.frame, drawColor, false);
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                BaseDrawing.DrawTexture(spriteBatch, Glowmask1.Value, 0, NPC.position + new Vector2(0f, NPC.gfxOffY), NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, Main.npcFrameCount[NPC.type], NPC.frame, purple, false);
                BaseDrawing.DrawTexture(spriteBatch, Glowmask2.Value, 0, NPC.position + new Vector2(0f, NPC.gfxOffY), NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, Main.npcFrameCount[NPC.type], NPC.frame, Color.White, false);
            }
            return false;
        }
    }
}