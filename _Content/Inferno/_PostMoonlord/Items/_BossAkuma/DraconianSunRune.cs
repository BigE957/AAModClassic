using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;

using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using AAModClassic.CrossMod;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs._BossShen;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma
{
    public class DraconianSunRune : BaseAAItem
    {
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

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianDayTimeFalse"), new Color(180, 41, 32), false);
                return false;
            }
            if (player.ZoneAnyInferno())
            {
                if (!ContentReplacementSystem.NeedToReplaceContent && !player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda && !AAWorld.downedYamata)
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianRuneFalse1"), Color.Indigo, false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<Akuma>()))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianRuneFalse2"), new Color(180, 41, 32), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<AkumaA>()))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianRuneFalse2"), new Color(0, 191, 255), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<Shen>()) || NPC.AnyNPCs(ModContent.NPCType<ShenA>()) || NPC.AnyNPCs(ModContent.NPCType<ShenSpawn>()) ||
                    NPC.AnyNPCs(ModContent.NPCType<ShenTransition>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDeath>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDefeat>()))
                {
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<AkumaTransition>()))
                {
                    return false;
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianRuneInfernoFalse"), new Color(180, 41, 32), false);
            return false;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianRuneTrue1"), new Color(175, 75, 255));
            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianRuneTrue2"), Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<AkumaA>(), false, 0, 0, Language.GetTextValue("Mods.AAModClassic.Common.AkumaA"), false);
            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/AkumaRoar"), player.position);
            return true;
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int bossType = Mod.Find<ModNPC>(name).Type;
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-2000, 2000, (float)Main.rand.NextDouble()), 1200f);
                Main.npc[npcID].netUpdate2 = true;
            }
        }
    }
}