using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;

using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._CrossMod;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon
{
    public class ChaosSigil : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.BossSummon";
        
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
            if (NPC.AnyNPCs(ModContent.NPCType<YamataBody>()) || NPC.AnyNPCs(ModContent.NPCType<YamataABody>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ChaosSigilFalse1"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<AkumaHead>()) || NPC.AnyNPCs(ModContent.NPCType<AkumaAHead>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ChaosSigilFalse2"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<ShenDoragon>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ChaosSigilFalse3"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<ShenDoragonA>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ChaosSigilFalse3"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (!ContentReplacementSystem.NeedToReplaceContent && !AAWorld.downedShen && !player.GetModPlayer<ZAAPlayer>().ZoneRisingSunPagoda && !player.GetModPlayer<ZAAPlayer>().ZoneRisingMoonLake)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ChaosSigilFalse4"), Color.DarkMagenta, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<ShenDoragonSpawn>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDoragonTransition>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDoragonDefeat>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDoragonDeath>()))
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

                AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<ShenDoragon>(), true, 0, 0, Language.GetTextValue("Mods.AAModClassic.Common.ShenDoragon"), false);
            }
            else
            {
                AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<ShenDoragonSpawn>(), false, 0, 0);
                AAWorld.ShenSummoned = true;
            }

            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/ShenRoar"), player.position);
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<DraconianSunSigil>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DreadMoonSigil>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DiscordiumBar>(), 10);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}
