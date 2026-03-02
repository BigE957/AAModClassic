using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Armor.Oroboros
{
    [AutoloadEquip(EquipType.Legs)]
	public class OroborosBoots : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Oroboros Wood Boots");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 18;
            Item.value = 100;
            Item.rare = ItemRarityID.Orange;
            Item.defense = 4;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(null, "OroborosWood", 25);
                recipe.AddTile(TileID.WorkBenches);
                recipe.Register();
            }
        }
    }
}