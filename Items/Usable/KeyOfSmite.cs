using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Usable
{
    public class KeyOfSmite : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Key of Smite");
			// Tooltip.SetDefault("'Charged with flaming energy'");
		}


        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.rare = 0;
            Item.maxStack = 99;
            Item.value = 100;
            Item.useStyle = 4;
            Item.useTime = Item.useAnimation = 19;
            Item.noMelee = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "SoulOfSmite", 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }


    }
}
