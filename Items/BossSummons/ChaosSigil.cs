using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;
using AAMod.NPCs.Bosses.Shen;
using System.Collections.Generic;

using Terraria.ID;

namespace AAMod.Items.BossSummons
{
    public class ChaosSigil : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Sigil");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"A cursed tablet filled with unstable magic
Summons the chaos emperor
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
                    line2.OverrideColor = new Color(176, 39, 157);
                }
            }
        }


        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (!AAWorld.downedAkuma || !AAWorld.downedYamata)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ChaosSigilFalse5"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Yamata.Yamata>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Yamata.Awakened.YamataA>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ChaosSigilFalse1"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Akuma.Akuma>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Akuma.Awakened.AkumaA>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ChaosSigilFalse2"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Shen>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ChaosSigilFalse3"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<ShenA>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ChaosSigilFalse3"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (!AAWorld.downedShen && !player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda && !player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ChaosSigilFalse4"), Color.DarkMagenta, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<ShenSpawn>()) || NPC.AnyNPCs(ModContent.NPCType<ShenTransition>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDefeat>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDeath>()))
            {
                return false;
            }
            return true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (AAWorld.ShenSummoned)
            {
                if (Main.netMode != 1) BaseUtility.Chat(AAWorld.downedShen ? Language.GetTextValue("Mods.AAMod.Common.ChaosSigilTrue1") : Language.GetTextValue("Mods.AAMod.Common.ChaosSigilTrue2"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);

                AAModGlobalNPC.SpawnBoss(player, Mod.Find<ModNPC>("Shen").Type, true, 0, 0, Language.GetTextValue("Mods.AAMod.Common.ShenDoragon"), false);
            }
            if (!AAWorld.ShenSummoned)
            {
                AAModGlobalNPC.SpawnBoss(player, Mod.Find<ModNPC>("ShenSpawn").Type, false, 0, 0);
                AAWorld.ShenSummoned = true;
            }

            SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/ShenRoar"), player.position);
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "DraconianSigil", 1);
            recipe.AddIngredient(null, "DreadSigil", 1);
            recipe.AddIngredient(null, "CrucibleScale", 5);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(null, "Discordium", 10);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}
