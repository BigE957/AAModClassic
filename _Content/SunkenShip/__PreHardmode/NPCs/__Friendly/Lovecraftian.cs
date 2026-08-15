using AAModClassic._Content.Corruption.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Crimson.___PreHardmode.Items.Consumables;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items.Consumables;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items.Materials;
using AAModClassic._Content.Hallow.__Hardmode.Items.Consumables;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Jungle.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Mire.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Ocean.___PreHardmode.Items.Quest;
using AAModClassic._Content.Purity.___PreHardmode.Items.Consumables;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Consumables;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Materials;
using AAModClassic._Content.RedMushroom.___PreHardmode.NPCs.Friendly;
using AAModClassic._Content.Snow.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Terrarium.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Terrarium.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAModClassic._Content.SunkenShip.__PreHardmode.NPCs.__Friendly
{
    [AutoloadHead]
	public class Lovecraftian : ModNPC, ILocalizedModType
    {
        public new string LocalizationCategory => "NPCs.TownNPCs";

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 26;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 10;
            NPCID.Sets.AttackFrameCount[NPC.type] = 5;
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 40;
            NPCID.Sets.AttackAverageChance[NPC.type] = 20;
            NPCID.Sets.HatOffsetY[NPC.type] = 3;

            NPC.Happiness
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Love)
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.WitchDoctor, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Dislike);
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.damage = 40;
            NPC.defense = 38;
            NPC.lifeMax = 600;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Guide;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.Lovecraftian")
            ]);
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
        {
            if (!AAConfigClient.Instance.NoAATownNPC)
            {
                for (int k = 0; k < 255; k++)
                {
                    Player player = Main.player[k];
                    if (player.active)
                    {
                        if (NPC.downedBoss1 == true)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public override bool CanGoToStatue(bool toKingStatue) => !toKingStatue;

        public override List<string> SetNPCNameList()/* tModPorter Suggestion: Return a list of names */
		{
            return ["Aletheia", "C'thalpa", "D’endrrah", "Ycnagnnisssz", "Yidhra"];			
		}
        
        public override string GetChat()
        {   
            WeightedRandom<string> chat = new WeightedRandom<string>();

            int Pirate = NPC.FindFirstNPC(NPCID.Pirate);

            int Mutant = !ModLoader.TryGetMod("Fargowiltas", out Mod Fargos) ? -1 : NPC.FindFirstNPC(Fargos.Find<ModNPC>("Mutant").Type);

            int HordeZombie = !ModLoader.TryGetMod("GRealm", out Mod GRealm) ? -1 : NPC.FindFirstNPC(GRealm.Find<ModNPC>("HordeZombie").Type);

            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.LovecraftianChat1"));

            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.LovecraftianChat2"));

            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.LovecraftianChat3"));

            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.LovecraftianChat4"));

            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.LovecraftianChat5"));

            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.LovecraftianChat6"));

            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.LovecraftianChat7"));
            

            //If Pirate is present
            if (Pirate >= 0)
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.LovecraftianChat8") + Main.npc[Pirate].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.LovecraftianChat9"));
            }

            //If mutant is present

            if (Mutant >= 0)
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.LovecraftianChat10") + Main.npc[Mutant].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.LovecraftianChat11"));
            }

            //If Horde Zombie is present
            if (HordeZombie >= 0)
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.LovecraftianChat12"));
            }


            //Post - Moon Lord
            if (NPC.downedMoonlord)
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.LovecraftianChat13"));
            }

            //Providing materials

            return chat; 
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.button1");
            button2 = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.button2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            Player player = Main.LocalPlayer;
            ZAAPlayer p = player.GetModPlayer<ZAAPlayer>();

            if (firstButton)
            {
                shopName = "Shop";
            }

            if (!firstButton)
            {
                SoundEngine.PlaySound(SoundID.MenuTick);

                int Mushman = NPC.FindFirstNPC(ModContent.NPCType<Mushman>());

                int Item1 = player.FindItem(ModContent.ItemType<TerraShard>());
                int Item2 = player.FindItem(ModContent.ItemType<DragonScale>());
                int Item3 = player.FindItem(ModContent.ItemType<MirePod>());
                int Item4 = player.FindItem(ItemID.RottenChunk);
                int Item5 = player.FindItem(ItemID.Vertebrae);
                int Item6 = player.FindItem(ItemID.PixieDust);
                int Item7 = player.FindItem(ModContent.ItemType<DoomiteScrap>());
                int Item8 = player.FindItem(ItemID.JungleSpores);
                int Item9 = player.FindItem(ModContent.ItemType<Mushium>());
                int Item10 = player.FindItem(ModContent.ItemType<GlowingMushium>());
                int Item11 = player.FindItem(ItemID.Stinger);
                int Item12 = player.FindItem(ItemID.IceMachine);
                int Item13 = player.FindItem(ItemID.Bunny);

                if (Item1 >= 0 && AAWorld.squid1 < 5) //Item 1: 3 Blueberries
                {
                    Main.npcChatCornerItem = ModContent.ItemType<TerraShard>();
                    player.inventory[Item1].stack--;
                    if (player.inventory[Item1].stack <= 0)
                    {
                        player.inventory[Item1] = new Item();
                    }
                    if (AAWorld.squid1 == 4)
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.PurityFlaskChat");
                        player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<PurityFlask>(), 5);
                        Main.npcChatCornerItem = ModContent.ItemType<PurityFlask>();
                    }

                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
						AANet.SendNetMessage<UpdateLovecraftianCount>((byte)1);
                    }
                    AAWorld.squid1++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else if (Item2 >= 0 && AAWorld.squid2 < 5) //Item 2: 3 Teal Mushrooms
                {
                    Main.npcChatCornerItem = ModContent.ItemType<DragonScale>();
                    player.inventory[Item2].stack--;
                    if (player.inventory[Item2].stack <= 0)
                    {
                        player.inventory[Item2] = new Item();
                    }
                    if (AAWorld.squid2 == 4)
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.AshJarChat");
                        player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<AshJar>(), 5);
                        Main.npcChatCornerItem = ModContent.ItemType<AshJar>();
                    }

                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
						AANet.SendNetMessage<UpdateLovecraftianCount>((byte)2);
                    }
                    AAWorld.squid2++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else if (Item3 >= 0 && AAWorld.squid3 < 5)
                {
                    Main.npcChatCornerItem = ModContent.ItemType<MirePod>();
                    player.inventory[Item3].stack--;
                    if (player.inventory[Item3].stack <= 0)
                    {
                        player.inventory[Item3] = new Item();
                    }
                    if (AAWorld.squid3 == 4)
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.DarkwaterFlaskChat");
                        player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<DarkwaterFlask>(), 5);
                        Main.npcChatCornerItem = ModContent.ItemType<DarkwaterFlask>();
                    }

					if(Main.netMode == NetmodeID.MultiplayerClient)
					{
						AANet.SendNetMessage<UpdateLovecraftianCount>((byte)3);
					}
                    AAWorld.squid3++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else if (Item4 >= 0 && AAWorld.squid4 < 5)
                {
                    Main.npcChatCornerItem = ItemID.RottenChunk;
                    player.inventory[Item4].stack--;
                    if (player.inventory[Item4].stack <= 0)
                    {
                        player.inventory[Item4] = new Item();
                    }
                    if (AAWorld.squid4 == 4)
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.CorruptionFlaskChat");
                        player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<CorruptionFlask>(), 5);
                        Main.npcChatCornerItem = ModContent.ItemType<CorruptionFlask>();
                    }

					if(Main.netMode == NetmodeID.MultiplayerClient)
					{
						AANet.SendNetMessage<UpdateLovecraftianCount>((byte)4);
					}
                    AAWorld.squid4++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else if (Item5 >= 0 && AAWorld.squid5 < 5)
                {
                    Main.npcChatCornerItem = ItemID.Vertebrae;
                    player.inventory[Item5].stack--;
                    if (player.inventory[Item5].stack <= 0)
                    {
                        player.inventory[Item5] = new Item();
                    }
                    if (AAWorld.squid5 == 4)
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.CrimsonFlaskChat");
                        player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<CrimsonFlask>(), 5);
                        Main.npcChatCornerItem = ModContent.ItemType<CrimsonFlask>();
                    }
					if(Main.netMode == NetmodeID.MultiplayerClient)
					{
						AANet.SendNetMessage<UpdateLovecraftianCount>((byte)5);
					}
                    AAWorld.squid5++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else if (Item6 >= 0 && AAWorld.squid6 < 5)
                {
                    Main.npcChatCornerItem = ItemID.PixieDust;
                    player.inventory[Item6].stack--;
                    if (player.inventory[Item6].stack <= 0)
                    {
                        player.inventory[Item6] = new Item();
                    }
                    if (AAWorld.squid6 == 4)
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.HallowFlaskChat");
                        player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<HallowFlask>(), 5);
                        Main.npcChatCornerItem = ModContent.ItemType<HallowFlask>();
                    }
					if(Main.netMode == NetmodeID.MultiplayerClient)
					{
						AANet.SendNetMessage<UpdateLovecraftianCount>((byte)6);
					}
                    AAWorld.squid6++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else if (Item7 >= 0 && AAWorld.squid7 < 5)
                {
                    Main.npcChatCornerItem = ModContent.ItemType<DoomiteScrap>();
                    player.inventory[Item7].stack--;
                    if (player.inventory[Item7].stack <= 0)
                    {
                        player.inventory[Item7] = new Item();
                    }
                    if (AAWorld.squid7 == 4)
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.VoidFlaskChat");
                        player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<VoidFlask>(), 5);
                        Main.npcChatCornerItem = ModContent.ItemType<DoomiteScrap>();
                    }
					if(Main.netMode == NetmodeID.MultiplayerClient)
					{
						AANet.SendNetMessage<UpdateLovecraftianCount>((byte)7);
					}
                    AAWorld.squid7++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else if (Item8 >= 0 && AAWorld.squid8 < 5)
                {
                    Main.npcChatCornerItem = ItemID.JungleSpores;
                    player.inventory[Item8].stack--;
                    if (player.inventory[Item8].stack <= 0)
                    {
                        player.inventory[Item8] = new Item();
                    }
                    if (AAWorld.squid8 == 4)
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.FungicideChat");
                        player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<Fungicide>(), 5);
                        Main.npcChatCornerItem = ModContent.ItemType<Fungicide>();
                    }
					if(Main.netMode == NetmodeID.MultiplayerClient)
					{
						AANet.SendNetMessage<UpdateLovecraftianCount>((byte)8);
					}
                    AAWorld.squid8++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else if (Item9 >= 0 && AAWorld.squid9 < 5 && (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) || (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && Mushman >= 0)))
                {
                    Main.npcChatCornerItem = ModContent.ItemType<Mushium>();
                    player.inventory[Item9].stack--;
                    if (player.inventory[Item9].stack <= 0)
                    {
                        player.inventory[Item9] = new Item();
                    }
                    if (AAWorld.squid9 == 4)
                    {
                        string mushmanName = "";
                        if (Mushman != -1)
                            mushmanName = Main.npc[Mushman].GivenName;
                        else
                            mushmanName = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.SporeSacMushmanNameFallback");

                        Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.SporeSacChat").FormatWith(mushmanName);
                        player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<SporeBag>(), 5);
                        Main.npcChatCornerItem = ModContent.ItemType<SporeBag>();
                    }
					if(Main.netMode == NetmodeID.MultiplayerClient)
					{
						AANet.SendNetMessage<UpdateLovecraftianCount>((byte)9);
					}
                    AAWorld.squid9++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else if (Item10 >= 0 && AAWorld.squid10 < 5 && (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) || (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && Mushman >= 0)))
                {
                    Main.npcChatCornerItem = ModContent.ItemType<GlowingMushium>();
                    player.inventory[Item10].stack--;
                    if (player.inventory[Item10].stack <= 0)
                    {
                        player.inventory[Item10] = new Item();
                    }
                    if (AAWorld.squid10 == 4)
                    {
                        string mushmanName = "";
                        if (Mushman != -1)
                            mushmanName = Main.npc[Mushman].GivenName;
                        else
                            mushmanName = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.GlowingSporeSacMushmanNameFallback");

                        Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.GlowingSporeSacChat").FormatWith(mushmanName);
                        player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<GlowingSporeBag>(), 5);
                        Main.npcChatCornerItem = ModContent.ItemType<GlowingSporeBag>();
                    }
					if(Main.netMode == NetmodeID.MultiplayerClient)
					{
						AANet.SendNetMessage<UpdateLovecraftianCount>((byte)10);
					}
                    AAWorld.squid10++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else if (Item11 >= 0 && AAWorld.squid11 < 5)
                {
                    Main.npcChatCornerItem = ItemID.Stinger;
                    player.inventory[Item11].stack--;
                    if (player.inventory[Item11].stack <= 0)
                    {
                        player.inventory[Item11] = new Item();
                    }
                    if (AAWorld.squid11 == 4)
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.JungleFlaskChat");
                        player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<JungleFlask>(), 5);
                        Main.npcChatCornerItem = ModContent.ItemType<JungleFlask>();
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        AANet.SendNetMessage<UpdateLovecraftianCount>((byte)11);
                    }
                    AAWorld.squid11++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else if (Item12 >= 0 && AAWorld.squid12 < 1)
                {
                    Main.npcChatCornerItem = ItemID.IceMachine;
                    player.inventory[Item12].stack--;
                    if (player.inventory[Item12].stack <= 0)
                    {
                        player.inventory[Item12] = new Item();
                    }
                    if (AAWorld.squid12 == 0)
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.IceFlaskChat");
                        player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<IceFlask>(), 3);
                        player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<IcemeltFlask>(), 3);
                        Main.npcChatCornerItem = ModContent.ItemType<IceFlask>();
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        AANet.SendNetMessage<UpdateLovecraftianCount>((byte)12);
                    }
                    AAWorld.squid12++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else if (Item13 >= 0 && AAWorld.squid13 < 5)
                {
                    Main.npcChatCornerItem = ItemID.Bunny;
                    player.inventory[Item13].stack--;
                    if (player.inventory[Item13].stack <= 0)
                    {
                        player.inventory[Item13] = new Item();
                    }
                    if (AAWorld.squid13 == 4)
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.ForestFlaskChat");
                        player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<ForestFlask>(), 5);
                        Main.npcChatCornerItem = ModContent.ItemType<ForestFlask>();
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        AANet.SendNetMessage<UpdateLovecraftianCount>((byte)13);
                    }
                    AAWorld.squid13++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else
                {
                    if (!BasePlayer.HasItem(player, ModContent.ItemType<LovecraftiansResearchList>()))
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.SquidListChat");
                        int itemID = Item.NewItem(NPC.GetSource_GiftOrReward(), (int)player.position.X, (int)player.position.Y, player.width, player.height, ModContent.ItemType<LovecraftiansResearchList>(), 1, false, 0, false, false);
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemID, 1f, 0f, 0f, 0, 0, 0);
                        }
                    }
                    else
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Lovecraftian.NothingChat");
                    }
                    Main.npcChatCornerItem = 0;
                    SoundEngine.PlaySound(SoundID.MenuTick);
                }
            }
        }

        public override void AddShops()
        {
            NPCShop shop = new(Type);
            shop.Add<PurityFlask>(AAConditions.LovecraftianQuestPurity);
            shop.Add<AshJar>(AAConditions.LovecraftianQuestInferno);
            shop.Add<DarkwaterFlask>(AAConditions.LovecraftianQuestMire);
            shop.Add<CorruptionFlask>(AAConditions.LovecraftianQuestCorruption);
            shop.Add<CrimsonFlask>(AAConditions.LovecraftianQuestCrimson);
            shop.Add<HallowFlask>(AAConditions.LovecraftianQuestHallow);
            shop.Add<VoidFlask>(AAConditions.LovecraftianQuestVoid);
            shop.Add<Fungicide>(AAConditions.LovecraftianQuestAntiMushroom);
            shop.Add<SporeBag>(AAConditions.LovecraftianQuestRedMushroom);
            shop.Add<GlowingSporeBag>(AAConditions.LovecraftianQuestGlowingMushroom);
            shop.Add<JungleFlask>(AAConditions.LovecraftianQuestJungle);
            shop.Add<IceFlask>(AAConditions.LovecraftianQuestIce);
            shop.Add<IcemeltFlask>(AAConditions.LovecraftianQuestAntiIce);
            shop.Add<ForestFlask>(AAConditions.LovecraftianQuestForest);
            shop.Register();
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<Lovecraftian_LovecraftianEye>();
            attackDelay = 1;
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

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {

            multiplier = 4f;

            randomOffset = 2f;

        }
    }
}
