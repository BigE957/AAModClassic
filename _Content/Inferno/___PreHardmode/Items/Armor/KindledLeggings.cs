using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class KindledLeggings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Kindled Suneate");
            // Tooltip.SetDefault("Forged in the flames of the blazing sun");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 18;
			Item.value = 5000;
			Item.rare = ItemRarityID.Green;
			Item.defense = 7;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<IncineriteBar>(), 20);
            recipe.AddIngredient(ModContent.ItemType<ScorchedScale>(), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
	}
}