using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Boxes
{
    public class InfernoNightBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Inferno Night Music Box");
            // Tooltip.SetDefault(@"Plays 'Burnt to Ashes' by Quicksilvur");
        }

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("InfernoNightBox").Type;
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(null, "Razewood", 20);
			recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
