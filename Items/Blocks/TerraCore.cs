using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class TerraCore : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Core of Terraria");
            /* Tooltip.SetDefault(@"Combines most crafting stations into one
Used to create ancient crafting stations"); */
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 36;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Purple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 1000000;
            Item.createTile = Mod.Find<ModTile>("TerraCore").Type;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }   

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("AAMod:AstralStations", 1);
            recipe.AddIngredient(null, "TruePaladinsSmeltery", 1);
            recipe.Register();
        }
    }
}
