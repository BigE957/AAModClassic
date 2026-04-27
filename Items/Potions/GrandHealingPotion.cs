using AAModClassic.Tiles.Crafters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Potions
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
			Item.maxStack = Item.CommonMaxStack;
			Item.healLife = 400;
            Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.potion = true;
			Item.value = 50000;
            Item.rare = ItemRarityID.Purple;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SuperHealingPotion);
            recipe.AddRecipeGroup("AAModClassic:AncientMaterials");
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
			recipe.Register();
		}
	}
}