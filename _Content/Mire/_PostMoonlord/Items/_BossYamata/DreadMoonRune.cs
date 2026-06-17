using System.Collections.Generic;

using Microsoft.Xna.Framework;

//using AAModClassic.NPCs.Bosses.Infinity;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

using Terraria.Localization;
using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic._CrossMod;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata
{
    public class DreadMoonRune : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.BossSummon";

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
            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadRuneTrue1"), Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadRuneTrue2"), new Color(146, 30, 68));
            DreadMoonSigil.SpawnBoss(player, ModContent.NPCType<YamataABody>(), false, new Vector2(player.Center.X, player.Center.Y - 100), Language.GetTextValue("Mods.AAModClassic.Common.YamataA"));
            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/YamataRoar"), player.position);
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
                if (!ContentReplacementSystem.NeedToReplaceContent && !player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake && !AAWorld.downedYamata)
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadFalse1"), Color.Indigo, false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<YamataBody>()))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadFalse2"), new Color(45, 46, 70), false);
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
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadMireFalse"), new Color(45, 46, 70), false);
            return false;
        }
		
	}
}