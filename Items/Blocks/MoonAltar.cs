using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks
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
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.rare = ItemRarityID.Red;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.createTile = ModContent.TileType<MoonAltar>();
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