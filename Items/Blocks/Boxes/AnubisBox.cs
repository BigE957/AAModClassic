using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Boxes
{
    public class AnubisBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Anubis Music Box");
            // Tooltip.SetDefault(@"Plays 'Strings of Judgement' by Universe");
        }

        public override void SetDefaults()
		{
			Item.useStyle = 1;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("AnubisBox").Type;
			Item.width = 24;
			Item.height = 24;
			Item.rare = 5;
			Item.value = 10000;
			Item.accessory = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(null, "ForsakenFragment", 3);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
