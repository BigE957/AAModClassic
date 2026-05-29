using AAModClassic._Content._Dev.___PreHardmode.Items.Materials;
using AAModClassic._Content.Acropolis.__Hardmode.NPCs.__BossAthena;
using AAModClassic._Content.Acropolis._PostMoonlord.NPCs.__BossAthenaA;
using AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit;
using AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahRabbitA;
using AAModClassic._Content.Desert.___PreHardmode.NPCs.__BossDesertDjinn;
using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.BossStandard;
using AAModClassic._Content.Desert.__Hardmode.Items.Quest;
using AAModClassic._Content.Desert.__Hardmode.Items.Weapons;
using AAModClassic._Content.Desert.__Hardmode.NPCs.__BossAnubis;
using AAModClassic._Content.Desert._PostMoonlord.NPCs.__BossAnubisA;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossFeudalFungus;
using AAModClassic._Content.Hoard.__Hardmode.NPCs.__BossGreed;
using AAModClassic._Content.Hoard._PostMoonlord.NPCs.__BossGreedA;
using AAModClassic._Content.Inferno.___PreHardmode.NPCs.__BossBroodmother;
using AAModClassic._Content.Mire.___PreHardmode.NPCs.__BossHydra;
using AAModClassic._Content.RedMushroom.___PreHardmode.NPCs.__BossMushroomMonarch;
using AAModClassic._Content.Snow.___PreHardmode.NPCs.__BossSubzeroSerpent;
using AAModClassic._Content.Stars._PostMoonlord.Items.Quest;
using AAModClassic._Unofficial.Desert;
using AAModClassic._Unreleased.Content.Desert.__Hardmode.NPCs.__BossAnubis;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.CrossMod;
using AAModClassic.UI.WorldGen;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAModClassic._Content.Desert.___PreHardmode.NPCs.Friendly
{
    [AutoloadHead]
	public class Legendscribe : ModNPC
	{
        private static int ShimmerHeadIndex;

        public static Asset<Texture2D> Glowmask;
        public static Asset<Texture2D> GlowmaskShimmer;

        public override void Load()
        {
            ShimmerHeadIndex = Mod.AddNPCHeadTexture(Type, Texture + "_Shimmer_Head");

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            GlowmaskShimmer = ModContent.Request<Texture2D>(Texture + "_Shimmer_Glow");
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
            Main.npcFrameCount[Type] = 26;
            NPCID.Sets.ExtraFramesCount[Type] = 10;
            NPCID.Sets.AttackFrameCount[Type] = 5;

            NPCID.Sets.DangerDetectRange[Type] = 700;
            NPCID.Sets.AttackType[Type] = 0;
            NPCID.Sets.AttackTime[Type] = 40;
            NPCID.Sets.AttackAverageChance[Type] = 20;

            NPCID.Sets.HatOffsetY[Type] = 3;

            NPCID.Sets.ShimmerTownTransform[Type] = true;
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
            NPC.dontTakeDamageFromHostiles = true;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.buffImmune[BuffID.Shimmer] = false;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.Legendscribe")
            ]);
        }

        public float TeleportToHouseTimer = 0;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(TeleportToHouseTimer);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                TeleportToHouseTimer = reader.ReadSingle();
            }
        }

		public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (player.active && !NPC.AnyNPCs(ModContent.NPCType<Anubis>()) && 
                    !NPC.AnyNPCs(ModContent.NPCType<AnubisForsakenTransition>()) &&
                    !NPC.AnyNPCs(ModContent.NPCType<AnubisA>()))
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

        public static bool SwitchInfo = false;
        public static bool DoNext = false;
        public static bool Mushroom = false;
        public static bool Glowshroom = false;
        public static bool Grips = false;
        public static bool Brood = false;
        public static bool Hydra = false;
        public static bool Djinn = false;
        public static bool Serpent = false;
        public static bool Retriever = false;
        public static bool OrthrusX = false;
        public static bool RaiderUltima = false;
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
            Retriever = false;
            OrthrusX = false;
            RaiderUltima = false;
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

            string RetrieverT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons9");

            string OrthrusT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons9");

            string RaiderT = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.SetChatButtons9");

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

            int siegeOffset = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed) ? 3 : 0;

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
            else if (siegeOffset == 3 && Main.hardMode && ChatNumber == 8)
            {
                button2 = RetrieverT;
                Retriever = true;
            }
            else if (siegeOffset == 3 && Main.hardMode && ChatNumber == 9)
            {
                button2 = OrthrusT;
                OrthrusX = true;
            }
            else if (siegeOffset == 3 && Main.hardMode && ChatNumber == 10)
            {
                button2 = RaiderT;
                RaiderUltima = true;
            }
            else if (ChatNumber == 8 + siegeOffset && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {
                button2 = AnubisT;
                AnubisB = true;
            }
            else if (ChatNumber == 9 + siegeOffset && NPCExtensions.BeenKilled<Anubis>())
            {
                button2 = AthenaT;
                Athena = true;
            }
            else if (ChatNumber == 10 + siegeOffset && NPCExtensions.BeenKilled<Anubis>())
            {
                button2 = GreedT;
                Greed = true;
            }
            else if (ChatNumber == 11 + siegeOffset && Main.hardMode)
            {
                button2 = RajahT;
                Rajah = true;
            }
            else if (ChatNumber == 12 + siegeOffset && NPC.downedMoonlord && NPCExtensions.BeenKilled<Anubis>())
            {
                button2 = AnubisFT;
                AnubisF = true;
            }
            else if (ChatNumber == 13 + siegeOffset && NPC.downedMoonlord && NPCExtensions.BeenKilled<AnubisA>() && NPCExtensions.BeenKilled<Athena>())
            {
                button2 = AthenaAT;
                AthenaA = true;
            }
            else if (ChatNumber == 14 + siegeOffset && NPC.downedMoonlord && NPCExtensions.BeenKilled<AnubisA>() && NPCExtensions.BeenKilled<GreedHead>())
            {
                button2 = GreedAT;
                GreedA = true;
            }
            else if (ChatNumber == 15 + siegeOffset && NPCExtensions.BeenKilled<GreedAHead>() && NPCExtensions.BeenKilled<AthenaA>())
            {
                button2 = EquinoxT;
                Equinox = true;
            }
            else if (ChatNumber == 16 + siegeOffset && NPC.downedMoonlord && AAWorld.downedEquinox)
            {
                button2 = SistersT;
                Sisters = true;
            }
            else if (ChatNumber == 17 + siegeOffset && NPC.downedMoonlord && AAWorld.downedSisters)
            {
                button2 = AkumaT;
                Akuma = true;
            }
            else if (ChatNumber == 18 + siegeOffset && NPC.downedMoonlord && AAWorld.downedSisters)
            {
                button2 = YamataT;
                Yamata = true;
            }
            else if (ChatNumber == 19 + siegeOffset && NPC.downedMoonlord && AAWorld.downedNC)
            {
                button2 = ZeroT;
                Zero = true;
            }
            else if (ChatNumber == 20 + siegeOffset && NPCExtensions.BeenKilled<RajahRabbitA>())
            {
                button2 = RajahCT;
                RajahC = true;
            }
            else if (ChatNumber == 21 + siegeOffset && AAWorld.downedAllAncients)
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
            if(WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                NPC.Transform(ModContent.NPCType<LegendscribeUnofficial>());
                return false;
            }

            if (NPC.AnyNPCs(ModContent.NPCType<Anubis>()) ||
                NPC.AnyNPCs(ModContent.NPCType<AnubisForsakenTransition>()) ||
                NPC.AnyNPCs(ModContent.NPCType<AnubisA>()) ||
                NPC.AnyNPCs(ModContent.NPCType<AnubisUnreleased>()))
            {
                TPDust();
                NPC.active = false;
            }
            if (Vector2.Distance(NPC.position, new Vector2(NPC.homeTileX, NPC.homeTileY)) > 3000 && TeleportToHouseTimer < 240 && !NPC.homeless)
            {
                TeleportToHouseTimer++;
                if (TeleportToHouseTimer >= 240)
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
                            TeleportToHouseTimer = 0;
                        }
                    }
                }
            }
            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = NPC.IsShimmerVariant ? GlowmaskShimmer.Value : Glowmask.Value;
            //TODO: this is disgusting. find out whatever stat vanilla modifies while npcs are sitting to lift the spirte up (npc.ai[0] == 5)
            int sittingOffset = NPC.ai[0] == 5 ? 4 : 0;
            spriteBatch.Draw(tex, NPC.Center - screenPos + new Vector2(0f, NPC.gfxOffY - (4 + sittingOffset)), NPC.frame, Color.White * NPC.Opacity, NPC.rotation, NPC.frame.Size() / 2f, NPC.scale, NPC.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
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
                return NPCExtensions.BeenKilled<DesertDjinn>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedDjinnY") : 
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedDjinnN");
            }
            else if (Serpent)
            {
                return NPCExtensions.BeenKilled<SubzeroSerpent_Head>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedSerpentY") : 
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedSerpentN");
            }
            else if (Retriever)
            {
                return NPCExtensions.BeenKilled<SubzeroSerpent_Head>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedRetrieverY") :
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedRetrieverN");
            }
            else if (OrthrusX)
            {
                return NPCExtensions.BeenKilled<SubzeroSerpent_Head>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedOrthrusXY") :
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedOrthrusXN");
            }
            else if (RaiderUltima)
            {
                return NPCExtensions.BeenKilled<SubzeroSerpent_Head>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedRaiderUltimaY") :
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedRaiderUltimaN");
            }
            else if (AnubisB)
            {
                if (!BasePlayer.HasItem(player, ModContent.ItemType<__Hardmode.Items._BossAnubis.RasScepter>()))
                {
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<__Hardmode.Items._BossAnubis.RasScepter>(), 1);
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
                return NPCExtensions.BeenKilled<GreedHead>() ? (player.GetModPlayer<AAPlayer>().AnubisBook ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedGreedYBookY") : 
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedGreedYBookN")) :
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedGreedN");
            }
            else if (Rajah)
            {
                return NPCExtensions.BeenKilled<RajahRabbit>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedRajahY") :
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedRajahN");
            }
            else if (AnubisF)
            {
                return NPCExtensions.BeenKilled<AnubisA>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedFAnubisY") :
                    Language.GetOrRegister("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedFAnubisN").FormatWith(player.name);
            }
            else if (AthenaA)
            {
                return NPCExtensions.BeenKilled<AthenaA>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedAthenaAY") :
                    Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedAthenaAN");
            }
            else if (GreedA)
            {
                return NPCExtensions.BeenKilled<GreedAHead>() ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.downedGreedAY") :
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
            return LegendscribeDialogue(NPC);
        }

        public static string LegendscribeDialogue(NPC npc)
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            Player player = Main.LocalPlayer;
            AAPlayer mPlayer = player.GetModPlayer<AAPlayer>();

            #region general
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat1"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat2"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat3"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat4"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat5"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat6"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat7"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat9"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat10"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat11") + (WorldGen.crimson ? Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat12") : Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat13")) + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat14"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat15"));

            // line.
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat32"));
            else
            {
                int integerLimit = int.MaxValue;
                if (player.HasItem(ModContent.ItemType<ShinyCharm>()) || player.HasItem(ModContent.ItemType<ShinyCharmFish>()))
                    integerLimit -= 1;
                if (Main.rand.NextBool(integerLimit))
                    return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat32");
            }
            #endregion

            #region conditional
            int femaleNPC = FindFemaleNPC();
            if (Main.bloodMoon && femaleNPC > -1)
            {
                femaleNPC = NPC.FindFirstNPC(femaleNPC);
                if (femaleNPC != NPCID.PartyGirl)
                    chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat16") + Main.npc[femaleNPC].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat17"));
                else if (femaleNPC == NPCID.PartyGirl)
                    chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat18") + Main.npc[femaleNPC].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat19"));
            }

            if (player.head == ArmorIDs.Head.AncientBattleArmor && player.body == ArmorIDs.Body.AncientBattleArmor && player.legs == ArmorIDs.Legs.AncientBattleArmor && Main.rand.NextBool(4))
                return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat20");
            else if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && player.head == ArmorIDs.Head.AncientArmor && player.body == ArmorIDs.Body.AncientArmor && player.legs == ArmorIDs.Legs.AncientArmor && Main.rand.NextBool(4))
                return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat20");

            if ((player.armor[0].type == ModContent.ItemType<AnubisMask>() || player.armor[10].type == ModContent.ItemType<AnubisMask>()) && Main.rand.NextBool(4))
                return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChatMask");

            if (NPCExtensions.BeenKilled<DesertDjinn>())
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat8"));

            if (BirthdayParty.GenuineParty || BirthdayParty.ManualParty)
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat21"));
            #endregion

            #region crossmod
            Mod GRealm = ModSupport.GetMod("Grealm");
            Mod Fargos = ModSupport.GetMod("Fargowiltas");
            Mod Redemption = ModSupport.GetMod("Redemption");
            Mod Thorium = ModSupport.GetMod("ThoriumMod");
            Mod SOTS = ModSupport.GetMod("SOTS");

            int HordeZombie = GRealm == null ? -1 : NPC.FindFirstNPC(ModSupport.GetModNPC("GRealm", "HordeZombie").NPC.type);
            if (HordeZombie >= 0)
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat23") + Main.npc[HordeZombie].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat24"));

            int Mutant = Fargos == null ? -1 : NPC.FindFirstNPC(ModSupport.GetModNPC("Fargowiltas", "Mutant").NPC.type);
            if (Mutant >= 0)
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat25") + Main.npc[Mutant].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat26"));

            int Newb = Redemption == null ? -1 : NPC.FindFirstNPC(ModSupport.GetModNPC("Redemption", "Newb").NPC.type);
            if (Newb >= 0)
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat27") + Main.npc[Newb].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat28"));

            int Cobbler = Thorium == null ? -1 : NPC.FindFirstNPC(ModSupport.GetModNPC("ThoriumMod", "Cobbler").NPC.type);
            if (Cobbler >= 0)
                chat.Add(Main.npc[Cobbler].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat29"));

            int ConfusedZombie = Thorium == null ? -1 : NPC.FindFirstNPC(ModSupport.GetModNPC("ThoriumMod", "ConfusedZombie").NPC.type);
            if (ConfusedZombie >= 0)
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat30") + Main.npc[ConfusedZombie].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChat31"));

            if (ModLoader.TryGetMod("CalamityMod", out var cal))
            {
                if (NPCExtensions.BeenKilled<GreedAHead>() && (bool)cal.Call("GetBossDowned", "devourerofgods"))
                {
                    chat.Add("Mods.AAModClassic.NPCs.TownNPCs.Anubis.GreedACalamityMod");
                }
            }

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                bool isWearingAnubisHatFromSOTS = SOTS != null && (player.armor[0].type == ModSupport.GetModItem("SOTS", "AnubisHat").Item.type || player.armor[10].type == ModSupport.GetModItem("SOTS", "AnubisHat").Item.type) ? true : false;
                if (isWearingAnubisHatFromSOTS)
                {
                    int textToUse = Main.rand.Next(4);
                    if (textToUse == 0)
                        return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChatSOTSAnubisMask1");
                    else if (textToUse == 1)
                        return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChatSOTSAnubisMask2");
                    else if (textToUse == 2)
                        return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChatSOTSAnubisMask3");
                    else if (textToUse == 3)
                        return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.AnubisChatSOTSAnubisMask4");
                }
            }
            #endregion

            #region progression
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !BasePlayer.HasItem(player, ModContent.ItemType<__Hardmode.Items._BossAnubis.RasScepter>()))
            {
                if (!mPlayer.GivenAnuSummon)
                {
                    mPlayer.GivenAnuSummon = true;
                    player.QuickSpawnItem(npc.GetSource_GiftOrReward(), ModContent.ItemType<__Hardmode.Items._BossAnubis.RasScepter>(), 1);
                    return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.GetSummonItemChat");
                }
            }

            if (NPCExtensions.BeenKilled<AnubisA>() && !BasePlayer.HasItem(player, ModContent.ItemType<WormIdol>()))
            {
                if (!mPlayer.GivenWormIdol)
                {
                    mPlayer.GivenWormIdol = true;
                    player.QuickSpawnItem(npc.GetSource_GiftOrReward(), ModContent.ItemType<WormIdol>(), 1);
                    return Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Anubis.GetSummonItemChat2");
                }
            }
            #endregion

            return chat;
        }

        /// <summary>
        /// unused but i dont have the heart to remove it
        /// </summary>
        /// <returns>a completely emptyy weightedrandom</returns>
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
            projType = ModContent.ProjectileType<Legendscribe_Judgement>();
            attackDelay = 5;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {

            multiplier = 4f;

            randomOffset = 2f;

        }

        public static int FindFemaleNPC()
        {
            List<int> femaleNPCIDs = [];
            foreach (NPC npc in Main.npc)
            {
                if (npc.townNPC == true && npc.active)
                {
                    bool? isFemale = NPCLoader.CanGoToStatue(npc, false);
                    if (isFemale == true)
                        femaleNPCIDs.Add(npc.type);
                    // dear redigit: i hope you go to hell
                    if (npc.type == NPCID.Nurse || npc.type == NPCID.BestiaryGirl || npc.type == NPCID.Dryad || npc.type == NPCID.Stylist || npc.type == NPCID.Mechanic || npc.type == NPCID.PartyGirl || npc.type == NPCID.Steampunker || npc.type == NPCID.Princess)
                        femaleNPCIDs.Add(npc.type);
                }
            }

            if (femaleNPCIDs.Count == 0)
                return -1;

            int femaleNPC = femaleNPCIDs[Main.rand.Next(femaleNPCIDs.Count)];
            return femaleNPC;
        }
    }
}