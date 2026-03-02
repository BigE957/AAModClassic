using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Potions
{
    public class GrandHealingPotion : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Grand Healing Potion");
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 34;
			Item.useTurn = true;
			Item.maxStack = 30;
			Item.healLife = 400;
            Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useStyle = 2;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.potion = true;
			Item.value = 50000;
            Item.rare = 11;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SuperHealingPotion);
            recipe.AddRecipeGroup("AAMod:AncientMaterials");
            recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.Register();
		}
	}
}