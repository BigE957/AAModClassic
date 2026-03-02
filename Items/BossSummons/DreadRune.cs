using System.Collections.Generic;

using Microsoft.Xna.Framework;

//using AAMod.NPCs.Bosses.Infinity;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

using Terraria.Localization;
using Terraria.ID;

namespace AAMod.Items.BossSummons
{
    public class DreadRune : BaseAAItem
	{

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dread Moon Rune");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"An enchanted tablet radiating dark chaotic energy
Summons Yamata no Orochi
Can only be used in the mire at night
Non-Consumable"); */
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(146, 30, 68);
                }
            }
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadRuneTrue1"), Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadRuneTrue2"), new Color(146, 30, 68));
            DreadSigil.SpawnBoss(player, ModContent.NPCType<NPCs.Bosses.Yamata.Awakened.YamataA>(), false, new Vector2(player.Center.X, player.Center.Y - 100), Language.GetTextValue("Mods.AAMod.Common.YamataA"));
            SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/YamataRoar"), player.position);
            return true;
		}

        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadTimeFalse"), new Color(45, 46, 70), false);
                return false;
            }
            if (player.GetModPlayer<AAPlayer>().ZoneMire)
            {
                if (!player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake && !AAWorld.downedYamata)
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadFalse1"), Color.Indigo, false);
                    return false;
                }
                if (NPC.AnyNPCs(Mod.Find<ModNPC>("Yamata").Type))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadFalse2"), new Color(45, 46, 70), false);
                    return false;
                }
                if (NPC.AnyNPCs(Mod.Find<ModNPC>("YamataA").Type))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadFalse2"), new Color(146, 30, 68), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.Shen>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenA>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenSpawn>()) || 
                    NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenTransition>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenDeath>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenDefeat>()))
                {
                    return false;
                }
                if (NPC.AnyNPCs(Mod.Find<ModNPC>("YamataTransition").Type))
                {
                    return false;
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadMireFalse"), new Color(45, 46, 70), false);
            return false;
        }
		
	}
}