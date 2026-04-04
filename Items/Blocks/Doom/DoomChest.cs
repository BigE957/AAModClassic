using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Doom
{
    public class DoomChest : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Doom Chest");
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
			Item.createTile = ModContent.TileType<DoomChest>();
		}

		public override void AddRecipes()
		{
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.IronBar, 2);
                recipe.AddIngredient(null, "ApocalyptitePlate", 12);
                recipe.AddTile(null, "ACS");
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.LeadBar, 2);
                recipe.AddIngredient(null, "ApocalyptitePlate", 12);
                recipe.AddTile(null, "ACS");
                recipe.Register();
            }
        }
    }
}