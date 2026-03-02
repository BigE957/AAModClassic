using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.BogwoodF
{
    public class BogwoodChest : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Bogwood Chest");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 500;
			Item.createTile = Mod.Find<ModTile>("BogwoodChest").Type;
		}

		public override void AddRecipes()
		{
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.IronBar, 2);
                recipe.AddIngredient(null, "Bogwood", 12);
                recipe.AddTile(TileID.WorkBenches);
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.LeadBar, 2);
                recipe.AddIngredient(null, "Bogwood", 12);
                recipe.AddTile(TileID.WorkBenches);
                recipe.Register();
            }
        }
    }
}