
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
using AAModClassic.Items.BossSummons;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Misc;
using AAModClassic.CrossMod;

namespace AAModClassic.NPCs.TownNPCs
{
    [AutoloadHead]
	public class Anubis : ModNPC
	{
        public override string Texture => "AAMod/NPCs/TownNPCs/Anubis";
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
                internalAI = reader.ReadFloat();
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
			string SwitchInfoT = Lang.TownNPCAnubis("SetChatButtons1");

            string DoNextT = Lang.TownNPCAnubis("SetChatButtons2");

            string MushT = Lang.TownNPCAnubis("SetChatButtons3");

            string GlowT = Lang.TownNPCAnubis("SetChatButtons4");

            string GripT = Lang.TownNPCAnubis("SetChatButtons5");

            string BroodT = Lang.TownNPCAnubis("SetChatButtons6");

            string HydraT = Lang.TownNPCAnubis("SetChatButtons7");

            string DjinnT = Lang.TownNPCAnubis("SetChatButtons8");

            string SerpentT = Lang.TownNPCAnubis("SetChatButtons9");

            string AnubisT = Lang.TownNPCAnubis("SetChatButtons14");

            string AthenaT = Lang.TownNPCAnubis("SetChatButtons21");

            string GreedT = Lang.TownNPCAnubis("SetChatButtons22");

            string RajahT = Lang.TownNPCAnubis("SetChatButtons23");

            string AnubisFT = Lang.TownNPCAnubis("SetChatButtons27");

            string AthenaAT = Lang.TownNPCAnubis("SetChatButtons24");

            string GreedAT = Lang.TownNPCAnubis("SetChatButtons25");

            string EquinoxT = Lang.TownNPCAnubis("SetChatButtons13");

            string SistersT = Lang.TownNPCAnubis("SetChatButtons15");

            string AkumaT = Lang.TownNPCAnubis("SetChatButtons16");

            string YamataT = Lang.TownNPCAnubis("SetChatButtons17");

            string ZeroT = Lang.TownNPCAnubis("SetChatButtons18");

            string ShenT = Lang.TownNPCAnubis("SetChatButtons19");

            string RajahCT = Lang.TownNPCAnubis("SetChatButtons26");
            
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
            else if (ChatNumber == 9 && AAWorld.downedAnubis)
            {
                button2 = AthenaT;
                Athena = true;
            }
            else if (ChatNumber == 10 && AAWorld.downedAnubis)
            {
                button2 = GreedT;
                Greed = true;
            }
            else if (ChatNumber == 11 && Main.hardMode)
            {
                button2 = RajahT;
                Rajah = true;
            }
            else if (ChatNumber == 12 && NPC.downedMoonlord && AAWorld.downedAnubis)
            {
                button2 = AnubisFT;
                AnubisF = true;
            }
            else if (ChatNumber == 13 && NPC.downedMoonlord && AAWorld.downedAnubisA && AAWorld.downedAthena)
            {
                button2 = AthenaAT;
                AthenaA = true;
            }
            else if (ChatNumber == 14 && NPC.downedMoonlord && AAWorld.downedAnubisA && AAWorld.downedGreed)
            {
                button2 = GreedAT;
                GreedA = true;
            }
            else if (ChatNumber == 15 && AAWorld.downedGreedA && AAWorld.downedAthenaA)
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
            else if (ChatNumber == 20 && AAWorld.downedRajahsRevenge)
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

