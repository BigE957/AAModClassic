using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Blocks.Boxes
{
    public class ShenABox : BaseAAItem
	{
        
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shen Doragon Awakened Music Box");
            
            // Tooltip.SetDefault(@"Plays 'Blaze of Glory' by Charlie Debnam");
        }

		public override void SetDefaults()
		{
			Item.useStyle = 1;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("ShenABox").Type;
            Item.width = 72;
			Item.height = 36;
			Item.rare = 4;
			Item.value = 10000;
			Item.accessory = true;
        }

        public override void AddRecipes()
        {
            if (Main.expertMode == true)
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.MusicBox);
                recipe.AddIngredient(null, "ShenBox");
                recipe.AddIngredient(null, "ChaosSoul");
                recipe.AddTile(TileID.Sawmill);
                recipe.Register();
            }
        }
    }
}
