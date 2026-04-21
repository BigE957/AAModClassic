
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using AAModClassic.NPCs.Bosses.Anubis.Forsaken;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.CrossMod;
using Terraria.Localization;
using AAModClassic.NPCs.Bosses.Rajah;
using AAModClassic.Utilities;
using AAModClassic.NPCs.Bosses.Athena.Olympian;
using AAModClassic.NPCs.Bosses.Greed;
using AAModClassic.NPCs.Bosses.Djinn;
using AAModClassic.NPCs.Bosses.Athena;
using AAModClassic.NPCs.Bosses.Anubis;
using AAModClassic.NPCs.Bosses.Serpent;
using AAModClassic.NPCs.Bosses.Broodmother;
using AAModClassic.NPCs.Bosses.FeudalFungus;
using AAModClassic.NPCs.Bosses.MushroomMonarch;
using AAModClassic.___Content.Mire.___PreHardmode.NPCs.__BossHydra;
using AAModClassic.___Content.Stars._PostMoonlord.Items.Quest;
using AAModClassic.___Content.Desert.__Hardmode.Items.Weapons;
using AAModClassic.___Content.Desert.__Hardmode.Items._BossAnubis.BossStandard;
using AAModClassic.___Content.Desert.__Hardmode.Items.Quest;

namespace AAModClassic.NPCs.TownNPCs
{
    [AutoloadHead]
	public class Legendscribe : ModNPC
	{
        public override string Texture => "AAModClassic/NPCs/TownNPCs/Legendscribe";
        public override void ModifyTypeName(ref string typeName)
        {
            typeName = "Legendscribe";
        }

		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 26;
            NPC.dontTakeDamageFromHostiles = true;
			NPCID.Sets.ExtraFramesCount[NPC.type] = 10;
			NPCID.Sets.AttackFrameCount[NPC.type] = 5;
			NPCID.Sets.DangerDetectRange[NPC.type] = 700;
			NPCID.Sets.AttackType[NPC.type] = 0;
			NPCID.Sets.AttackTime[NPC.type] = 40;
			NPCID.Sets.AttackAverageChance[NPC.type] = 20;
			NPCID.Sets.HatOffsetY[NPC.type] = 3;
		}

