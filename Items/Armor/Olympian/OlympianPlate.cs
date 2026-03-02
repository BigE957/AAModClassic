using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Olympian
{
    [AutoloadEquip(EquipType.Body)]
	public class OlympianPlate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Olympian Breastplate");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
			Item.value = Item.sellPrice (0, 5, 0, 0);
			Item.rare = 6;
			Item.defense = 8;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GladiatorBreastplate);
            recipe.AddIngredient(null, "GoddessFeather", 10);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}