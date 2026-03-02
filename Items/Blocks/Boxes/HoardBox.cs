using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic.Items.Blocks.Boxes
{
    public class HoardBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hoard Music Box");
            // Tooltip.SetDefault(@"Plays 'Cove' by Universe");
        }

        public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("HoardBox").Type;
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Yellow;
			Item.value = 10000;
			Item.accessory = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(null, "CovetiteCrystal", 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
