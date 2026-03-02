using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Ocean
{
    [AutoloadEquip(EquipType.Body)]
	public class OceanShirt : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ocean Chestplate");
			/* Tooltip.SetDefault(@"Increases maximum mana by 20
5% increased magic damage"); */
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
			Item.value = Item.sellPrice (0, 0, 5, 0);
			Item.rare = 3;
			Item.defense = 4;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.statManaMax2 += 20;
            player.GetDamage(DamageClass.Magic) += 0.05f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Coral, 6);
			recipe.AddIngredient(ItemID.Starfish, 2);
			recipe.AddIngredient(ItemID.Seashell, 3);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}