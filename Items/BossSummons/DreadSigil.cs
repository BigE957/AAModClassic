using AAModClassic;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.NPCs.Bosses.Shen;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Items.BossSummons
{
    public class DreadSigil : BaseAAItem
	{

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dread Moon Sigil");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"A ragged old tablet said to contain the dark magic of a new moon
Summons Yamata
Can only be used at night in the mire
Non-Consumable"); */
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 500;
            Item.consumable = false;
            Item.rare = ItemRarityID.Red;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "EventideAbyssium", 10);
            recipe.AddIngredient(null, "DarkMatter", 5);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
            SpawnBoss(player, ModContent.NPCType<NPCs.Bosses.Yamata.Yamata>(), true, new Vector2(player.Center.X, player.Center.Y - 100),  Language.GetTextValue("Mods.AAModClassic.Common.Yamata"));
            SoundEngine.PlaySound(new SoundStyle("Sounds/Sounds/YamataRoar"), player.position);
            if (!AAWorld.downedYamata)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadSigilTrue1"), new Color(45, 46, 70));
            }
            if (AAWorld.downedYamata)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadSigilTrue2"), new Color(45, 46, 70));
            }

            return true;
		}

		public override bool CanUseItem(Player player)
		{
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadTimeFalse"), new Color(45, 46, 70), false);
                return false;
            }
            if (player.GetModPlayer<AAPlayer>().ZoneMire)
			{
                if (!AAWorld.downedYamata && !player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake)
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadSigilMireFalse"), new Color(45, 46, 70), false);
                    return false;
                }
				if (NPC.AnyNPCs(Mod.Find<ModNPC>("Yamata").Type))
				{
					if(player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadFalse2"), new Color(45, 46, 70), false);
					return false;
				}
                if (NPC.AnyNPCs(Mod.Find<ModNPC>("YamataA").Type))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadFalse2"), new Color(146, 30, 68), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.Shen>()) || NPC.AnyNPCs(ModContent.NPCType<ShenA>()) || NPC.AnyNPCs(ModContent.NPCType<ShenSpawn>()) ||
                    NPC.AnyNPCs(ModContent.NPCType<ShenTransition>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDeath>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDefeat>()))
                {
                    return false;
                }
                if (NPC.AnyNPCs(Mod.Find<ModNPC>("YamataTransition").Type))
                {
                    return false;
                }
                return true;
			}
			if(player.whoAmI == Main.myPlayer) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadMireFalse"), new Color(45, 46, 70), false);			
			return false;
		}

        public static void SpawnBoss(Player player, int bossType, bool spawnMessage = true, Vector2 npcCenter = default, string overrideDisplayName = "", bool namePlural = false)
        {
            if (npcCenter == default)
                npcCenter = player.Center;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.AnyNPCs(bossType)) { return; }
                int npcID = NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)npcCenter.X, (int)npcCenter.Y, bossType, 0);
                Main.npc[npcID].Center = npcCenter;
                Main.npc[npcID].netUpdate2 = true;
                if (spawnMessage)
                {
                    string npcName = !string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : overrideDisplayName;
                    if ((npcName == null || npcName.Equals("")) && Main.npc[npcID].ModNPC != null)
                        npcName = Main.npc[npcID].ModNPC.DisplayName.ToString();
                    if (namePlural)
                    {
                        if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(npcName + " " + Language.GetTextValue("Mods.AAModClassic.Common.BosshasAwoken"), 175, 75, 255, false); }
                        else
                        if (Main.netMode == NetmodeID.Server)
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(npcName + " " + Language.GetTextValue("Mods.AAModClassic.Common.BosshasAwoken")), new Color(175, 75, 255), -1);
                        }
                    }
                    else
                    {
                        if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false); }
                        else
                        if (Main.netMode == NetmodeID.Server)
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
                            {
                            NetworkText.FromLiteral(npcName)
                            }), new Color(175, 75, 255), -1);
                        }
                    }
                }
            }
            else
            {
                //I have no idea how to convert this to the standard system so im gonna post this method too lol
                AANet.SendNetMessage(AANet.SummonNPCFromClient, (byte)player.whoAmI, (short)bossType, spawnMessage, (int)npcCenter.X, (int)npcCenter.Y, overrideDisplayName, namePlural);
            }
        }

    }
}