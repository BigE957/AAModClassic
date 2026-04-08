using AAModClassic.___Content.Mire._PreHardmode.Items._BossHydra.BossStandard;
using AAModClassic.___Content.Mire._PreHardmode.Items.Accessories;
using AAModClassic.___Content.Mire._PreHardmode.Items.Materials;
using AAModClassic.___Content.Mire._PreHardmode.Items.Pets;
using AAModClassic.___Content.Mire._PreHardmode.Items.Weapons;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.CrossMod;
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

namespace AAModClassic.___Content.Mire._PreHardmode.NPCs._BossHydra
{
    [AutoloadBossHead]
    public class Hydra : ModNPC
    {
        public NPC Head1;
        public NPC Head2;
        public NPC Head3;
        public NPC Head4;
        public NPC Head5;
        public NPC Head6;
        public NPC Head7;
        public NPC Head8;
        public NPC Head9;
        public bool HeadsSpawned = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hydra");
            Main.npcFrameCount[NPC.type] = 15;
        }

        public override void SetDefaults()
        {
            NPC.npcSlots = 100;
            NPC.width = 130;
            NPC.height = 116;
            NPC.aiStyle = -1;
            NPC.damage = 40;
            NPC.defense = 300;
            NPC.lifeMax = 4000;
            NPC.value = Item.sellPrice(0, 5, 0, 0);
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.Item80;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.noGravity = false;
            NPC.netAlways = true;
            Music = Mod.GetSoundSlot(SoundType.Music, "Music/HydraTheme");
            NPC.buffImmune[BuffID.Poisoned] = true;
        }

        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }

        public override void OnKill()
        {
            if (!AAWorld.downedHydra)
            {
                NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.position.X + (Main.rand.Next(2) == 0 ? 200 : -200), (int)NPC.position.Y - 200, ModContent.NPCType<HarukaShade>());
            }
            AAWorld.downedHydra = true;

            NPC.value = 0f;
            NPC.boss = false;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<HydraBag>()));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HydraTrophy>(), 10));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            if (ContentReplacementSystem.NeedToReplaceContent)
                notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, ModContent.ItemType<HydrasSpear>(), ModContent.ItemType<Mossket>(), ModContent.ItemType<GunkWand>(), ModContent.ItemType<GlowmossBall>(), ModContent.ItemType<ShadowBand>()));

            //notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HydraMask(1-3)>(), 7)); Exists but doesn't drop...?

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HydraHide>(), 1, 30, 50));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AbyssiumOre>(), 1, 40, 90));

            npcLoot.Add(notExpertRule);
        }

        public Rectangle frameBottom = new Rectangle(0, 0, 1, 1);
        public bool chasePlayer = false;
        public bool runningAway = false;
        public Player playerTarget = null;

        public bool TeleportMe1 = false;
        public bool TeleportMe2 = false;
        public bool TeleportMe3 = false;

		public void HandleHeads()
		{
			if(Main.netMode != NetmodeID.MultiplayerClient)
			{
				if(!HeadsSpawned)
				{
                    headindex[0] = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<HydraHead1>(), 0);
					Head1 = Main.npc[headindex[0]];
					Head1.ai[0] = NPC.whoAmI;

                    headindex[1] = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<HydraHead2>(), 0);
					Head2 = Main.npc[headindex[1]];
					Head2.ai[0] = NPC.whoAmI;

                    headindex[2] = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<HydraHead3>(), 0);
					Head3 = Main.npc[headindex[2]];
					Head3.ai[0] = NPC.whoAmI;					

					Head1.netUpdate = true;
					Head2.netUpdate = true;
					Head3.netUpdate = true;
					HeadsSpawned = true;
                    NPC.netUpdate = true;
				}
			}
            else
			{
				if(!HeadsSpawned)
				{
                    if(headindex[0] != -1)
                    {
                        Head1 = Main.npc[headindex[0]];
					    Head1.ai[0] = NPC.whoAmI;
                    }
                    if(headindex[1] != -1)
                    {
                        Head2 = Main.npc[headindex[1]];
					    Head1.ai[0] = NPC.whoAmI;
                    }
                    if(headindex[2] != -1)
                    {
                        Head3 = Main.npc[headindex[2]];
					    Head1.ai[0] = NPC.whoAmI;
                    }

					if(Head1 != null && Head2 != null && Head3 != null)
					{
						HeadsSpawned = true;
					}
				}
			}
		}

        public override void AI()
        {

            if (Main.dayTime)
            {
                AIMovementRunAway();
                return;
            }

            HandleHeads();

            if (playerTarget != null)
            {
                float dist = NPC.Distance(playerTarget.Center);
                if (!playerTarget.GetModPlayer<AAPlayer>().ZoneMire)
                {
                    NPC.alpha += 3;
                    if (NPC.alpha >= 255)
                    {
                        NPC.alpha = 255;
                    }
                    if (dist > 700 || !Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                    {
                        NPC.alpha += 3;
                        if (NPC.alpha >= 255)
                        {
                            Vector2 tele = new Vector2(playerTarget.Center.X + (Main.rand.Next(2) == 0 ? 120 : -120), playerTarget.Center.Y - 16);
                            TeleportMe1 = true;
                            TeleportMe2 = true;
                            TeleportMe3 = true;
                            NPC.Center = tele;
                            NPC.netUpdate = true;
                        }
                    }
                }
                else
                {
                    if (dist > 700 || !Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                    {
                        NPC.alpha += 3;
                        if (NPC.alpha >= 255)
                        {
                            Vector2 tele = new Vector2(playerTarget.Center.X + (Main.rand.Next(2) == 0 ? 120 : -120), playerTarget.Center.Y - 16);
                            TeleportMe1 = true;
                            TeleportMe2 = true;
                            TeleportMe3 = true;
                            NPC.Center = tele;
                            NPC.netUpdate = true;
                        }
                    }
                    else
                    {
                        NPC.alpha -= 3;
                        if (NPC.alpha <= 0)
                        {
                            NPC.alpha = 0;
                        }
                    }
                }
            }

            for (int m = NPC.oldPos.Length - 1; m > 0; m--)
            {
                NPC.oldPos[m] = NPC.oldPos[m - 1];
            }
            NPC.oldPos[0] = NPC.position;

            bool foundTarget = TargetClosest();

            if (!runningAway && foundTarget)
            {
                if (Math.Abs(NPC.velocity.X) > 12f) NPC.velocity.X *= 0.8f;
                if (Math.Abs(NPC.velocity.Y) > 12f) NPC.velocity.Y *= 0.8f;
                if (NPC.velocity.Y > 7f) NPC.velocity.Y *= 0.75f;
                NPC.timeLeft = 50;
                AIMovementNormal();
            }
            else
            {
                runningAway = true;
                AIMovementRunAway();
                return;
            }
            
            bool noHeads = !NPC.AnyNPCs(ModContent.NPCType<HydraHead1>()) && !NPC.AnyNPCs(ModContent.NPCType<HydraHead2>()) && !NPC.AnyNPCs(ModContent.NPCType<HydraHead3>()) &&
                !NPC.AnyNPCs(ModContent.NPCType<HydraHead4>()) && !NPC.AnyNPCs(ModContent.NPCType<HydraHead5>()) && !NPC.AnyNPCs(ModContent.NPCType<HydraHead6>()) &&
                !NPC.AnyNPCs(ModContent.NPCType<HydraHead7>()) && !NPC.AnyNPCs(ModContent.NPCType<HydraHead8>()) && !NPC.AnyNPCs(ModContent.NPCType<HydraHead9>());

            if (HeadsSpawned && noHeads)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.life = 0;
                    NPC.checkDead();
                    NPC.netUpdate = true;
                }
                return;
            }
        }

        public float[] internalAI = new float[1];

        public int[] headindex = {-1, -1, -1};
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(headindex[0]);
                writer.Write(headindex[1]);
                writer.Write(headindex[2]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadFloat();
                headindex[0] = reader.ReadInt();
                headindex[1] = reader.ReadInt();
                headindex[2] = reader.ReadInt();
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.velocity.X != 0)
                NPC.spriteDirection = NPC.velocity.X > 0 ? 1 : -1;

            NPC.frameCounter--;
            if (NPC.frameCounter <= 0)
            {
                NPC.frameCounter = 5;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y > frameHeight * 14)
                {
                    NPC.frame.Y = frameHeight * 2;
                }
            }
            if (NPC.velocity.X == 0)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = 0;
            }
            if (NPC.velocity.Y != 0)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = frameHeight;
            }
        }

        public void AIMovementRunAway()
        {
            NPC.alpha += 2;
            if (Main.netMode != NetmodeID.MultiplayerClient) internalAI[0] += 2;
            if (internalAI[0] >= 255)
            {
                NPC.active = false;
                NPC.netUpdate = true;
            }
        }

        public void AIMovementNormal()
        {
            BaseAI.AIZombie(NPC, ref NPC.ai, false, false, -1, 0.07f, 3f, 14, 20, 1, true, 1, 1, true, null, false);
            NPC.rotation = 0f;
        }

        public bool TargetClosest()
        {
            int[] players = BaseAI.GetPlayers(NPC.Center, 2000f);
            float dist = 999999;
            int foundPlayer = -1;
            if (foundPlayer != -1)
            {
                BaseAI.SetTarget(NPC, foundPlayer);
                playerTarget = Main.player[foundPlayer];
                return true;
            }
            else
            {
                for (int m = 0; m < players.Length; m++)
                {
                    Player p = Main.player[players[m]];
                    if (Vector2.Distance(p.Center, NPC.Center) < dist)
                    {
                        dist = Vector2.Distance(p.Center, NPC.Center);
                        foundPlayer = p.whoAmI;
                    }
                }
            }
            if (foundPlayer != -1)
            {
                BaseAI.SetTarget(NPC, foundPlayer);
                playerTarget = Main.player[foundPlayer];
                return true;
            }
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("HydraGoreBody").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("HydraGoreLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("HydraGoreLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("HydraGoreLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("HydraGoreLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("HydraGoreTail").Type, 1f);
            }
        }
        private static float X(float t,
        float x0, float x1, float x2)
        {
            return (float)(
                x0 * Math.Pow(1 - t, 2) +
                x1 * 2 * t * Math.Pow(1 - t, 1) +
                x2 * Math.Pow(t, 2)
            );
        }
        private static float Y(float t,
            float y0, float y1, float y2)
        {
            return (float)(
                 y0 * Math.Pow(1 - t, 2) +
                 y1 * 2 * t * Math.Pow(1 - t, 1) +
                 y2 * Math.Pow(t, 2)
             );
        }
        public void DrawHead(SpriteBatch spriteBatch, string headTexture, string glowMaskTexture, NPC head, Color drawColor)
        {
            if (head != null && head.active && head.ModNPC != null && head.ModNPC is HydraHead1)
            {
                string neckTex = "NPCs/Bosses/Hydra/HydraNeck";
                Texture2D neckTex2D = Mod.GetTexture(neckTex);
                Vector2 neckOrigin = new Vector2(NPC.Center.X, NPC.Center.Y - 30);
                Vector2 connector = head.Center;
                float chainsPerUse = 0.05f;
                for (float i = 0; i <= 1; i += chainsPerUse)
                {
                    Vector2 distBetween;
                    float projTrueRotation;
                    if (i != 0)
                    {
                        distBetween = new Vector2(X(i, neckOrigin.X, (neckOrigin.X + connector.X) / 2, connector.X) -
                        X(i - chainsPerUse, neckOrigin.X, (neckOrigin.X + connector.X) / 2, connector.X),
                        Y(i, neckOrigin.Y, neckOrigin.Y + 50, connector.Y) -
                        Y(i - chainsPerUse, neckOrigin.Y, neckOrigin.Y + 50, connector.Y));
                        projTrueRotation = distBetween.ToRotation() - (float)Math.PI / 2;
                        spriteBatch.Draw(neckTex2D, new Vector2(X(i, neckOrigin.X, (neckOrigin.X + connector.X) / 2, connector.X) - Main.screenPosition.X, Y(i, neckOrigin.Y, neckOrigin.Y + 50, connector.Y) - Main.screenPosition.Y),
                        new Rectangle(0, 0, neckTex2D.Width, neckTex2D.Height), drawColor, projTrueRotation,
                        new Vector2(neckTex2D.Width * 0.5f, neckTex2D.Height * 0.5f), 1f, SpriteEffects.None, 0f);
                    }
                }
                spriteBatch.Draw(Mod.GetTexture(headTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, drawColor, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Mod.GetTexture(glowMaskTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, Color.White, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            drawColor = NPC.GetAlpha(drawColor);

            int frameWidth = 152;
            frameBottom = BaseDrawing.GetFrame(0, frameWidth, 44, 0, 2);

            HeadDraw(spriteBatch, drawColor);

            string tailTex = "NPCs/Bosses/Hydra/HydraTail";
            BaseDrawing.DrawTexture(spriteBatch, Mod.GetTexture(tailTex), 0, NPC.position + new Vector2(0f, NPC.gfxOffY - 30), NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, 1, frameBottom, drawColor, false);
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position + new Vector2(0f, NPC.gfxOffY), NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, Main.npcFrameCount[NPC.type], NPC.frame, drawColor, false);

            if (Head1 != null)
            {
                DrawHead(spriteBatch, "NPCs/Bosses/Hydra/HydraHead1", "Glowmasks/HydraHead1_Glow", Head1, drawColor); //draw main head last!
            }
            return false;
        }

        public void HeadDraw(SpriteBatch sb, Color drawColor)
        {
            if (Head2 != null)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead2", "Glowmasks/HydraHead2_Glow", Head2, drawColor);
            }

            if (Head3 != null)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead3", "Glowmasks/HydraHead3_Glow", Head3, drawColor);
            }

            if (Head4 != null)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead4", "Glowmasks/HydraHead4_Glow", Head4, drawColor);
            }

            if (Head5 != null)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead5", "Glowmasks/HydraHead5_Glow", Head5, drawColor);
            }

            if (Head6 != null)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead6", "Glowmasks/HydraHead6_Glow", Head6, drawColor);
            }

            if (Head7 != null)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead7", "Glowmasks/HydraHead5_Glow", Head7, drawColor);
            }

            if (Head8 != null)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead8", "Glowmasks/HydraHead4_Glow", Head8, drawColor);
            }

            if (Head9 != null)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead9", "Glowmasks/HydraHead6_Glow", Head9, drawColor);
            }
        }
    }
}