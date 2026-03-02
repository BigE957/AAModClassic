using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Boxes
{
    public class InfernoUBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Inferno Underground Music Box");
            // Tooltip.SetDefault(@"Plays 'Superheated' by Quicksilvur");
        }

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("InfernoUBox").Type;
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "InfernoBox");
            recipe.AddIngredient(null, "Torchstone", 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
