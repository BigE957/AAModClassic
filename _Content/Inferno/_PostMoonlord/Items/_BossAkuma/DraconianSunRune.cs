using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
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

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma
{
    public class DraconianSunRune : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.BossSummon";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Draconian Sun Rune");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"An enchanted tablet bursting with flaming chaotic energy
Summons Akuma Awakened
Only Usable during the day in the inferno
Non-Consumable"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 28;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(0, 191, 255);
                }
            }
        }

        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime)
            {
                if (!Main.dedServ && player.whoAmI == Main.myPlayer)
                {
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        WorldTextSystem.StartDialogueOnClient("Mods.AAModClassic.WorldText.Akuma.Spawn", Main.LocalPlayer.Center, 0, 60, false, new BossText());
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianDayTimeFalse"), new Color(180, 41, 32), false);
                }
                return false;
            }
            if (!player.ZoneAnyInferno())
            {
                if (!Main.dedServ && player.whoAmI == Main.myPlayer)
                {
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        WorldTextSystem.StartDialogueOnClient("Mods.AAModClassic.WorldText.Akuma.Spawn", Main.LocalPlayer.Center, 2, 60, false, new BossText());
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianRuneInfernoFalse"), new Color(180, 41, 32), false);
                }
                return false;
            }

            bool anyActiveAkuma = false;
            bool anyBlockingEnemy = false;
            bool isAwakened = false;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.type == ModContent.NPCType<AkumaAHead>())
                {
                    anyActiveAkuma = true;
                    isAwakened = true;
                    break;
                }

                if (npc.type == ModContent.NPCType<AkumaHead>())
                {
                    anyActiveAkuma = true;
                    isAwakened = false;
                    break;
                }

                if (npc.type == ModContent.NPCType<ShenDoragon>() || npc.type == ModContent.NPCType<ShenDoragonA>() ||
                    npc.type == ModContent.NPCType<ShenDoragonSpawn>() || npc.type == ModContent.NPCType<ShenDoragonTransition>() || npc.type == ModContent.NPCType<ShenDoragonDeath>() || npc.type == ModContent.NPCType<ShenDoragonDefeat>() ||
                    npc.type == ModContent.NPCType<AkumaTransition>())
                    anyBlockingEnemy = true;
            }

            if (anyActiveAkuma)
            {
                if (!Main.dedServ && player.whoAmI == Main.myPlayer)
                {
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        WorldTextSystem.StartDialogueOnClient("Mods.AAModClassic.WorldText.Akuma.Spawn", Main.LocalPlayer.Center, isAwakened ? 1 : 5, 60, false, new BossText());
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianRuneFalse2"), isAwakened ? Color.DeepSkyBlue : new Color(180, 41, 32), false);
                }
                return false;
            }

            if (anyBlockingEnemy)
                return false;

            return true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianRuneTrue1"), new Color(175, 75, 255));
                if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                    WorldTextSystem.StartDialogueOnClient("Mods.AAModClassic.WorldText.Akuma.Spawn", Main.LocalPlayer.Center, 4, 60, false, new BossText());
                else
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianRuneTrue2"), Color.DeepSkyBlue);
            }
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<AkumaAHead>(), false);
            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/AkumaRoar"), player.position);
            return true;
        }
    }
}