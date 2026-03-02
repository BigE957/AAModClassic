using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Armor.Razewood
{
    [AutoloadEquip(EquipType.Body)]
    public class RazewoodChestplate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Razewood Chestplate");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.value = 2000;
            Item.rare = ItemRarityID.White;
            Item.defense = 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Razewood", 30);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}