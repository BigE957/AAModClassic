using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAMod.NPCs.TownNPCs
{
    [AutoloadHead]
	public class Lovecraftian : ModNPC
	{
        public override string Texture => "AAMod/NPCs/TownNPCs/Lovecraftian";

        public override bool IsLoadingEnabled(Mod mod)
		{
			name = "Lovecraftian";
			return Mod.Properties/* tModPorter Note: Removed. Instead, assign the properties directly (ContentAutoloadingEnabled, GoreAutoloadingEnabled, MusicAutoloadingEnabled, and BackgroundAutoloadingEnabled) */.Autoload;
		}

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

        public override void HitEffect(NPC.HitInfo hit)
        {
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

        public override List<string> SetNPCNameList()/* tModPorter Suggestion: Return a list of names */
		{
			switch (WorldGen.genRand.Next(4))
			{
				case 0:
					return "Aletheia";
				case 1:
					return "C'thalpa";
				case 2:
					return "D’endrrah";
                case 3:
                    return "Ycnagnnisssz";
                default:
                    return "Yidhra";				
			}
		}
        
        public override string GetChat()
        {   
			Mod Fargos = ModLoader.GetMod("FargoMod");
			Mod GRealm = ModLoader.GetMod("Grealm");

            WeightedRandom<string> chat = new WeightedRandom<string>();


            int Pirate = NPC.FindFirstNPC(NPCID.Pirate);
            int Mutant = Fargos == null ? -1 : NPC.FindFirstNPC(Fargos.Find<ModNPC>("Mutant").Type);
            int HordeZombie = GRealm == null ? -1 : NPC.FindFirstNPC(GRealm.Find<ModNPC>("HordeZombie").Type);

            chat.Add(Lang.TownNPCLovecraftian("LovecraftianChat1"));

            chat.Add(Lang.TownNPCLovecraftian("LovecraftianChat2"));

            chat.Add(Lang.TownNPCLovecraftian("LovecraftianChat3"));

            chat.Add(Lang.TownNPCLovecraftian("LovecraftianChat4"));

            chat.Add(Lang.TownNPCLovecraftian("LovecraftianChat5"));

            chat.Add(Lang.TownNPCLovecraftian("LovecraftianChat6"));

            chat.Add(Lang.TownNPCLovecraftian("LovecraftianChat7"));
            

            //If Pirate is present
            if (Pirate >= 0)
            {
                chat.Add(Lang.TownNPCLovecraftian("LovecraftianChat8") + Main.npc[Pirate].GivenName + Lang.TownNPCLovecraftian("LovecraftianChat9"));
            }

            //If mutant is present

            if (Mutant >= 0)
            {
                chat.Add(Lang.TownNPCLovecraftian("LovecraftianChat10") + Main.npc[Mutant].GivenName + Lang.TownNPCLovecraftian("LovecraftianChat11"));
            }

            //If Horde Zombie is present
            if (HordeZombie >= 0)
            {
                chat.Add(Lang.TownNPCLovecraftian("LovecraftianChat12"));
            }


            //Post - Moon Lord
            if (NPC.downedMoonlord)
            {
                chat.Add(Lang.TownNPCLovecraftian("LovecraftianChat13"));
            }

            //Providing materials

            return chat; 
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Lang.TownNPCLovecraftian("button1");
            button2 = Lang.TownNPCLovecraftian("button2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            Player player = Main.LocalPlayer;
            AAPlayer p = player.GetModPlayer<AAPlayer>();

            if (firstButton)
            {
                shop = true;
            }

            if (!firstButton)
            {
                SoundEngine.PlaySound(SoundID.MenuTick);

                int Mushman = NPC.FindFirstNPC(Mod.Find<ModNPC>("Mushman").Type);

                int Item1 = player.FindItem(ModContent.ItemType<Items.Materials.TerraShard>());
                int Item2 = player.FindItem(ModContent.ItemType<Items.Materials.DragonScale>());
                int Item3 = player.FindItem(ModContent.ItemType<Items.Materials.MirePod>());
                int Item4 = player.FindItem(ItemID.RottenChunk);
                int Item5 = player.FindItem(ItemID.Vertebrae);
                int Item6 = player.FindItem(ItemID.PixieDust);
                int Item7 = player.FindItem(ModContent.ItemType<Items.Materials.DoomiteScrap>());
                int Item8 = player.FindItem(ItemID.JungleSpores);
                int Item9 = player.FindItem(ModContent.ItemType<Items.Boss.MushroomMonarch.Mushium>());
                int Item10 = player.FindItem(ModContent.ItemType<Items.Boss.MushroomMonarch.GlowingMushium>());
                int Item11 = player.FindItem(ItemID.Stinger);
                int Item12 = player.FindItem(ItemID.IceMachine);
                int Item13 = player.FindItem(ItemID.Bunny);

                if (Item1 >= 0 && AAWorld.squid1 < 5) //Item 1: 3 Blueberries
                {
                    Main.npcChatCornerItem = ModContent.ItemType<Items.Materials.TerraShard>();
                    player.inventory[Item1].stack--;
                    if (player.inventory[Item1].stack <= 0)
                    {
                        player.inventory[Item1] = new Item();
                    }
                    if (AAWorld.squid1 == 4)
                    {
                        Main.npcChatText = Lang.TownNPCLovecraftian("PurityFlaskChat");
                        player.QuickSpawnItem(Mod.Find<ModItem>("PurityFlask").Type, 5);
                        Main.npcChatCornerItem = Mod.Find<ModItem>("PurityFlask").Type;
                    }

                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)1);
                    }
                    AAWorld.squid1++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else if (Item2 >= 0 && AAWorld.squid2 < 5) //Item 2: 3 Teal Mushrooms
                {
                    Main.npcChatCornerItem = ModContent.ItemType<Items.Materials.DragonScale>();
                    player.inventory[Item2].stack--;
                    if (player.inventory[Item2].stack <= 0)
                    {
                        player.inventory[Item2] = new Item();
                    }
                    if (AAWorld.squid2 == 4)
                    {
                        Main.npcChatText = Lang.TownNPCLovecraftian("AshJarChat");
                        player.QuickSpawnItem(Mod.Find<ModItem>("AshJar").Type, 5);
                        Main.npcChatCornerItem = Mod.Find<ModItem>("AshJar").Type;
                    }

                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)2);
                    }
                    AAWorld.squid2++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else if (Item3 >= 0 && AAWorld.squid3 < 5)
                {
                    Main.npcChatCornerItem = ModContent.ItemType<Items.Materials.MirePod>();
                    player.inventory[Item3].stack--;
                    if (player.inventory[Item3].stack <= 0)
                    {
                        player.inventory[Item3] = new Item();
                    }
                    if (AAWorld.squid3 == 4)
                    {
                        Main.npcChatText = Lang.TownNPCLovecraftian("DarkwaterFlaskChat");
                        player.QuickSpawnItem(Mod.Find<ModItem>("DarkwaterFlask").Type, 5);
                        Main.npcChatCornerItem = Mod.Find<ModItem>("DarkwaterFlask").Type;
                    }

					if(Main.netMode == NetmodeID.MultiplayerClient)
					{
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)3);
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
                        Main.npcChatText = Lang.TownNPCLovecraftian("CorruptionFlaskChat");
                        player.QuickSpawnItem(Mod.Find<ModItem>("CorruptionFlask").Type, 5);
                        Main.npcChatCornerItem = Mod.Find<ModItem>("CorruptionFlask").Type;
                    }

					if(Main.netMode == NetmodeID.MultiplayerClient)
					{
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)4);
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
                        Main.npcChatText = Lang.TownNPCLovecraftian("CrimsonFlaskChat");
                        player.QuickSpawnItem(Mod.Find<ModItem>("CrimsonFlask").Type, 5);
                        Main.npcChatCornerItem = Mod.Find<ModItem>("CrimsonFlask").Type;
                    }
					if(Main.netMode == NetmodeID.MultiplayerClient)
					{
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)5);
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
                        Main.npcChatText = Lang.TownNPCLovecraftian("HallowFlaskChat");
                        player.QuickSpawnItem(Mod.Find<ModItem>("HallowFlask").Type, 5);
                        Main.npcChatCornerItem = Mod.Find<ModItem>("HallowFlask").Type;
                    }
					if(Main.netMode == NetmodeID.MultiplayerClient)
					{
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)6);
					}
                    AAWorld.squid6++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else if (Item7 >= 0 && AAWorld.squid7 < 5)
                {
                    Main.npcChatCornerItem = ModContent.ItemType<Items.Materials.DoomiteScrap>();
                    player.inventory[Item7].stack--;
                    if (player.inventory[Item7].stack <= 0)
                    {
                        player.inventory[Item7] = new Item();
                    }
                    if (AAWorld.squid7 == 4)
                    {
                        Main.npcChatText = Lang.TownNPCLovecraftian("VoidFlaskChat");
                        player.QuickSpawnItem(Mod.Find<ModItem>("VoidFlask").Type, 5);
                        Main.npcChatCornerItem = Mod.Find<ModItem>("Z").Type;
                    }
					if(Main.netMode == NetmodeID.MultiplayerClient)
					{
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)7);
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
                        Main.npcChatText = Lang.TownNPCLovecraftian("FungicideChat");
                        player.QuickSpawnItem(Mod.Find<ModItem>("Fungicide").Type, 5);
                        Main.npcChatCornerItem = Mod.Find<ModItem>("Fungicide").Type;
                    }
					if(Main.netMode == NetmodeID.MultiplayerClient)
					{
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)8);
					}
                    AAWorld.squid8++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else if (Item9 >= 0 && AAWorld.squid9 < 5 && Mushman >= 0)
                {
                    Main.npcChatCornerItem = ModContent.ItemType<Items.Boss.MushroomMonarch.Mushium>();
                    player.inventory[Item9].stack--;
                    if (player.inventory[Item9].stack <= 0)
                    {
                        player.inventory[Item9] = new Item();
                    }
                    if (AAWorld.squid9 == 4)
                    {
                        Main.npcChatText = Lang.TownNPCLovecraftian("SporeSacChat1") + Main.npc[Mushman].GivenName + Lang.TownNPCLovecraftian("SporeSacChat2");
                        player.QuickSpawnItem(Mod.Find<ModItem>("SporeSac").Type, 5);
                        Main.npcChatCornerItem = Mod.Find<ModItem>("SporeSac").Type;
                    }
					if(Main.netMode == NetmodeID.MultiplayerClient)
					{
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)9);
					}
                    AAWorld.squid9++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else if (Item10 >= 0 && AAWorld.squid10 < 5 && Mushman >= 0)
                {
                    Main.npcChatCornerItem = ModContent.ItemType<Items.Boss.MushroomMonarch.GlowingMushium>();
                    player.inventory[Item10].stack--;
                    if (player.inventory[Item10].stack <= 0)
                    {
                        player.inventory[Item10] = new Item();
                    }
                    if (AAWorld.squid10 == 4)
                    {
                        Main.npcChatText = Lang.TownNPCLovecraftian("GlowingSporeSacChat1") + Main.npc[Mushman].GivenName + Lang.TownNPCLovecraftian("GlowingSporeSacChat2");
                        player.QuickSpawnItem(Mod.Find<ModItem>("GlowingSporeSac").Type, 5);
                        Main.npcChatCornerItem = Mod.Find<ModItem>("GlowingSporeSac").Type;
                    }
					if(Main.netMode == NetmodeID.MultiplayerClient)
					{
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)10);
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
                        Main.npcChatText = Lang.TownNPCLovecraftian("JungleFlaskChat");
                        player.QuickSpawnItem(Mod.Find<ModItem>("JungleFlask").Type, 5);
                        Main.npcChatCornerItem = Mod.Find<ModItem>("JungleFlask").Type;
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)11);
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
                        Main.npcChatText = Lang.TownNPCLovecraftian("IceFlaskChat");
                        player.QuickSpawnItem(Mod.Find<ModItem>("IceFlask").Type, 3);
                        player.QuickSpawnItem(Mod.Find<ModItem>("IcemeltFlask").Type, 3);
                        Main.npcChatCornerItem = Mod.Find<ModItem>("IceFlask").Type;
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)12);
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
                        Main.npcChatText = Lang.TownNPCLovecraftian("ForestFlaskChat");
                        player.QuickSpawnItem(Mod.Find<ModItem>("ForestFlask").Type, 5);
                        Main.npcChatCornerItem = Mod.Find<ModItem>("ForestFlask").Type;
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)13);
                    }
                    AAWorld.squid13++;
                    SoundEngine.PlaySound(SoundID.Chat);
                }
                else
                {
                    if (!BasePlayer.HasItem(player, ModContent.ItemType<Items.Flasks.SquidList>()))
                    {
                        Main.npcChatText = Lang.TownNPCLovecraftian("SquidListChat");
                        int itemID = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("SquidList").Type, 1, false, 0, false, false);
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemID, 1f, 0f, 0f, 0, 0, 0);
                        }
                    }
                    else
                    {
                        Main.npcChatText = Lang.TownNPCLovecraftian("NothingChat");
                    }
                    Main.npcChatCornerItem = 0;
                    SoundEngine.PlaySound(SoundID.MenuTick);
                }
            }
        }


        public override void ModifyActiveShop(string shopName, Item[] items)
        {
            if (AAWorld.squid1 >= 5)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Flasks.PurityFlask>());
                nextSlot++;
            }
            if (AAWorld.squid2 >= 5)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Flasks.AshJar>());
                nextSlot++;
            }
            if (AAWorld.squid3 >= 5)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Flasks.DarkwaterFlask>());
                nextSlot++;
            }
            if (AAWorld.squid4 >= 5)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Flasks.CorruptionFlask>());
                nextSlot++;
            }
            if (AAWorld.squid5 >= 5)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Flasks.CrimsonFlask>());
                nextSlot++;
            }
            if (AAWorld.squid6 >= 5)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Flasks.HallowFlask>());
                nextSlot++;
            }
            if (AAWorld.squid7 >= 5)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Flasks.VoidFlask>());
                nextSlot++;
            }
            if (AAWorld.squid8 >= 5)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Flasks.Fungicide>());
                nextSlot++;
            }
            if (AAWorld.squid9 >= 5)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Usable.SporeSac>());
                nextSlot++;
            }
            if (AAWorld.squid10 >= 5)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Flasks.GlowingSporeSac>());
                nextSlot++;
            }
            if (AAWorld.squid11 >= 5)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Flasks.JungleFlask>());
                nextSlot++;
            }
            if (AAWorld.squid12 >= 1)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Flasks.IceFlask>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Flasks.IcemeltFlask>());
                nextSlot++;
            }
            if (AAWorld.squid13 >= 5)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Flasks.ForestFlask>());
                nextSlot++;
            }
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<EyeShot>();
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
