using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Potions
{
    public class GrandManaPotion : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Grand Mana Potion");
		}
		
		public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item3;
            Item.healMana = 400;
            Item.useStyle = 2;
            Item.useTurn = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.maxStack = 50;
            Item.consumable = true;
            Item.width = 14;
            Item.height = 24;
            Item.value = 50000;
            Item.rare = 11;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SuperManaPotion);
            recipe.AddRecipeGroup("AAMod:AncientMaterials");
            recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.Register();
		}
	}
}