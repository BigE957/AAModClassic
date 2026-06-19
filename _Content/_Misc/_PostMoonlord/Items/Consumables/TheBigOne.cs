using System.Collections.Generic;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Misc._PostMoonlord.Items.Consumables
{
    public class TheBigOne : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Consumables";
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
			Item.maxStack = Item.CommonMaxStack;
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
			recipe.AddIngredient(ModContent.ItemType<GrandHealingPotion>());
            recipe.AddIngredient(ModContent.ItemType<GrandManaPotion>());
            recipe.AddRecipeGroup("AAModClassic:SuperancientMaterial");
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
		}
	}
}