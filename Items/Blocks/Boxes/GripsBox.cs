using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Boxes
{
    public class GripsBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Grips of Chaos Music Box");
            // Tooltip.SetDefault(@"Plays 'Twin Terror' by Spectral Aves");
        }

		public override void SetDefaults()
		{
			Item.useStyle = 1;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("GripsBox").Type;
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
            recipe.AddIngredient(null, "Incinerite", 5);
            recipe.AddIngredient(null, "Abyssium", 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
