using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using AAModClassic.NPCs.Bosses.AH;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.NPCs.Bosses.AH.Haruka;
using AAModClassic.NPCs.Bosses.AH.Ashe;

namespace AAModClassic.Items.BossSummons
{
    public class FlamesOfAnarchy : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flames of Anarchy");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"The flames of chaos burn in this antique china
Calls upon the Sisters of Discord
Non-Consumable"); */
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 4));
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 46;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ItemRarityID.Purple;
            Item.noUseGraphic = true;
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<Ashe>()) && !NPC.AnyNPCs(ModContent.NPCType<Haruka>()) && !NPC.AnyNPCs(ModContent.NPCType<AHSpawn>());
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            SoundEngine.PlaySound(SoundID.Roar, player.position);

            if (AAWorld.SistersSummoned && !AAWorld.downedSisters)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.SistersDownedInfo1"), new Color(102, 20, 48));

                AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<Ashe>(), false, -1, 0, "Ashe Akuma", false);

                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.SistersDownedInfo2"), new Color(72, 78, 117));
                AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<Haruka>(), false, 1, 0, "Haruka Yamata", false);
                return true;
            }
            else if (AAWorld.SistersSummoned && AAWorld.downedSisters)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.SistersInfo1"), new Color(72, 78, 117));

                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.SistersInfo2"), new Color(102, 20, 48));
                AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<Ashe>(), false, -1, 0, "Ashe Akuma", false);
                AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<Haruka>(), false, 1, 0, "Haruka Yamata", false);
                return true;
            }
            else
            {
                AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<AHSpawn>(), false, 0, 0);
                AAWorld.SistersSummoned = true;
                return true;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "RadiantIncinerite", 10);
            recipe.AddIngredient(null, "DeepAbyssium", 10);
            recipe.AddIngredient(null, "DragonFire", 5);
            recipe.AddIngredient(null, "HydraToxin", 5);
            recipe.AddIngredient(null, "SoulOfSmite", 5);
            recipe.AddIngredient(null, "SoulOfSpite", 5);
            recipe.AddIngredient(null, "SearingSpark", 3);
            recipe.AddIngredient(null, "TerrorSoul", 3);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
    }
}