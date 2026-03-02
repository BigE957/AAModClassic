using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.GlowingMushium
{
    [AutoloadEquip(EquipType.Legs)]
	public class ShroomPants : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Glowing Mushium Pants");
            // Tooltip.SetDefault("2% increased mana regeneration");

        }

		public override void SetDefaults()
		{
            Item.width = 22;
			Item.height = 18;
			Item.value = 50;
			Item.rare = 1;
            Item.value = Item.sellPrice(0, 0, 25, 0);
            Item.defense = 2;
		}

		public override void UpdateEquip(Player player)
        {
            player.manaRegenBonus += 2;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "GlowingMushiumBar", 5);
            recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}