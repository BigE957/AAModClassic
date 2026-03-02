using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Armor.Razewood
{
    [AutoloadEquip(EquipType.Legs)]
	public class RazewoodBoots : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Razewood Boots");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 18;
            Item.value = 100;
            Item.rare = ItemRarityID.White;
            Item.defense = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Razewood", 25);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}