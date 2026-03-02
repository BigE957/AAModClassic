using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Boxes
{
    public class DjinnBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Desert Djinn Music Box");
            // Tooltip.SetDefault(@"Plays 'The Dust Bowl' by Universe");
        }

        public override void SetDefaults()
		{
			Item.useStyle = 1;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("DjinnBox").Type;
			Item.width = 24;
			Item.height = 24;
			Item.rare = 4;
			Item.value = 10000;
			Item.accessory = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(null, "DesertMana", 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
