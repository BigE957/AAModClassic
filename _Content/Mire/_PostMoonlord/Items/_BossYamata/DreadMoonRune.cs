using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic._CrossMod;
using AAModClassic.Base;
using AAModClassic.Globals;
using AAModClassic.UI.Dialogue;
using AAModClassic.UI.Dialogue.DisplayEffects;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

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
            if (Main.netMode != NetmodeID.MultiplayerClient)
                
            if (Main.netMode != NetmodeID.MultiplayerClient)
                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadRuneTrue2"), new Color(146, 30, 68));
            DreadMoonSigil.SpawnBoss(player, ModContent.NPCType<YamataABody>(), false, new Vector2(player.Center.X, player.Center.Y - 100), Language.GetTextValue("Mods.AAModClassic.Common.YamataA"));
            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/YamataRoar"), player.position);
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadRuneTrue1"), Color.Magenta);
                if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                    WorldTextSystem.StartDialogue("Mods.AAModClassic.WorldText.Yamata.Spawn", player.Center, 7, 30, false, new BossText());
                else
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadRuneTrue2"), new Color(146, 30, 68));
            }
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime)
            {
                if (!Main.dedServ && player.whoAmI == Main.myPlayer)
                {
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        WorldTextSystem.StartDialogueOnClient("Mods.AAModClassic.WorldText.Yamata.Spawn", Main.LocalPlayer.Center, 0, 30, false, new BossText());
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadTimeFalse"), AAColor.YamataDialogue, false);
                }
                return false;
            }

            if (!player.ZoneAnyMire())
            {
                if (!Main.dedServ && player.whoAmI == Main.myPlayer)
                {
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        WorldTextSystem.StartDialogueOnClient("Mods.AAModClassic.WorldText.Yamata.Spawn", Main.LocalPlayer.Center, 1, 30, false, new BossText());
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadMireFalse"), AAColor.YamataDialogue, false);
                }
                return false;
            }

            bool anyActiveYamata = false;
            bool anyBlockingEnemy = false;
            bool isAwakened = false;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.type == ModContent.NPCType<YamataABody>())
                {
                    anyActiveYamata = true;
                    isAwakened = true;
                    break;
                }

                if (npc.type == ModContent.NPCType<YamataBody>())
                {
                    anyActiveYamata = true;
                    isAwakened = false;
                    break;
                }

                if (npc.type == ModContent.NPCType<ShenDoragon>() || npc.type == ModContent.NPCType<ShenDoragonA>() ||
                    npc.type == ModContent.NPCType<ShenDoragonSpawn>() || npc.type == ModContent.NPCType<ShenDoragonTransition>() || npc.type == ModContent.NPCType<ShenDoragonDeath>() || npc.type == ModContent.NPCType<ShenDoragonDefeat>() ||
                    npc.type == ModContent.NPCType<YamataTransition>())
                    anyBlockingEnemy = true;
            }

            if (anyActiveYamata)
            {
                if (!Main.dedServ && player.whoAmI == Main.myPlayer)
                {
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        WorldTextSystem.StartDialogueOnClient("Mods.AAModClassic.WorldText.Yamata.Spawn", Main.LocalPlayer.Center, isAwakened ? 4 : 3, 30, false, new BossText());
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadFalse2"), isAwakened ? new Color(146, 30, 68) : AAColor.YamataDialogue, false);
                }
                return false;
            }

            if (anyBlockingEnemy)
                return false;

            return true;
        }
    }
}