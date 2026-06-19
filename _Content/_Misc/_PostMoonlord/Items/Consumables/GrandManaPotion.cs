using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Misc._PostMoonlord.Items.Consumables
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
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useTurn = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.width = 14;
            Item.height = 24;
            Item.value = 50000;
            Item.rare = ItemRarityID.Purple;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(4);
			recipe.AddIngredient(ItemID.SuperManaPotion, 4);
            recipe.AddRecipeGroup("AAModClassic:LateAncientMaterial");
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
		}
	}
}