        public void ResetBools()
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
                int Item = player.FindItem(ModContent.ItemType<AnubisBook>());
                if (Item >= 0 && !player.GetModPlayer<AAPlayer>().AnubisBook && Greed)
                {
                    player.inventory[Item].stack--;
                    if (player.inventory[Item].stack <= 0)
                    {
                        player.inventory[Item] = new Item();
                    }

                    Main.npcChatText = Lang.TownNPCAnubis("GetBookChat");
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<Items.Magic.AnubisBlockBook>(), 1);

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
                            CombatText.NewText(NPC.Hitbox, Color.Gold, Lang.TownNPCAnubis("CombatTextChat"));
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
            Texture2D t = Mod.GetTexture(AAWorld.downedAnubisA ? "NPCs/TownNPCs/AnubisF" : "NPCs/TownNPCs/Anubis");
            Texture2D g = Mod.GetTexture(AAWorld.downedAnubisA ? "Glowmasks/AnubisF_Glow" : "Glowmasks/Anubis_Glow");
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
                return AAWorld.downedMonarch ? Lang.TownNPCAnubis("downedMonarchY") : 
                    Lang.TownNPCAnubis("downedMonarchN");
            }
            else if (Glowshroom)
            {
                return AAWorld.downedFungus ? Lang.TownNPCAnubis("downedFungusY") : 
                    Lang.TownNPCAnubis("downedFungusN");
            }
            else if (Grips)
            {
                return AAWorld.downedGrips ? Lang.TownNPCAnubis("downedGripsY") : 
                    Lang.TownNPCAnubis("downedGripsN");
            }
            else if (Brood)
            {
                return AAWorld.downedBrood ? Lang.TownNPCAnubis("downedBroodY") :
                    Lang.TownNPCAnubis("downedBroodN");
            }
            else if (Hydra)
            {
                return AAWorld.downedHydra ? Lang.TownNPCAnubis("downedHydraY") : 
                    Lang.TownNPCAnubis("downedHydraN");
            }
            else if (Djinn)
            {
                return AAWorld.downedDjinn ? Lang.TownNPCAnubis("downedDjinnY") : 
                    Lang.TownNPCAnubis("downedDjinnN");
            }
            else if (Serpent)
            {
                return AAWorld.downedSerpent ? Lang.TownNPCAnubis("downedSerpentY") : 
                    Lang.TownNPCAnubis("downedSerpentN");
            }
            else if (AnubisB)
            {
                if (!BasePlayer.HasItem(player, ModContent.ItemType<Items.BossSummons.Scepter>()))
                {
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<Items.BossSummons.Scepter>(), 1);
                    return Lang.TownNPCAnubis("AnubisScapterLost"); 
                }

