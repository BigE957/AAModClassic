using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Void._PostMoonlord.Items.Materials
{
    public class ApocalyptitePlate : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 30;
            Item.maxStack = 99;
			Item.value = Terraria.Item.sellPrice(0, 3, 0, 0);
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Apocalyptite Plate");
            // Tooltip.SetDefault("A forboding energy rings from this metal plating");
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
        public override void AddRecipes()
        {                                                   
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ApocalyptiteOre>(), 5);              //example of how to craft with a modded item
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}
