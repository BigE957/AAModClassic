using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._CrossMod;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata
{
    public class DreadMoonSigil : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.BossSummon";

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

        

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<DarkmatterBar>(), 5);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
            SpawnBoss(player, ModContent.NPCType<YamataBody>(), true, new Vector2(player.Center.X, player.Center.Y - 100),  Language.GetTextValue("Mods.AAModClassic.Common.Yamata"));
            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/YamataRoar"), player.position);
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
            if (player.ZoneAnyMire())
			{
                if (!ContentReplacementSystem.NeedToReplaceContent && !AAWorld.downedYamata && !player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake)
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadSigilMireFalse"), new Color(45, 46, 70), false);
                    return false;
                }
				if (NPC.AnyNPCs(ModContent.NPCType<YamataBody>()))
				{
					if(player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadFalse2"), new Color(45, 46, 70), false);
					return false;
				}
                if (NPC.AnyNPCs(ModContent.NPCType<YamataABody>()))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadFalse2"), new Color(146, 30, 68), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<ShenDoragon>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDoragonA>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDoragonSpawn>()) ||
                    NPC.AnyNPCs(ModContent.NPCType<ShenDoragonTransition>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDoragonDeath>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDoragonDefeat>()))
                {
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<YamataTransition>()))
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
                AANet.SendNetMessage<SummonNPCFromClient>((byte)player.whoAmI, (short)bossType, spawnMessage, (int)npcCenter.X, (int)npcCenter.Y, overrideDisplayName, namePlural);
            }
        }

    }
}