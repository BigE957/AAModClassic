using AAModClassic._Content.Terrarium.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Terrarium.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class BiomiteLeggings : BaseAAItem
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
			Item.rare = ItemRarityID.Green;
			Item.defense = 5;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<TerraShard>(), 20);
			recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
	}
}