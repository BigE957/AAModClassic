using AAModClassic.___Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic.Items.Boss.Broodmother;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Armor.Kindled
{
    [AutoloadEquip(EquipType.Legs)]
	public class KindledSuneate : BaseAAItem
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
            recipe.AddIngredient(ModContent.ItemType<BroodScale>(), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
	}
}