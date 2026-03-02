using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;
using AAMod.NPCs.Bosses.Akuma;
using AAMod.NPCs.Bosses.Akuma.Awakened;
using System.Collections.Generic;

using Terraria.ID;

namespace AAMod.Items.BossSummons
{
    public class DraconianSigil : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Draconian Sun Sigil");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"An ornate tablet said to contain the radiant power of a thousand suns
Summons Akuma
Only Usable during the day in the inferno
Non-Consumable"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 28;
            Item.rare = 2;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
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


        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DraconianDayTimeFalse"), new Color(180, 41, 32), false);
                return false;
            }
            if (player.GetModPlayer<AAPlayer>().ZoneInferno)
            {
                if (!AAWorld.downedAkuma && !player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda)
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DraconianRuneFalse2"), new Color(180, 41, 32), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<Akuma>()))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DraconianSigilFalse"), new Color(180, 41, 32), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<AkumaA>()))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DraconianSigilFalse"), new Color(0, 191, 255), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.Shen>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenA>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenSpawn>()) ||
                    NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenTransition>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenDeath>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenDefeat>()))
                {
                    return false;
                }
                if (NPC.AnyNPCs(Mod.Find<ModNPC>("AkumaTransition").Type))
                {
                    return false;
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DraconianSigilInfernoFalse"), new Color(180, 41, 32), false);
            return false;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {

            if (!AAWorld.downedAkuma)
            {
                if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DraconianSignalTrue1"), new Color(180, 41, 32));
            }
            if (AAWorld.downedAkuma)
            {
                if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DraconianSignalTrue2"), new Color(180, 41, 32));
            }
            AAModGlobalNPC.SpawnBoss(player, Mod.Find<ModNPC>("Akuma").Type, true, 0, 0, Language.GetTextValue("Mods.AAMod.Common.Akuma"), false);
            SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/AkumaRoar"), player.position);
            return true;
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = Mod.Find<ModNPC>(name).Type;
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-2000, 2000, (float)Main.rand.NextDouble()), 1200f);
                Main.npc[npcID].netUpdate2 = true;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "DaybreakIncinerite", 10);
            recipe.AddIngredient(null, "RadiumBar", 5);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
    }
}