        public float internalAI = 0;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI = reader.ReadSingle();
            }
        }

        public override void SetDefaults()
		{
			NPC.townNPC = true;
			NPC.friendly = true;
			NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = NPCAIStyleID.Passive;
			NPC.damage = 10;
			NPC.defense = 68;
			NPC.lifeMax = 160000;
            NPC.HitSound = SoundID.NPCHit23;
            NPC.DeathSound = SoundID.NPCDeath39;
            NPC.knockBackResist = 0f;
			AnimationType = NPCID.Guide;
            NPC.lavaImmune = true;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

		public override void HitEffect(NPC.HitInfo hit)
		{
		}

		public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (player.active && !NPC.AnyNPCs(ModContent.NPCType<Bosses.Anubis.Anubis>()) && 
                    !NPC.AnyNPCs(ModContent.NPCType<FATransition>()) &&
                    !NPC.AnyNPCs(ModContent.NPCType<FATransition2>()) &&
                    !NPC.AnyNPCs(ModContent.NPCType<ForsakenAnubis>()))
                {
                    return true;
                }
            }
            return false;
		}

		public override List<string> SetNPCNameList()/* tModPorter Suggestion: Return a list of names */
		{
            return ["Anubis"];
        }

        public override void PostAI()
        {
        }

        public static bool SwitchInfo = false;
        public static bool DoNext = false;
        public static bool Mushroom = false;
        public static bool Glowshroom = false;
        public static bool Grips = false;
        public static bool Brood = false;
        public static bool Hydra = false;
        public static bool Djinn = false;
        public static bool Serpent = false;
        public static bool AnubisB = false;
        public static bool Athena = false;
        public static bool Greed = false;
        public static bool Rajah = false;
        public static bool AnubisF = false;
        public static bool AthenaA = false;
        public static bool GreedA = false;
        public static bool Equinox = false;
        public static bool Sisters = false;
        public static bool Akuma = false;
        public static bool Yamata = false;
        public static bool Zero = false;
        public static bool Shen = false;
        public static bool RajahC = false;
        public static bool BaseChat = false;
        public static int ChatNumber = 0;

        public override void ResetEffects()
        {
            SwitchInfo = false;
            DoNext = false;
            Mushroom = false;
            Glowshroom = false;
            Grips = false;
            Brood = false;
            Hydra = false;
            Djinn = false;
            Serpent = false;
            AnubisB = false;
            Athena = false;
            Greed = false;
            Rajah = false;
            AnubisF = false;
            AthenaA = false;
            GreedA = false;
            Equinox = false;
            Sisters = false;
            Akuma = false;
            Yamata = false;
            Zero = false;
            Shen = false;
            RajahC = false;
        }
        
        public override void SetChatButtons(ref string button, ref string button2)
        {
			string SwitchInfoT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons1");

            string DoNextT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons2");

            string MushT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons3");

            string GlowT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons4");

            string GripT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons5");

            string BroodT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons6");

            string HydraT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons7");

            string DjinnT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons8");

            string SerpentT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons9");

            string AnubisT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons14");

            string AthenaT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons21");

            string GreedT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons22");

            string RajahT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons23");

            string AnubisFT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons27");

            string AthenaAT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons24");

            string GreedAT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons25");

            string EquinoxT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons13");

            string SistersT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons15");

            string AkumaT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons16");

            string YamataT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons17");

            string ZeroT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons18");

            string ShenT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons19");

            string RajahCT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons26");
            
            button = SwitchInfoT;

            if (ChatNumber == 0)
			{
			    button2 = DoNextT;
                DoNext = true;
            }
            else if (ChatNumber == 1)
            {
                button2 = MushT;
                Mushroom = true;
            }
            else if (ChatNumber == 2)
            {
                button2 = GlowT;
                Glowshroom = true;
            }
            else if (ChatNumber == 3)
            {
                button2 = GripT;
                Grips = true;
            }
            else if (ChatNumber == 4)
            {
                button2 = BroodT;
                Brood = true;
            }
            else if (ChatNumber == 5)
            {
                button2 = HydraT;
                Hydra = true;
            }
            else if (ChatNumber == 6 && NPC.downedBoss3)
            {
                button2 = DjinnT;
                Djinn = true;
            }
            else if (ChatNumber == 7 && NPC.downedBoss3)
            {
                button2 = SerpentT;
                Serpent = true;
            }
            else if (ChatNumber == 8 && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {
                button2 = AnubisT;
                AnubisB = true;
            }
            else if (ChatNumber == 9 && NPCExtensions.BeenKilled<Anubis>())
            {
                button2 = AthenaT;
                Athena = true;
            }
            else if (ChatNumber == 10 && NPCExtensions.BeenKilled<Anubis>())
            {
                button2 = GreedT;
                Greed = true;
            }
            else if (ChatNumber == 11 && Main.hardMode)
            {
                button2 = RajahT;
                Rajah = true;
            }
            else if (ChatNumber == 12 && NPC.downedMoonlord && NPCExtensions.BeenKilled<Anubis>())
            {
                button2 = AnubisFT;
                AnubisF = true;
            }
            else if (ChatNumber == 13 && NPC.downedMoonlord && NPCExtensions.BeenKilled<ForsakenAnubis>() && NPCExtensions.BeenKilled<Athena>())
            {
                button2 = AthenaAT;
                AthenaA = true;
            }
            else if (ChatNumber == 14 && NPC.downedMoonlord && NPCExtensions.BeenKilled<ForsakenAnubis>() && NPCExtensions.BeenKilled<Greed>())
            {
                button2 = GreedAT;
                GreedA = true;
            }
            else if (ChatNumber == 15 && NPCExtensions.BeenKilled<GreedA>() && NPCExtensions.BeenKilled<AthenaA>())
            {
                button2 = EquinoxT;
                Equinox = true;
            }
            else if (ChatNumber == 16 && NPC.downedMoonlord && AAWorld.downedEquinox)
            {
                button2 = SistersT;
                Sisters = true;
            }
            else if (ChatNumber == 17 && NPC.downedMoonlord && AAWorld.downedSisters)
            {
                button2 = AkumaT;
                Akuma = true;
            }
            else if (ChatNumber == 18 && NPC.downedMoonlord && AAWorld.downedSisters)
            {
                button2 = YamataT;
                Yamata = true;
            }
            else if (ChatNumber == 19 && NPC.downedMoonlord && AAWorld.downedNC)
            {
                button2 = ZeroT;
                Zero = true;
            }
            else if (ChatNumber == 20 && NPCExtensions.BeenKilled<SupremeRajah>())
            {
                button2 = RajahCT;
                RajahC = true;
            }
            else if (ChatNumber == 21 && AAWorld.downedAllAncients)
            {
                button2 = ShenT;
                Shen = true;
            }
            else
            {
                ChatNumber = 0;
                button2 = DoNextT;
                DoNext = true;
            }
        }

        public static void ResetBools()
        {
            DoNext = false;
            Mushroom = false;
            Glowshroom = false;
            Grips = false;
            Brood = false;
            Hydra = false;
            Djinn = false;
            Serpent = false;
            AnubisB = false;
            Athena = false;
            Greed = false;
            Rajah = false;
            AnubisF = false;
            AthenaA = false;
            GreedA = false;
            Equinox = false;
            Sisters = false;
            Akuma = false;
            Yamata = false;
            Zero = false;
            Shen = false;
            RajahC = false;
        }

		public override void OnChatButtonClicked(bool firstButton, ref string shopName)
		{
			if (firstButton)
			{
                ResetBools();
				ChatNumber += 1;
				if (ChatNumber > 21)
				{
					ChatNumber = 0;
				}
			}
			else
            {
                Player player = Main.LocalPlayer;
                int Item = player.FindItem(ModContent.ItemType<TheLifeAndEpicAdventuresOfAnubisTheWonderDog>());
                if (Item >= 0 && !player.GetModPlayer<AAPlayer>().AnubisBook && Greed)
                {
                    player.inventory[Item].stack--;
                    if (player.inventory[Item].stack <= 0)
                    {
                        player.inventory[Item] = new Item();
                    }

                    Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.GetBookChat");
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<TheLifeAndEpicAdventuresOfAnubisTheWonderDogSpecialEdition>(), 1);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                Main.npcChatText = BossChat();
			}
		}

        public override bool PreAI()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Bosses.Anubis.Anubis>()) ||
                NPC.AnyNPCs(ModContent.NPCType<FATransition>()) ||
                NPC.AnyNPCs(ModContent.NPCType<FATransition2>()) ||
                NPC.AnyNPCs(ModContent.NPCType<ForsakenAnubis>()))
            {
                TPDust();
                NPC.active = false;
            }
            if (Vector2.Distance(NPC.position, new Vector2(NPC.homeTileX, NPC.homeTileY)) > 3000 && internalAI < 240 && !NPC.homeless)
            {
                internalAI++;
                if (internalAI >= 240)
                {
                    bool flag4 = true;
                    int num3 = NPC.homeTileY;
                    for (int k = 0; k < 2; k++)
                    {
                        Rectangle rectangle = new Rectangle((int)(NPC.position.X + NPC.width / 2 - NPC.sWidth / 2 - NPC.safeRangeX), (int)(NPC.position.Y + NPC.height / 2 - NPC.sHeight / 2 - NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
                        if (k == 1)
                        {
                            rectangle = new Rectangle(NPC.homeTileX * 16 + 8 - NPC.sWidth / 2 - NPC.safeRangeX, num3 * 16 + 8 - NPC.sHeight / 2 - NPC.safeRangeY, NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
                        }
                        for (int l = 0; l < 255; l++)
                        {
                            if (Main.player[l].active)
                            {
                                Rectangle rectangle2 = new Rectangle((int)Main.player[l].position.X, (int)Main.player[l].position.Y, Main.player[l].width, Main.player[l].height);
                                if (rectangle2.Intersects(rectangle))
                                {
                                    flag4 = false;
                                    break;
                                }
                            }
                            if (!flag4)
                            {
                                break;
                            }
                        }
                    }
                    if (flag4)
                    {
                        if (!Collision.SolidTiles(NPC.homeTileX - 1, NPC.homeTileX + 1, num3 - 3, num3 - 1))
                        {
                            TPDust();
                            CombatText.NewText(NPC.Hitbox, Color.Gold, Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.CombatTextChat"));
                            NPC.velocity.X = 0f;
                            NPC.velocity.Y = 0f;
                            NPC.position.X = NPC.homeTileX * 16 + 8 - NPC.width / 2;
                            NPC.position.Y = num3 * 16 - NPC.height - 0.1f;
                            NPC.netUpdate = true;
                            internalAI = 0;
                        }
                    }
                }
            }
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D t = Mod.GetTexture(NPCExtensions.BeenKilled<ForsakenAnubis>() ? "NPCs/TownNPCs/LegendscribeF" : "NPCs/TownNPCs/Legendscribe");
            Texture2D g = Mod.GetTexture(NPCExtensions.BeenKilled<ForsakenAnubis>() ? "Glowmasks/LegendscribeF_Glow" : "Glowmasks/Legendscribe_Glow");
            BaseDrawing.DrawTexture(spriteBatch, t, 0, NPC, drawColor);
            BaseDrawing.DrawTexture(spriteBatch, g, 0, NPC, Color.White);
            return false;
        }

        public void TPDust()
        {
            Vector2 position = NPC.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, DustID.Granite, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 15; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 50, default, 3.7f);
                Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 25, default, 1.5f);
                Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
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
                Main.dust[num90].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += NPC.DirectionTo(Main.dust[num92].position) * 3f;
            }
        }

        public static bool DoG => (bool)ModSupport.GetModWorldConditions("CalamityMod", "CalamityWorld", "downedDoG", false, true);

        public string BossChat()
        {
            Player player = Main.LocalPlayer;
            if (Mushroom)
            {
                return NPCExtensions.BeenKilled<MushroomMonarch>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedMonarchY") : 
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedMonarchN");
            }
            else if (Glowshroom)
            {
                return NPCExtensions.BeenKilled<FeudalFungus>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedFungusY") : 
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedFungusN");
            }
            else if (Grips)
            {
                return AAWorld.downedGrips ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedGripsY") : 
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedGripsN");
            }
            else if (Brood)
            {
                return NPCExtensions.BeenKilled<Broodmother>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedBroodY") :
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedBroodN");
            }
            else if (Hydra)
            {
                return NPCExtensions.BeenKilled<HydraBody>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedHydraY") : 
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedHydraN");
            }
            else if (Djinn)
            {
                return NPCExtensions.BeenKilled<Djinn>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedDjinnY") : 
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedDjinnN");
            }
            else if (Serpent)
            {
                return NPCExtensions.BeenKilled<SerpentHead>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedSerpentY") : 
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedSerpentN");
            }
            else if (AnubisB)
            {
                if (!BasePlayer.HasItem(player, ModContent.ItemType<Items.BossSummons.Scepter>()))
                {
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<Items.BossSummons.Scepter>(), 1);
                    return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisScapterLost"); 
                }

                return NPCExtensions.BeenKilled<Anubis>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedAnubisBY") :
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedAnubisBN");
            }
            else if (Athena)
            {
                return NPCExtensions.BeenKilled<Athena>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedAthenaY") :
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedAthenaN");
            }
            else if (Greed)
            {
                return NPCExtensions.BeenKilled<Greed>() ? (player.GetModPlayer<AAPlayer>().AnubisBook ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedGreedYBookY") : 
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedGreedYBookN")) :
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedGreedN");
            }
            else if (Rajah)
            {
                return NPCExtensions.BeenKilled<Rajah>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedRajahY") :
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedRajahN");
            }
            else if (AnubisF)
            {
                return NPCExtensions.BeenKilled<ForsakenAnubis>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedFAnubisY") :
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedFAnubisN1") + player.name + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedFAnubisN2");
            }
            else if (AthenaA)
            {
                return NPCExtensions.BeenKilled<AthenaA>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedAthenaAY") :
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedAthenaAN");
            }
            else if (GreedA)
            {
                if (ModSupport.GetMod("CalamityMod") != null)
                {
                    if (DoG && NPCExtensions.BeenKilled<GreedA>())
                    {
                        return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.GreedACalamityMod");
                    }
                }
                return NPCExtensions.BeenKilled<GreedA>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedGreedAY") :
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedGreedAN");
            }
            else if (Equinox)
            {
                return AAWorld.downedEquinox ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedEquinoxY") : 
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedEquinoxN");
            }
            else if (Sisters)
            {
                return AAWorld.downedSisters ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedSistersY") : 
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedSistersN");
            }
            else if (Akuma)
            {
                return AAWorld.downedAkuma ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedAkumaY") : 
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedAkumaN");
            }
            else if (Yamata)
            {
                return AAWorld.downedYamata ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedYamataY") :
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedYamataN");
            }
            else if (Zero)
            {
                return AAWorld.downedZero ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedZeroY") : 
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedZeroN");
            }
            else if (Shen)
            {
                return AAWorld.downedShen ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedShenY") :
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedShenN");
            }
            else if (RajahC)
            {
                return AAWorld.downedShen ?  Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedRajahCY") :
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedRajahCN");
            }
            else
            {
                return GuideChat();
            }
        }

        public static string GuideChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            if (!AAWorld.downedYamata)
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AkumaGuideChat"));
            }

            if (!AAWorld.downedAkuma)
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.YamataGuideChat"));
            }
            if (Main.rand.NextBool(2))
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.BroodMotherGuideChat"));
            }
            else
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.HydraGuideChat"));
            }
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.VoidGuideChat"));
            if (Main.hardMode)
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.HardModeGuideChat1"));
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.HardModeGuideChat2"));
            }

            if (AAWorld.downedEquinox)
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.EquinoxBossGuideChat"));
            }
            return chat;
        }

        public override string GetChat()
        {
            Mod GRealm = ModSupport.GetMod("Grealm");
            Mod Fargos = ModSupport.GetMod("Fargowiltas");
            Mod Redemption = ModSupport.GetMod("Redemption");
            Mod Thorium = ModSupport.GetMod("ThoriumMod");

            //int HordeZombie = GRealm == null ? -1 : NPC.FindFirstNPC(ModSupport.GetModNPC("GRealm", "HordeZombie").npc.type);
            int Mutant = Fargos == null ? -1 : NPC.FindFirstNPC(ModSupport.GetModNPC("Fargowiltas", "Mutant").NPC.type);
            int Newb = Redemption == null ? -1 : NPC.FindFirstNPC(ModSupport.GetModNPC("Redemption", "Newb").NPC.type);
            int Cobbler = Thorium == null ? -1 : NPC.FindFirstNPC(ModSupport.GetModNPC("ThoriumMod", "Cobbler").NPC.type);
            int ConfusedZombie = Thorium == null ? -1 : NPC.FindFirstNPC(ModSupport.GetModNPC("ThoriumMod", "ConfusedZombie").NPC.type);

            WeightedRandom<string> chat = new WeightedRandom<string>();

            Player player = Main.LocalPlayer;
            AAPlayer mPlayer = player.GetModPlayer<AAPlayer>();

            if (player.head == ModContent.ItemType<AnubisMask>() && Main.rand.NextBool(5))
            {
                return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChatMask");
            }

            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat1"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat2"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat3"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat4"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat5"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat6"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat7"));
            if (NPCExtensions.BeenKilled<Djinn>())
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat8"));
            }
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat9"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat10"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat11") + (WorldGen.crimson ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat12") : Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat13")) + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat14"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat15"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat32"));



            int FemaleNPC = NPC.FindFirstNPC(FindFemaleNPC());


            if (Main.bloodMoon && FemaleNPC != NPCID.PartyGirl)
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat16") + Main.npc[FemaleNPC].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat17"));
            }
            else if (Main.bloodMoon && FemaleNPC == NPCID.PartyGirl)
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat18") + Main.npc[FemaleNPC].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat19"));
            }

            if (player.head == 200 && player.body == 198 && player.legs == 142)
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat20"));
            }

            if (BirthdayParty.GenuineParty || BirthdayParty.ManualParty)
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat21"));
            }

            /*if (HordeZombie >= 0)
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat23") + Main.npc[HordeZombie].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat24"));
            }*/

            if (Mutant >= 0)
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat25") + Main.npc[Mutant].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat26"));
            }

            if (Newb >= 0)
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat27") + Main.npc[Newb].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat28"));
            }

            if (Cobbler >= 0)
            {
                chat.Add(Main.npc[Cobbler].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat29"));
            }

            if (ConfusedZombie >= 0)
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat30") + Main.npc[ConfusedZombie].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat31"));
            }

            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !BasePlayer.HasItem(player, ModContent.ItemType<Items.BossSummons.Scepter>()))
            {
                if (!mPlayer.GivenAnuSummon)
                {
                    mPlayer.GivenAnuSummon = true;
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<Items.BossSummons.Scepter>(), 1);
                    return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.GetSummonItemChat");
                }
            }

            if (NPCExtensions.BeenKilled<ForsakenAnubis>() && !BasePlayer.HasItem(player, ModContent.ItemType<WormIdol>()))
            {
                if (!mPlayer.GivenWormIdol)
                {
                    mPlayer.GivenWormIdol = true;
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<WormIdol>(), 1);
                    return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.GetSummonItemChat2");
                }
            }

            return chat;
        }

        public static string WHATTHEFUCKDOIDOANUBIS()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            
            return chat;
        }

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
    }
}