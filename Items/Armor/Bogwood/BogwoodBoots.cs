using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Bogwood
{
    [AutoloadEquip(EquipType.Legs)]
	public class BogwoodBoots : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bogwood Boots");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 18;
            Item.value = 100;
            Item.rare = 0;
            Item.defense = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Bogwood", 25);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}