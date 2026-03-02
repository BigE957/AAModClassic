using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class MoonAltar : BaseAAItem
	{

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dread Moon Altar");
        }

        public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.rare = 10;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.createTile = Mod.Find<ModTile>("MoonAltar").Type;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "EventideAbyssium", 15);
			recipe.AddTile(null, "ACS");
			recipe.Register();
		}
	}
}