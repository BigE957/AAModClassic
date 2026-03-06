using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Potions
{
    public class TheBigOne : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Big One");
		}
		
		public override void SetDefaults()
		{
            Item.rare = ItemRarityID.Purple;
			Item.width = 20;
			Item.height = 38;
			Item.useTurn = true;
			Item.maxStack = 50;
			Item.healLife = 600;
            Item.healMana = 600;
            Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.potion = true;
			Item.value = 100000;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(216, 110, 40);
	            }
	        }
	    }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "GrandHealingPotion");
            recipe.AddIngredient(null, "GrandManaPotion");
            recipe.AddRecipeGroup("AAModClassic:SuperAncientMaterials");
            recipe.AddTile(null, "ACS");
			recipe.Register();
		}
	}
}