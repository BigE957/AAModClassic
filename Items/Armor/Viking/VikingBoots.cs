using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Viking
{
    [AutoloadEquip(EquipType.Legs)]
	public class VikingBoots : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Viking Greaves");
        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
			Item.value = Item.sellPrice(0, 0, 5, 0);
			Item.rare = 3;
			Item.defense = 8;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null,"RelicBar", 14);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}