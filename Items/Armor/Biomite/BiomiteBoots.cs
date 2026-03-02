using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Biomite
{
    [AutoloadEquip(EquipType.Legs)]
	public class BiomiteBoots : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Biomite Greaves");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 18;
			Item.value = 4500;
			Item.rare = 2;
			Item.defense = 5;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "TerraShard", 20);
			recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
	}
}