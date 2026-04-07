using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;

using Terraria.ID;
using AAModClassic.NPCs.Bosses.Akuma.Awakened;
using AAModClassic;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.NPCs.Bosses.Akuma;
using AAModClassic.NPCs.Bosses.Shen;
using AAModClassic.Globals;
using AAModClassic.___Content.Mire._PostMoonlord.NPCs._BossYamata;
using AAModClassic.___Content.Mire._PostMoonlord.NPCs._BossYamata.Awakened;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Items.Materials;
using AAModClassic.___Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic.Items.Boss.Akuma;
using AAModClassic.___Content.Mire._PostMoonlord.Items._BossYamata;

namespace AAModClassic.Items.BossSummons
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
                    line2.OverrideColor = new Color(176, 39, 157);
                }
            }
        }


        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (!AAWorld.downedAkuma || !AAWorld.downedYamata)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ChaosSigilFalse5"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Yamata>()) || NPC.AnyNPCs(ModContent.NPCType<YamataA>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ChaosSigilFalse1"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Akuma>()) || NPC.AnyNPCs(ModContent.NPCType<AkumaA>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ChaosSigilFalse2"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Shen>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ChaosSigilFalse3"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<ShenA>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ChaosSigilFalse3"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (!AAWorld.downedShen && !player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda && !player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ChaosSigilFalse4"), Color.DarkMagenta, false);
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
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(AAWorld.downedShen ? Language.GetTextValue("Mods.AAModClassic.Common.ChaosSigilTrue1") : Language.GetTextValue("Mods.AAModClassic.Common.ChaosSigilTrue2"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);

                AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<Shen>(), true, 0, 0, Language.GetTextValue("Mods.AAModClassic.Common.ShenDoragon"), false);
            }
            if (!AAWorld.ShenSummoned)
            {
                AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<ShenSpawn>(), false, 0, 0);
                AAWorld.ShenSummoned = true;
            }

            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Sounds/ShenRoar"), player.position);
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<DraconianSigil>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DreadSigil>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<Discordium>(), 10);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}
