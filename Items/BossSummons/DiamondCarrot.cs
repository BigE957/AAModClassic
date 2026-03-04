using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.NPCs.Bosses.Rajah;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Items.BossSummons
{
    public class DiamondCarrot : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ten Carat Carrot");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"The fury of the Raging Rajah can be felt radiating from this ornate carrot...
Non-consumable"); */
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 14;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noUseGraphic = true;
            Item.consumable = false;
            Item.UseSound = new SoundStyle("AAModClassic/Sounds/Sounds/Rajah");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity14;
                }
            }
        }

        public override bool CanUseItem(Player player)
        {
            return !(NPC.AnyNPCs(ModContent.NPCType<Rajah>()) ||
                NPC.AnyNPCs(ModContent.NPCType<SupremeRajah>()));
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (!AAWorld.downedRajahsRevenge)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DiamondCarrotRajahText1"), 107, 137, 179);
            }
            else
            {
                string Name;
                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    Name = "Terrarians";
                }
                else
                {
                    Name = Main.LocalPlayer.name;
                }
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DiamondCarrotRajahText2") + Name + "!", 107, 137, 179);
            }
            int overrideDirection = Main.rand.Next(2) == 0 ? -1 : 1;
            AAModGlobalNPC.SpawnBoss(player, Mod.Find<ModNPC>("SupremeRajah").Type, false, player.Center + new Vector2(MathHelper.Lerp(500f, 800f, (float)Main.rand.NextDouble()) * overrideDirection, -1200), Language.GetTextValue("Mods.AAMod.Common.SupremeRajah"));
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "GoldenCarrot", 1);
            recipe.AddIngredient(null, "UnstableSingularity", 3);
            recipe.AddIngredient(null, "CrucibleScale", 3);
            recipe.AddIngredient(null, "DreadScale", 3);
            recipe.AddIngredient(ItemID.Diamond, 5);
            recipe.AddTile(null, "ACS");
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "PlatinumCarrot", 1);
            recipe.AddIngredient(null, "UnstableSingularity", 3);
            recipe.AddIngredient(null, "CrucibleScale", 3);
            recipe.AddIngredient(null, "DreadScale", 3);
            recipe.AddIngredient(ItemID.Diamond, 5);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}