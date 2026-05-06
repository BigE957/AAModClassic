using AAModClassic._Content.Desert.___PreHardmode.NPCs.__BossDesertDjinn;
using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.BossStandard;
using AAModClassic._Content.Desert.__Hardmode.Items.Quest;
using AAModClassic._Content.Desert.__Hardmode.Items.Weapons;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossFeudalFungus;
using AAModClassic._Content.Inferno.___PreHardmode.NPCs.__BossBroodmother;
using AAModClassic._Content.Mire.___PreHardmode.NPCs.__BossHydra;
using AAModClassic._Content.RedMushroom.___PreHardmode.NPCs.__BossMushroomMonarch;
using AAModClassic._Content.Snow.___PreHardmode.NPCs.__BossSubzeroSerpent;
using AAModClassic._Content.Stars._PostMoonlord.Items.Quest;
using AAModClassic.Backgrounds;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.CrossMod;
using AAModClassic.NPCs.Bosses.Anubis;
using AAModClassic.NPCs.Bosses.Anubis.Forsaken;
using AAModClassic.NPCs.Bosses.Athena;
using AAModClassic.NPCs.Bosses.Athena.Olympian;
using AAModClassic.NPCs.Bosses.Greed;
using AAModClassic.NPCs.Bosses.Rajah;
using AAModClassic.NPCs.TownNPCs;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace AAModClassic._Unofficial.Desert
{
    [AutoloadHead]
	public class LegendscribeUnofficial : ModNPC
	{
        private static int ShimmerHeadIndex;

        public static Asset<Texture2D> Shimmer;
        public static Asset<Texture2D> Glowmask;
        public static Asset<Texture2D> GlowmaskShimmer;
        public static Asset<Texture2D> PartyHat;

        public override void Load()
        {
            ShimmerHeadIndex = Mod.AddNPCHeadTexture(Type, Texture + "_Shimmer_Head");

            Shimmer = ModContent.Request<Texture2D>(Texture + "_Shimmer");
            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            GlowmaskShimmer = ModContent.Request<Texture2D>(Texture + "_Shimmer_Glow");
            PartyHat = ModContent.Request<Texture2D>(Texture + "_PartyHat");

            On_Main.DoDraw_DrawNPCsOverTiles += GeneralDrawLayer_DrawToLayer_NPCs;
        }

        // we wanna draw him under npcs bcuz he is so massive
        private static void GeneralDrawLayer_DrawToLayer_NPCs(On_Main.orig_DoDraw_DrawNPCsOverTiles orig, Main self)
        {
            foreach (NPC npc in Main.npc)
            {
                if (npc.whoAmI != -1 && npc.active && npc.type == ModContent.NPCType<LegendscribeUnofficial>())
                {
                    Texture2D tex = npc.IsShimmerVariant ? Shimmer.Value : TextureAssets.Npc[npc.type].Value;
                    Texture2D glow = npc.IsShimmerVariant ? GlowmaskShimmer.Value : Glowmask.Value;

                    Vector2 position = npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY - 8);
                    Color color = Lighting.GetColor(npc.Center.ToTileCoordinates()) * npc.Opacity;

                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
                    Main.spriteBatch.Draw(tex, position, npc.frame, color, npc.rotation, npc.frame.Size() / 2f, npc.scale, npc.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
                    Main.spriteBatch.Draw(glow, position, npc.frame, Color.White * npc.Opacity, npc.rotation, npc.frame.Size() / 2f, npc.scale, npc.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
                    if (BirthdayParty.ManualParty || BirthdayParty.GenuineParty)
                        Main.spriteBatch.Draw(PartyHat.Value, position, npc.frame, color, npc.rotation, npc.frame.Size() / 2f, npc.scale, npc.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
                    Main.spriteBatch.End();
                }
            }
            
            orig(self);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            return false;
        }

        public override void ModifyTypeName(ref string typeName)
        {
            typeName = "Legendscribe";
        }

        public override ITownNPCProfile TownNPCProfile()
        {
            return new Profiles.StackedNPCProfile(
                new Profiles.DefaultNPCProfile(Texture, NPCHeadLoader.GetHeadSlot(HeadTexture)),
                new Profiles.DefaultNPCProfile(Texture + "_Shimmer", ShimmerHeadIndex)
            );
        }

        public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 19;
			NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
			NPCID.Sets.AttackFrameCount[NPC.type] = 4;

			NPCID.Sets.DangerDetectRange[NPC.type] = 700;
			NPCID.Sets.AttackType[NPC.type] = 0;
			NPCID.Sets.AttackTime[NPC.type] = 40;
			NPCID.Sets.AttackAverageChance[NPC.type] = 20;

			NPCID.Sets.HatOffsetY[NPC.type] = 3;

            NPCID.Sets.ShimmerTownTransform[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 32;
            NPC.height = 74;
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.damage = 10;
            NPC.defense = 68;
            NPC.lifeMax = 160000;
            NPC.HitSound = SoundID.NPCHit23;
            NPC.DeathSound = SoundID.NPCDeath39;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.dontTakeDamageFromHostiles = true;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.buffImmune[BuffID.Shimmer] = false;
        }

        public float AwayFromPlayerTimer = 0;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(AwayFromPlayerTimer);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                AwayFromPlayerTimer = reader.ReadSingle();
            }
        }

		public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (player.active && !NPC.AnyNPCs(ModContent.NPCType<Anubis>()) && 
                    !NPC.AnyNPCs(ModContent.NPCType<FATransition>()) &&
                    !NPC.AnyNPCs(ModContent.NPCType<FATransition2>()) &&
                    !NPC.AnyNPCs(ModContent.NPCType<ForsakenAnubis>()))
                {
                    return true;
                }
            }
            return false;
		}

        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            bool isTallEnough = false;
            // the first block is fake for some reason. this checks for 5 blocks
            if (bottom - top >= 4)
                isTallEnough = true;
            else
                return false;

            bool hasGate = false;
            for (int x = left - 1; x <= right + 1; x++)
            {
                if (hasGate)
                    break;
                for (int y = top; y <= bottom; y++)
                {
                    int type = Main.tile[x, y].TileType;
                    if (type == TileID.TallGateOpen || type == TileID.TallGateClosed)
                    {
                        hasGate = true;
                        break;
                    }
                }
            }

            return hasGate && isTallEnough;
        }

        public override List<string> SetNPCNameList()
		{
            return ["Anubis"];
        }

        public override bool PreAI()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Anubis>()) ||
                NPC.AnyNPCs(ModContent.NPCType<FATransition>()) ||
                NPC.AnyNPCs(ModContent.NPCType<FATransition2>()) ||
                NPC.AnyNPCs(ModContent.NPCType<ForsakenAnubis>()))
            {
                TPDust();
                NPC.active = false;
            }
            if (Vector2.Distance(NPC.position, new Vector2(NPC.homeTileX, NPC.homeTileY)) > 3000 && AwayFromPlayerTimer < 240 && !NPC.homeless)
            {
                AwayFromPlayerTimer++;
                if (AwayFromPlayerTimer >= 240)
                {
                    bool IsNearbyPlayer = false;
                    for (int k = 0; k < 2; k++)
                    {
                        Rectangle NPCNearbyRectangle = new Rectangle((int)(NPC.position.X + NPC.width / 2 - NPC.sWidth / 2 - NPC.safeRangeX), (int)(NPC.position.Y + NPC.height / 2 - NPC.sHeight / 2 - NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
                        if (k == 1)
                        {
                            NPCNearbyRectangle = new Rectangle(NPC.homeTileX * 16 + 8 - NPC.sWidth / 2 - NPC.safeRangeX, NPC.homeTileY * 16 + 8 - NPC.sHeight / 2 - NPC.safeRangeY, NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
                        }
                        for (int l = 0; l < 255; l++)
                        {
                            if (Main.player[l].active)
                            {
                                Rectangle PlayerNearbyRectangle = new Rectangle((int)Main.player[l].position.X, (int)Main.player[l].position.Y, Main.player[l].width, Main.player[l].height);
                                if (PlayerNearbyRectangle.Intersects(NPCNearbyRectangle))
                                {
                                    IsNearbyPlayer = true;
                                    break;
                                }
                            }
                            if (IsNearbyPlayer)
                                break;
                        }
                    }
                    if (!IsNearbyPlayer)
                    {
                        if (!Collision.SolidTiles(NPC.homeTileX - 1, NPC.homeTileX + 1, NPC.homeTileY - 3, NPC.homeTileY - 1))
                        {
                            TPDust();
                            // why are you talkingwhen nobody sees you???
                            CombatText.NewText(NPC.Hitbox, Color.Gold, Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.CombatTextChat"));
                            NPC.velocity.X = 0f;
                            NPC.velocity.Y = 0f;
                            NPC.position.X = NPC.homeTileX * 16 + 8 - NPC.width / 2;
                            NPC.position.Y = NPC.homeTileY * 16 - NPC.height - 0.1f;
                            NPC.netUpdate = true;
                            AwayFromPlayerTimer = 0;
                        }
                    }
                }
            }
            return true;
        }

        public override void PostAI()
        {
            /*
            Main.NewText("ai 0: " + NPC.ai[0]);
            Main.NewText("ai 1: " + NPC.ai[1]);
            Main.NewText("ai 2: " + NPC.ai[2]);
            Main.NewText("ai 3: " + NPC.ai[3]);
            Main.NewText("localai 0: " + NPC.localAI[0]);
            Main.NewText("localai 1: " + NPC.localAI[1]);
            Main.NewText("localai 2: " + NPC.localAI[2]);
            Main.NewText("localai 3: " + NPC.localAI[3]);
            Main.NewText("framecounter: " + NPC.frameCounter);
            Main.NewText("frame: " + NPC.frame);
            */

            // slow down his walking anim. npc.ai[0] being 1 means hes in walk mode and is walking
            if (NPC.ai[0] == 1)
                NPC.frameCounter -= 1;

            Point npcCenterInTiles = new Point((int)NPC.Center.X / 16, (int)NPC.Center.Y / 16);

            Rectangle openGateRectangle = new Rectangle(npcCenterInTiles.X - 2, npcCenterInTiles.Y - 1, 5, 5);
            for (int x = openGateRectangle.X; x < openGateRectangle.X + openGateRectangle.Width; x++)
            {
                for (int y = openGateRectangle.Y; y < openGateRectangle.Y + openGateRectangle.Height; y++)
                {
                    if (Main.tile[x, y].TileType == TileID.TallGateClosed)
                        WorldGen.ShiftTallGate(x, y, false);
                }
            }

            Rectangle closeGateRectangle = new Rectangle(npcCenterInTiles.X - 3, npcCenterInTiles.Y - 1, 8, 5);
            for (int x = closeGateRectangle.X; x < closeGateRectangle.X + closeGateRectangle.Width; x++)
            {
                for (int y = closeGateRectangle.Y; y < closeGateRectangle.Y + closeGateRectangle.Height; y++)
                {
                    if (Main.tile[x, y].TileType == TileID.TallGateOpen && ((x < openGateRectangle.X || x > openGateRectangle.X + openGateRectangle.Width) || (y < openGateRectangle.Y || y > openGateRectangle.Y + openGateRectangle.Height)))
                        WorldGen.ShiftTallGate(x, y, true);
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            int type = NPC.type;
            int num165 = NPCID.Sets.ExtraFramesCount[NPC.type];

            // yes honey we do have to reimplement all of the general npc findframe stuff for anubis
            // tldr if u put in an animationstyle itll pull ALL data from the npc ur pulling anim style from
            // even if u have different frame counts itll pull from ur animstyle guy
            // tml should really fix this
            if (NPC.velocity.Y == 0f)
            {
                if (NPC.direction == 1)
                {
                    NPC.spriteDirection = 1;
                }
                if (NPC.direction == -1)
                {
                    NPC.spriteDirection = -1;
                }
                int num166 = Main.npcFrameCount[type] - NPCID.Sets.AttackFrameCount[type];
                if (NPC.ai[0] == 23f)
                {
                    NPC.frameCounter += 1.0;
                    int num167 = NPC.frame.Y / frameHeight;
                    int num85 = num166 - num167;
                    if ((uint)(num85 - 1) > 1u && (uint)(num85 - 4) > 1u && num167 != 0)
                    {
                        NPC.frame.Y = 0;
                        NPC.frameCounter = 0.0;
                    }
                    int num168 = 0;
                    num168 = !(NPC.frameCounter < 6.0) ? num166 - 4 : num166 - 5;
                    if (NPC.ai[1] < 6f)
                    {
                        num168 = num166 - 5;
                    }
                    NPC.frame.Y = frameHeight * num168;
                }
                else if (NPC.ai[0] >= 20f && NPC.ai[0] <= 22f)
                {
                    int num170 = NPC.frame.Y / frameHeight;
                    switch ((int)NPC.ai[0])
                    {
                        case 20:
                            if (NPC.ai[1] > 30f && (num170 < 23 || num170 > 27))
                            {
                                num170 = 23;
                            }
                            if (num170 > 0)
                            {
                                NPC.frameCounter += 1.0;
                            }
                            if (NPC.frameCounter > 4.0)
                            {
                                NPC.frameCounter = 0.0;
                                num170++;
                                if (num170 > 26 && NPC.ai[1] > 30f)
                                {
                                    num170 = 24;
                                }
                                if (num170 > 27)
                                {
                                    num170 = 0;
                                }
                            }
                            break;
                        case 21:
                            if (NPC.ai[1] > 30f && (num170 < 17 || num170 > 22))
                            {
                                num170 = 17;
                            }
                            if (num170 > 0)
                            {
                                NPC.frameCounter += 1.0;
                            }
                            if (NPC.frameCounter > 4.0)
                            {
                                NPC.frameCounter = 0.0;
                                num170++;
                                if (num170 > 21 && NPC.ai[1] > 30f)
                                {
                                    num170 = 18;
                                }
                                if (num170 > 22)
                                {
                                    num170 = 0;
                                }
                            }
                            break;
                    }
                    NPC.frame.Y = num170 * frameHeight;
                }
                else if (NPC.ai[0] == 2f)
                {
                    NPC.frameCounter += 1.0;
                    if (NPC.frame.Y / frameHeight == num166 - 1 && NPC.frameCounter >= 5.0)
                    {
                        NPC.frame.Y = 0;
                        NPC.frameCounter = 0.0;
                    }
                    else if (NPC.frame.Y / frameHeight == 0 && NPC.frameCounter >= 40.0)
                    {
                        NPC.frame.Y = frameHeight * (num166 - 1);
                        NPC.frameCounter = 0.0;
                    }
                    else if (NPC.frame.Y != 0 && NPC.frame.Y != frameHeight * (num166 - 1))
                    {
                        NPC.frame.Y = 0;
                        NPC.frameCounter = 0.0;
                    }
                }
                else if (NPC.ai[0] == 11f)
                {
                    NPC.frameCounter += 1.0;
                    if (NPC.frame.Y / frameHeight == num166 - 1 && NPC.frameCounter >= 50.0)
                    {
                        if (NPC.frameCounter == 50.0)
                        {
                            int num172 = Main.rand.Next(4);
                            for (int m = 0; m < 3 + num172; m++)
                            {
                                int num173 = Dust.NewDust(NPC.Center + Vector2.UnitX * -NPC.direction * 8f - Vector2.One * 5f + Vector2.UnitY * 8f, 3, 6, DustID.PirateStaff, -NPC.direction, 1f);
                                Main.dust[num173].velocity /= 2f;
                                Main.dust[num173].scale = 0.8f;
                            }
                            if (Main.rand.Next(30) == 0)
                            {
                                int num174 = Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center + Vector2.UnitX * -NPC.direction * 8f, Vector2.Zero, Main.rand.Next(580, 583));
                                Main.gore[num174].velocity /= 2f;
                                Main.gore[num174].velocity.Y = Math.Abs(Main.gore[num174].velocity.Y);
                                Main.gore[num174].velocity.X = (0f - Math.Abs(Main.gore[num174].velocity.X)) * NPC.direction;
                            }
                        }
                        if (NPC.frameCounter >= 100.0 && Main.rand.Next(20) == 0)
                        {
                            NPC.frame.Y = 0;
                            NPC.frameCounter = 0.0;
                        }
                    }
                    else if (NPC.frame.Y / frameHeight == 0 && NPC.frameCounter >= 20.0)
                    {
                        NPC.frame.Y = frameHeight * (num166 - 1);
                        NPC.frameCounter = 0.0;
                        EmoteBubble.NewBubble(89, new WorldUIAnchor(NPC), 90);
                    }
                    else if (NPC.frame.Y != 0 && NPC.frame.Y != frameHeight * (num166 - 1))
                    {
                        NPC.frame.Y = 0;
                        NPC.frameCounter = 0.0;
                    }
                }
                else if (NPC.ai[0] == 5f)
                {
                    NPC.frame.Y = frameHeight * (num166 - 3);
                    NPC.frameCounter = 0.0;
                }
                else if (NPC.ai[0] == 6f)
                {
                    NPC.frameCounter += 1.0;
                    int num175 = NPC.frame.Y / frameHeight;
                    int num84 = num166 - num175;
                    if ((uint)(num84 - 1) > 1u && (uint)(num84 - 4) > 1u && num175 != 0)
                    {
                        NPC.frame.Y = 0;
                        NPC.frameCounter = 0.0;
                    }
                    int num176 = 0;
                    num176 = !(NPC.frameCounter < 10.0) ? NPC.frameCounter < 16.0 ? num166 - 5 : NPC.frameCounter < 46.0 ? num166 - 4 : NPC.frameCounter < 60.0 ? num166 - 5 : !(NPC.frameCounter < 66.0) ? NPC.frameCounter < 72.0 ? num166 - 5 : NPC.frameCounter < 102.0 ? num166 - 4 : NPC.frameCounter < 108.0 ? num166 - 5 : !(NPC.frameCounter < 114.0) ? NPC.frameCounter < 120.0 ? num166 - 5 : NPC.frameCounter < 150.0 ? num166 - 4 : NPC.frameCounter < 156.0 ? num166 - 5 : !(NPC.frameCounter < 162.0) ? NPC.frameCounter < 168.0 ? num166 - 5 : NPC.frameCounter < 198.0 ? num166 - 4 : NPC.frameCounter < 204.0 ? num166 - 5 : !(NPC.frameCounter < 210.0) ? NPC.frameCounter < 216.0 ? num166 - 5 : NPC.frameCounter < 246.0 ? num166 - 4 : NPC.frameCounter < 252.0 ? num166 - 5 : !(NPC.frameCounter < 258.0) ? NPC.frameCounter < 264.0 ? num166 - 5 : NPC.frameCounter < 294.0 ? num166 - 4 : NPC.frameCounter < 300.0 ? num166 - 5 : 0 : 0 : 0 : 0 : 0 : 0 : 0;
                    if (num176 == num166 - 4 && num175 == num166 - 5)
                    {
                        Vector2 vector4 = NPC.Center + new Vector2(10 * NPC.direction, -4f);
                        for (int n = 0; n < 8; n++)
                        {
                            int num177 = Main.rand.Next(139, 143);
                            int num178 = Dust.NewDust(vector4, 0, 0, num177, NPC.velocity.X + NPC.direction, NPC.velocity.Y - 2.5f, 0, default, 1.2f);
                            Main.dust[num178].velocity.X += NPC.direction * 1.5f;
                            Main.dust[num178].position -= new Vector2(4f);
                            Main.dust[num178].velocity *= 2f;
                            Main.dust[num178].scale = 0.7f + Main.rand.NextFloat() * 0.3f;
                        }
                    }
                    NPC.frame.Y = frameHeight * num176;
                    if (NPC.frameCounter >= 300.0)
                    {
                        NPC.frameCounter = 0.0;
                    }
                }
                else if (NPC.ai[0] == 7f || NPC.ai[0] == 19f)
                {
                    NPC.frameCounter += 1.0;
                    int num179 = NPC.frame.Y / frameHeight;
                    int num83 = num166 - num179;
                    if ((uint)(num83 - 1) > 1u && (uint)(num83 - 4) > 1u && num179 != 0)
                    {
                        NPC.frame.Y = 0;
                        NPC.frameCounter = 0.0;
                    }
                    int num181 = 0;
                    if (NPC.frameCounter < 16.0)
                    {
                        num181 = 0;
                    }
                    else if (NPC.frameCounter == 16.0)
                    {
                        EmoteBubble.NewBubbleNPC(new WorldUIAnchor(NPC), 112);
                    }
                    else if (NPC.frameCounter < 128.0)
                    {
                        num181 = NPC.frameCounter % 16.0 < 8.0 ? num166 - 2 : 0;
                    }
                    else if (NPC.frameCounter < 160.0)
                    {
                        num181 = 0;
                    }
                    else if (NPC.frameCounter != 160.0)
                    {
                        num181 = NPC.frameCounter < 220.0 ? NPC.frameCounter % 12.0 < 6.0 ? num166 - 2 : 0 : 0;
                    }
                    else
                    {
                        EmoteBubble.NewBubbleNPC(new WorldUIAnchor(NPC), 60);
                    }
                    NPC.frame.Y = frameHeight * num181;
                    if (NPC.frameCounter >= 220.0)
                    {
                        NPC.frameCounter = 0.0;
                    }
                }
                else if (NPC.ai[0] == 9f)
                {
                    NPC.frameCounter += 1.0;
                    int num182 = NPC.frame.Y / frameHeight;
                    int num82 = num166 - num182;
                    if ((uint)(num82 - 1) > 1u && (uint)(num82 - 4) > 1u && num182 != 0)
                    {
                        NPC.frame.Y = 0;
                        NPC.frameCounter = 0.0;
                    }
                    int num183 = 0;
                    num183 = !(NPC.frameCounter < 10.0) ? !(NPC.frameCounter < 16.0) ? num166 - 4 : num166 - 5 : 0;
                    if (NPC.ai[1] < 16f)
                    {
                        num183 = num166 - 5;
                    }
                    if (NPC.ai[1] < 10f)
                    {
                        num183 = 0;
                    }
                    NPC.frame.Y = frameHeight * num183;
                }
                else if (NPC.ai[0] == 18f)
                {
                    NPC.frameCounter += 1.0;
                    int num184 = NPC.frame.Y / frameHeight;
                    int num81 = num166 - num184;
                    if ((uint)(num81 - 1) > 1u && (uint)(num81 - 4) > 1u && num184 != 0)
                    {
                        NPC.frame.Y = 0;
                        NPC.frameCounter = 0.0;
                    }
                    int num185 = 0;
                    if (NPC.frameCounter < 10.0)
                    {
                        num185 = 0;
                    }
                    else if (NPC.frameCounter < 16.0)
                    {
                        num185 = num166 - 1;
                    }
                    else
                    {
                        num185 = num166 - 2;
                    }
                    if (NPC.ai[1] < 16f)
                    {
                        num185 = num166 - 1;
                    }
                    if (NPC.ai[1] < 10f)
                    {
                        num185 = 0;
                    }
                    num185 = Main.npcFrameCount[type] - 2;
                    NPC.frame.Y = frameHeight * num185;
                }
                else if (NPC.ai[0] == 10f || NPC.ai[0] == 13f)
                {
                    NPC.frameCounter += 1.0;
                    int num186 = NPC.frame.Y / frameHeight;
                    if ((uint)(num186 - num166) > 3u && num186 != 0)
                    {
                        NPC.frame.Y = 0;
                        NPC.frameCounter = 0.0;
                    }
                    int num187 = 10;
                    int num188 = 6;
                    int num189 = 0;
                    num189 = !(NPC.frameCounter < num187) ? NPC.frameCounter < num187 + num188 ? num166 : NPC.frameCounter < num187 + num188 * 2 ? num166 + 1 : NPC.frameCounter < num187 + num188 * 3 ? num166 + 2 : NPC.frameCounter < num187 + num188 * 4 ? num166 + 3 : 0 : 0;
                    NPC.frame.Y = frameHeight * num189;
                }
                else if (NPC.ai[0] == 15f)
                {
                    NPC.frameCounter += 1.0;
                    int num190 = NPC.frame.Y / frameHeight;
                    if ((uint)(num190 - num166) > 3u && num190 != 0)
                    {
                        NPC.frame.Y = 0;
                        NPC.frameCounter = 0.0;
                    }
                    float num192 = NPC.ai[1] / NPCID.Sets.AttackTime[type];
                    int num193 = 0;
                    num193 = num192 > 0.65f ? num166 : num192 > 0.5f ? num166 + 1 : num192 > 0.35f ? num166 + 2 : num192 > 0f ? num166 + 3 : 0;
                    NPC.frame.Y = frameHeight * num193;
                }
                else if (NPC.ai[0] == 25f)
                {
                    NPC.frame.Y = frameHeight;
                }
                else if (NPC.ai[0] == 12f)
                {
                    NPC.frameCounter += 1.0;
                    int num194 = NPC.frame.Y / frameHeight;
                    if ((uint)(num194 - num166) > 4u && num194 != 0)
                    {
                        NPC.frame.Y = 0;
                        NPC.frameCounter = 0.0;
                    }
                    int num195 = num166 + NPC.GetShootingFrame(NPC.ai[2]);
                    NPC.frame.Y = frameHeight * num195;
                }
                else if (NPC.ai[0] == 14f || NPC.ai[0] == 24f)
                {
                    NPC.frameCounter += 1.0;
                    int num196 = NPC.frame.Y / frameHeight;
                    if ((uint)(num196 - num166) > 1u && num196 != 0)
                    {
                        NPC.frame.Y = 0;
                        NPC.frameCounter = 0.0;
                    }
                    int num197 = 12;
                    int num198 = NPC.frameCounter % num197 * 2.0 < num197 ? num166 : num166 + 1;
                    NPC.frame.Y = frameHeight * num198;
                    if (NPC.ai[0] == 24f)
                    {
                        if (NPC.frameCounter == 60.0)
                        {
                            EmoteBubble.NewBubble(87, new WorldUIAnchor(NPC), 60);
                        }
                        if (NPC.frameCounter == 150.0)
                        {
                            EmoteBubble.NewBubble(3, new WorldUIAnchor(NPC), 90);
                        }
                        if (NPC.frameCounter >= 240.0)
                        {
                            NPC.frame.Y = 0;
                        }
                    }
                }
                else if (NPC.ai[0] == 1001f)
                {
                    NPC.frame.Y = frameHeight * (num166 - 1);
                    NPC.frameCounter = 0.0;
                }
                else if (NPC.CanTalk && (NPC.ai[0] == 3f || NPC.ai[0] == 4f))
                {
                    NPC.frameCounter += 1.0;
                    int num199 = NPC.frame.Y / frameHeight;
                    int num80 = num166 - num199;
                    if ((uint)(num80 - 1) > 1u && (uint)(num80 - 4) > 1u && num199 != 0)
                    {
                        NPC.frame.Y = 0;
                        NPC.frameCounter = 0.0;
                    }
                    bool flag4 = NPC.ai[0] == 3f;
                    int num200 = 0;
                    int num201 = 0;
                    int num203 = -1;
                    int num204 = -1;
                    if (NPC.frameCounter < 10.0)
                    {
                        num200 = 0;
                    }
                    else if (NPC.frameCounter < 16.0)
                    {
                        num200 = num166 - 5;
                    }
                    else if (NPC.frameCounter < 46.0)
                    {
                        num200 = num166 - 4;
                    }
                    else if (NPC.frameCounter < 60.0)
                    {
                        num200 = num166 - 5;
                    }
                    else if (NPC.frameCounter < 216.0)
                    {
                        num200 = 0;
                    }
                    else if (NPC.frameCounter == 216.0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        num203 = 70;
                    }
                    else if (NPC.frameCounter < 286.0)
                    {
                        num200 = NPC.frameCounter % 12.0 < 6.0 ? num166 - 2 : 0;
                    }
                    else if (NPC.frameCounter < 320.0)
                    {
                        num200 = 0;
                    }
                    else if (NPC.frameCounter != 320.0 || Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        num200 = NPC.frameCounter < 420.0 ? NPC.frameCounter % 16.0 < 8.0 ? num166 - 2 : 0 : 0;
                    }
                    else
                    {
                        num203 = 100;
                    }
                    if (NPC.frameCounter < 70.0)
                    {
                        num201 = 0;
                    }
                    else if (NPC.frameCounter != 70.0 || Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        num201 = !(NPC.frameCounter < 160.0) ? NPC.frameCounter < 166.0 ? num166 - 5 : NPC.frameCounter < 186.0 ? num166 - 4 : NPC.frameCounter < 200.0 ? num166 - 5 : !(NPC.frameCounter < 320.0) ? NPC.frameCounter < 326.0 ? num166 - 1 : 0 : 0 : NPC.frameCounter % 16.0 < 8.0 ? num166 - 2 : 0;
                    }
                    else
                    {
                        num204 = 90;
                    }
                    if (flag4)
                    {
                        NPC nPC = Main.npc[(int)NPC.ai[2]];
                        if (num203 != -1)
                        {
                            EmoteBubble.NewBubbleNPC(new WorldUIAnchor(NPC), num203, new WorldUIAnchor(nPC));
                        }
                        if (num204 != -1 && nPC.CanTalk)
                        {
                            EmoteBubble.NewBubbleNPC(new WorldUIAnchor(nPC), num204, new WorldUIAnchor(NPC));
                        }
                    }
                    NPC.frame.Y = frameHeight * (flag4 ? num200 : num201);
                    if (NPC.frameCounter >= 420.0)
                    {
                        NPC.frameCounter = 0.0;
                    }
                }
                else if (NPC.CanTalk && (NPC.ai[0] == 16f || NPC.ai[0] == 17f))
                {
                    NPC.frameCounter += 1.0;
                    int num205 = NPC.frame.Y / frameHeight;
                    int num79 = num166 - num205;
                    if ((uint)(num79 - 1) > 1u && (uint)(num79 - 4) > 1u && num205 != 0)
                    {
                        NPC.frame.Y = 0;
                        NPC.frameCounter = 0.0;
                    }
                    bool flag5 = NPC.ai[0] == 16f;
                    int num206 = 0;
                    int num207 = -1;
                    if (NPC.frameCounter < 10.0)
                    {
                        num206 = 0;
                    }
                    else if (NPC.frameCounter < 16.0)
                    {
                        num206 = num166 - 5;
                    }
                    else if (NPC.frameCounter < 22.0)
                    {
                        num206 = num166 - 4;
                    }
                    else if (NPC.frameCounter < 28.0)
                    {
                        num206 = num166 - 5;
                    }
                    else if (NPC.frameCounter < 34.0)
                    {
                        num206 = num166 - 4;
                    }
                    else if (NPC.frameCounter < 40.0)
                    {
                        num206 = num166 - 5;
                    }
                    else if (NPC.frameCounter == 40.0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        num207 = 45;
                    }
                    else if (NPC.frameCounter < 70.0)
                    {
                        num206 = num166 - 4;
                    }
                    else if (NPC.frameCounter < 76.0)
                    {
                        num206 = num166 - 5;
                    }
                    else if (NPC.frameCounter < 82.0)
                    {
                        num206 = num166 - 4;
                    }
                    else if (NPC.frameCounter < 88.0)
                    {
                        num206 = num166 - 5;
                    }
                    else if (NPC.frameCounter < 94.0)
                    {
                        num206 = num166 - 4;
                    }
                    else if (NPC.frameCounter < 100.0)
                    {
                        num206 = num166 - 5;
                    }
                    else if (NPC.frameCounter == 100.0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        num207 = 45;
                    }
                    else if (NPC.frameCounter < 130.0)
                    {
                        num206 = num166 - 4;
                    }
                    else if (NPC.frameCounter < 136.0)
                    {
                        num206 = num166 - 5;
                    }
                    else if (NPC.frameCounter < 142.0)
                    {
                        num206 = num166 - 4;
                    }
                    else if (NPC.frameCounter < 148.0)
                    {
                        num206 = num166 - 5;
                    }
                    else if (NPC.frameCounter < 154.0)
                    {
                        num206 = num166 - 4;
                    }
                    else if (NPC.frameCounter < 160.0)
                    {
                        num206 = num166 - 5;
                    }
                    else if (NPC.frameCounter != 160.0 || Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        num206 = NPC.frameCounter < 220.0 ? num166 - 4 : NPC.frameCounter < 226.0 ? num166 - 5 : 0;
                    }
                    else
                    {
                        num207 = 75;
                    }
                    if (flag5 && num207 != -1)
                    {
                        int num208 = (int)NPC.localAI[2];
                        int num209 = (int)NPC.localAI[3];
                        int num210 = (int)Main.npc[(int)NPC.ai[2]].localAI[3];
                        int num211 = (int)Main.npc[(int)NPC.ai[2]].localAI[2];
                        int num212 = 3 - num208 - num209;
                        int num214 = 0;
                        if (NPC.frameCounter == 40.0)
                        {
                            num214 = 1;
                        }
                        if (NPC.frameCounter == 100.0)
                        {
                            num214 = 2;
                        }
                        if (NPC.frameCounter == 160.0)
                        {
                            num214 = 3;
                        }
                        int num215 = 3 - num214;
                        int num216 = -1;
                        int num217 = 0;
                        while (num216 < 0)
                        {
                            num79 = num217 + 1;
                            num217 = num79;
                            if (num79 >= 100)
                            {
                                break;
                            }
                            num216 = Main.rand.Next(2);
                            if (num216 == 0 && num211 >= num209)
                            {
                                num216 = -1;
                            }
                            if (num216 == 1 && num210 >= num208)
                            {
                                num216 = -1;
                            }
                            if (num216 == -1 && num215 <= num212)
                            {
                                num216 = 2;
                            }
                        }
                        if (num216 == 0)
                        {
                            Main.npc[(int)NPC.ai[2]].localAI[3] += 1f;
                            num210++;
                        }
                        if (num216 == 1)
                        {
                            Main.npc[(int)NPC.ai[2]].localAI[2] += 1f;
                            num211++;
                        }
                        int num218 = Utils.SelectRandom(Main.rand, 38, 37, 36);
                        int num219 = num218;
                        switch (num216)
                        {
                            case 0:
                                switch (num218)
                                {
                                    case 38:
                                        num219 = 37;
                                        break;
                                    case 37:
                                        num219 = 36;
                                        break;
                                    case 36:
                                        num219 = 38;
                                        break;
                                }
                                break;
                            case 1:
                                switch (num218)
                                {
                                    case 38:
                                        num219 = 36;
                                        break;
                                    case 37:
                                        num219 = 38;
                                        break;
                                    case 36:
                                        num219 = 37;
                                        break;
                                }
                                break;
                        }
                        if (num215 == 0)
                        {
                            if (num210 >= 2)
                            {
                                num218 -= 3;
                            }
                            if (num211 >= 2)
                            {
                                num219 -= 3;
                            }
                        }
                        EmoteBubble.NewBubble(num218, new WorldUIAnchor(NPC), num207);
                        EmoteBubble.NewBubble(num219, new WorldUIAnchor(Main.npc[(int)NPC.ai[2]]), num207);
                    }
                    NPC.frame.Y = frameHeight * (flag5 ? num206 : num206);
                    if (NPC.frameCounter >= 420.0)
                    {
                        NPC.frameCounter = 0.0;
                    }
                }
                else if (NPC.velocity.X == 0f)
                {
                    NPC.frame.Y = 0;
                    NPC.frameCounter = 0.0;
                }
                else
                {
                    int num221 = 6;
                    NPC.frameCounter += Math.Abs(NPC.velocity.X) * 2f;
                    NPC.frameCounter += 1.0;
                    int num222 = frameHeight * 2;
                    if (NPC.frame.Y < num222)
                    {
                        NPC.frame.Y = num222;
                    }
                    if (NPC.frameCounter > num221)
                    {
                        NPC.frame.Y += frameHeight;
                        NPC.frameCounter = 0.0;
                    }
                    if (NPC.frame.Y / frameHeight >= Main.npcFrameCount[type] - num165)
                    {
                        NPC.frame.Y = num222;
                    }
                }
            }
            else
            {
                NPC.frameCounter = 0.0;
                NPC.frame.Y = frameHeight;
            }

            NPC.position -= NPC.netOffset;
        }

        public void TPDust()
        {
            Vector2 position = NPC.Center + Vector2.One * -20f;
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, DustID.Granite, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
            }
            for (int num87 = 0; num87 < 15; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 50, default, 3.7f);
                Main.dust[num88].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + Main.rand.NextFloat() * 4f);
                num88 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 25, default, 1.5f);
                Main.dust[num88].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 10; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = NPC.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f;
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = NPC.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f;
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += NPC.DirectionTo(Main.dust[num92].position) * 3f;
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            bool fAnubisTime = NPC.downedMoonlord && !NPCExtensions.BeenKilled<ForsakenAnubis>();
            bool hasGreedBook = !Main.LocalPlayer.GetModPlayer<AAPlayer>().AnubisBook && Main.LocalPlayer.FindItem(ModContent.ItemType<TheLifeAndEpicAdventuresOfAnubisTheWonderDog>()) >= 0;
            if (!fAnubisTime && hasGreedBook)
                button = "Found your book";
            else
                button = "Help";

            if(!NPC.downedMoonlord || NPCExtensions.BeenKilled<ForsakenAnubis>())
                button2 = "What's next?";
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                Player player = Main.LocalPlayer;

                if (!NPCExtensions.BeenKilled<Anubis>() && player.GetModPlayer<AAPlayer>().GivenAnuSummon && !BasePlayer.HasItem(player, ModContent.ItemType<_Content.Desert.__Hardmode.Items._BossAnubis.RasScepter>()))
                {
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<_Content.Desert.__Hardmode.Items._BossAnubis.RasScepter>(), 1);
                    Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisScapterLost");
                    return;
                }

                if (NPC.downedMoonlord && !NPCExtensions.BeenKilled<ForsakenAnubis>())
                {
                    Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.UnofficialInterim.Help");
                    return;
                }

                if (!player.GetModPlayer<AAPlayer>().AnubisBook && NPCExtensions.BeenKilled<Greed>())
                {
                    int Item = player.FindItem(ModContent.ItemType<TheLifeAndEpicAdventuresOfAnubisTheWonderDog>());
                    if (Item >= 0)
                    {
                        player.inventory[Item].stack--;
                        if (player.inventory[Item].stack <= 0)
                        {
                            player.inventory[Item] = new Item();
                        }

                        Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.GetBookChat");
                        player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<TheLifeAndEpicAdventuresOfAnubisTheWonderDogSpecialEdition>(), 1);
                        player.GetModPlayer<AAPlayer>().AnubisBook = true;
                        SoundEngine.PlaySound(SoundID.Chat);
                        return;
                    }
                }

                Main.npcChatText = Legendscribe.GuideChat();
            }
            else
            {
                if (Main.LocalPlayer.GetModPlayer<AAPlayer>().AnubisBook)
                    QuestSystem.Questlines["LegendscribeEarlyGame"].Quests["Greed"].DescriptionComplete = Language.GetOrRegister("Mods.AAModClassic.UI.Quests.LegendscribeQuestline.Greed.Description.FoundBook");
                else
                    QuestSystem.Questlines["LegendscribeEarlyGame"].Quests["Greed"].DescriptionComplete = Language.GetOrRegister("Mods.AAModClassic.UI.Quests.LegendscribeQuestline.Greed.Description.Complete");
                LegendscribeQuestUISystem.OpenLegendscribeUI(NPC.whoAmI);
            }
        }

        public override string GetChat()
        {
            AnubisDialoguePlayer p = Main.LocalPlayer.GetModPlayer<AnubisDialoguePlayer>();

            if (NPC.downedMoonlord && !NPCExtensions.BeenKilled<ForsakenAnubis>())
            {
                if (!p.HasLostToForsakenAnubis)
                {
                    if (!p.HasSpokenToAnubisPostMoonLord)
                    {
                        p.HasSpokenToAnubisPostMoonLord = true;
                        return Language.GetOrRegister("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedAnubisFAnubisN").Format(Main.LocalPlayer.name);
                    }
                    else
                        return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.UnofficialInterim.PreFight.Repeat");
                }
                else
                {
                    if (!p.HasSpokenToAnubisAfterDyingToForsakenAnubis)
                    {
                        p.HasSpokenToAnubisAfterDyingToForsakenAnubis = true;
                        return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.UnofficialInterim.PostLose.First");
                    }
                    else if(p.HasLostMultipleTimesToForsakenAnubis)
                        return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.UnofficialInterim.PostLose.Repeat.MultipleDeaths." + Main.rand.Next(3));
                    else
                        return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.UnofficialInterim.PostLose.Repreat.FirstDeath" + Main.rand.Next(2));
                }
            }
            else if (!p.HasSpokenToAnubisPostForsakenAnubis && NPCExtensions.BeenKilled<ForsakenAnubis>())
            {
                p.HasSpokenToAnubisPostForsakenAnubis = true;
                return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.UnofficialInterim.PostVictory");
            }

            return Legendscribe.LegendscribeDialogue(NPC);
        }

        public static int FindFemaleNPC()
        {
            int FemaleNPC = Main.rand.Next(6);
            switch (FemaleNPC)
            {
                case 0:
                    FemaleNPC = NPCID.Nurse;
                    break;
                case 1:
                    FemaleNPC = NPCID.Dryad;
                    break;
                case 2:
                    FemaleNPC = NPCID.Stylist;
                    break;
                case 3:
                    FemaleNPC = NPCID.Mechanic;
                    break;
                case 4:
                    FemaleNPC = NPCID.Steampunker;
                    break;
                default:
                    FemaleNPC = NPCID.PartyGirl;
                    break;
            }
            return FemaleNPC;
        }

        #region attack values
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 30;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 20;
			randExtraCooldown = 20;
		}

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<JudgementNPC>();
            attackDelay = 5;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {

            multiplier = 4f;

            randomOffset = 2f;

        }
        #endregion

    }

    public class AnubisDialoguePlayer : ModPlayer
    {
        internal bool HasSpokenToAnubisPostMoonLord = false;
        internal bool HasLostToForsakenAnubis = false;
        internal bool HasLostMultipleTimesToForsakenAnubis = false;
        internal bool HasSpokenToAnubisAfterDyingToForsakenAnubis = false;
        internal bool HasSpokenToAnubisPostForsakenAnubis = false;

        public override void SaveData(TagCompound tag)
        {
            tag.Add("HasSpokenToAnubisPostMoonLord", HasSpokenToAnubisPostMoonLord);
            tag.Add("HasDiedToForsakenAnubis", HasLostToForsakenAnubis);
            tag.Add("HasDiedMultipleTimesToForsakenAnubis", HasLostMultipleTimesToForsakenAnubis);
            tag.Add("HasSpokenToAnubisAfterDyingToForsakenAnubis", HasSpokenToAnubisAfterDyingToForsakenAnubis);
        }

        public override void LoadData(TagCompound tag)
        {
            if (!tag.TryGet("HasSpokenToAnubisPostMoonLord", out HasSpokenToAnubisPostMoonLord))
                HasSpokenToAnubisPostMoonLord = false;
            if (!tag.TryGet("HasDiedToForsakenAnubis", out HasLostToForsakenAnubis))
                HasLostToForsakenAnubis = false;
            if (!tag.TryGet("HasDiedMultipleTimesToForsakenAnubis", out HasLostMultipleTimesToForsakenAnubis))
                HasLostMultipleTimesToForsakenAnubis = false;
            if (!tag.TryGet("HasSpokenToAnubisAfterDyingToForsakenAnubis", out HasSpokenToAnubisAfterDyingToForsakenAnubis))
                HasSpokenToAnubisAfterDyingToForsakenAnubis = false;
        }
    }
}