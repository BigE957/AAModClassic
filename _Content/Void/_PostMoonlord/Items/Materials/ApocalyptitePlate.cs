using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items.Materials
{
    public class ApocalyptitePlate : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Apocalyptite Plate");
            // Tooltip.SetDefault("A forboding energy rings from this metal plating");
            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 30;
            Item.maxStack = Item.CommonMaxStack;
			Item.value = Terraria.Item.sellPrice(0, 3, 0, 0);
        }

        
        public override void AddRecipes()
        {                                                   
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ApocalyptiteOre>(), 5);              //example of how to craft with a modded item
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}