                return AAWorld.downedAnubis ? Lang.TownNPCAnubis("downedAnubisBY") :
                    Lang.TownNPCAnubis("downedAnubisBN");
            }
            else if (Athena)
            {
                return AAWorld.downedAthena ? Lang.TownNPCAnubis("downedAthenaY") :
                    Lang.TownNPCAnubis("downedAthenaN");
            }
            else if (Greed)
            {
                return AAWorld.downedGreed ? (player.GetModPlayer<AAPlayer>().AnubisBook ? Lang.TownNPCAnubis("downedGreedYBookY") : 
                    Lang.TownNPCAnubis("downedGreedYBookN")) :
                    Lang.TownNPCAnubis("downedGreedN");
            }
            else if (Rajah)
            {
                return AAWorld.downedRajah ? Lang.TownNPCAnubis("downedRajahY") :
                    Lang.TownNPCAnubis("downedRajahN");
            }
            else if (AnubisF)
            {
                return AAWorld.downedAnubisA ? Lang.TownNPCAnubis("downedFAnubisY") :
                    Lang.TownNPCAnubis("downedFAnubisN1") + player.name + Lang.TownNPCAnubis("downedFAnubisN2");
            }
            else if (AthenaA)
            {
                return AAWorld.downedAthenaA ? Lang.TownNPCAnubis("downedAthenaAY") :
                    Lang.TownNPCAnubis("downedAthenaAN");
            }
            else if (GreedA)
            {
                if (ModSupport.GetMod("CalamityMod") != null)
                {
                    if (DoG && AAWorld.downedGreedA)
                    {
                        return Lang.TownNPCAnubis("GreedACalamityMod");
                    }
                }
                return AAWorld.downedGreedA ? Lang.TownNPCAnubis("downedGreedAY") :
                    Lang.TownNPCAnubis("downedGreedAN");
            }
            else if (Equinox)
            {
                return AAWorld.downedEquinox ? Lang.TownNPCAnubis("downedEquinoxY") : 
                    Lang.TownNPCAnubis("downedEquinoxN");
            }
            else if (Sisters)
            {
                return AAWorld.downedSisters ? Lang.TownNPCAnubis("downedSistersY") : 
                    Lang.TownNPCAnubis("downedSistersN");
            }
            else if (Akuma)
            {
                return AAWorld.downedAkuma ? Lang.TownNPCAnubis("downedAkumaY") : 
                    Lang.TownNPCAnubis("downedAkumaN");
            }
            else if (Yamata)
            {
                return AAWorld.downedYamata ? Lang.TownNPCAnubis("downedYamataY") :
                    Lang.TownNPCAnubis("downedYamataN");
            }
            else if (Zero)
            {
                return AAWorld.downedZero ? Lang.TownNPCAnubis("downedZeroY") : 
                    Lang.TownNPCAnubis("downedZeroN");
            }
            else if (Shen)
            {
                return AAWorld.downedShen ? Lang.TownNPCAnubis("downedShenY") :
                    Lang.TownNPCAnubis("downedShenN");
            }
            else if (RajahC)
            {
                return AAWorld.downedShen ?  Lang.TownNPCAnubis("downedRajahCY") :
                    Lang.TownNPCAnubis("downedRajahCN");
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
                chat.Add(Lang.TownNPCAnubis("AkumaGuideChat"));
            }

            if (!AAWorld.downedAkuma)
            {
                chat.Add(Lang.TownNPCAnubis("YamataGuideChat"));
            }
            if (Main.rand.Next(2) == 0)
            {
                chat.Add(Lang.TownNPCAnubis("BroodMotherGuideChat"));
            }
            else
            {
                chat.Add(Lang.TownNPCAnubis("HydraGuideChat"));
            }
            chat.Add(Lang.TownNPCAnubis("VoidGuideChat"));
            if (Main.hardMode)
            {
                chat.Add(Lang.TownNPCAnubis("HardModeGuideChat1"));
                chat.Add(Lang.TownNPCAnubis("HardModeGuideChat2"));
            }

            if (AAWorld.downedEquinox)
            {
                chat.Add(Lang.TownNPCAnubis("EquinoxBossGuideChat"));
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

            if (player.head == ModContent.ItemType<Items.Vanity.Mask.AnubisMask>() && Main.rand.Next(5) == 0)
            {
                return Lang.TownNPCAnubis("AnubisChatMask");
            }

            chat.Add(Lang.TownNPCAnubis("AnubisChat1"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat2"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat3"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat4"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat5"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat6"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat7"));
            if (AAWorld.downedDjinn)
            {
                chat.Add(Lang.TownNPCAnubis("AnubisChat8"));
            }
            chat.Add(Lang.TownNPCAnubis("AnubisChat9"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat10"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat11") + (WorldGen.crimson ? Lang.TownNPCAnubis("AnubisChat12") : Lang.TownNPCAnubis("AnubisChat13")) + Lang.TownNPCAnubis("AnubisChat14"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat15"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat32"));



            int FemaleNPC = NPC.FindFirstNPC(FindFemaleNPC());


            if (Main.bloodMoon && FemaleNPC != NPCID.PartyGirl)
            {
                chat.Add(Lang.TownNPCAnubis("AnubisChat16") + Main.npc[FemaleNPC].GivenName + Lang.TownNPCAnubis("AnubisChat17"));
            }
            else if (Main.bloodMoon && FemaleNPC == NPCID.PartyGirl)
            {
                chat.Add(Lang.TownNPCAnubis("AnubisChat18") + Main.npc[FemaleNPC].GivenName + Lang.TownNPCAnubis("AnubisChat19"));
            }

            if (player.head == 200 && player.body == 198 && player.legs == 142)
            {
                chat.Add(Lang.TownNPCAnubis("AnubisChat20"));
            }

            if (BirthdayParty.GenuineParty || BirthdayParty.ManualParty)
            {
                chat.Add(Lang.TownNPCAnubis("AnubisChat21"));
            }

            /*if (HordeZombie >= 0)
            {
                chat.Add(Lang.TownNPCAnubis("AnubisChat23") + Main.npc[HordeZombie].GivenName + Lang.TownNPCAnubis("AnubisChat24"));
            }*/

            if (Mutant >= 0)
            {
                chat.Add(Lang.TownNPCAnubis("AnubisChat25") + Main.npc[Mutant].GivenName + Lang.TownNPCAnubis("AnubisChat26"));
            }

            if (Newb >= 0)
            {
                chat.Add(Lang.TownNPCAnubis("AnubisChat27") + Main.npc[Newb].GivenName + Lang.TownNPCAnubis("AnubisChat28"));
            }

            if (Cobbler >= 0)
            {
                chat.Add(Main.npc[Cobbler].GivenName + Lang.TownNPCAnubis("AnubisChat29"));
            }

            if (ConfusedZombie >= 0)
            {
                chat.Add(Lang.TownNPCAnubis("AnubisChat30") + Main.npc[ConfusedZombie].GivenName + Lang.TownNPCAnubis("AnubisChat31"));
            }

            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !BasePlayer.HasItem(player, ModContent.ItemType<Items.BossSummons.Scepter>()))
            {
                if (!mPlayer.GivenAnuSummon)
                {
                    mPlayer.GivenAnuSummon = true;
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<Items.BossSummons.Scepter>(), 1);
                    return Lang.TownNPCAnubis("GetSummonItemChat");
                }
            }

            if (AAWorld.downedAnubisA && !BasePlayer.HasItem(player, ModContent.ItemType<WormIdol>()))
            {
                if (!mPlayer.GivenWormIdol)
                {
                    mPlayer.GivenWormIdol = true;
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<WormIdol>(), 1);
                    return Lang.TownNPCAnubis("GetSummonItemChat2");
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

        public int FindFemaleNPC()
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