using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Misc._PostMoonlord.Items.Consumables
{
    public class GrandHealingPotion : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Consumables";
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
			Recipe recipe = CreateRecipe(4);
			recipe.AddIngredient(ItemID.SuperHealingPotion, 4);
            recipe.AddRecipeGroup("AAModClassic:LateAncientMaterial");
            recipe.AddTile(TileID.Bottles);
			recipe.Register();
		}
	}
}