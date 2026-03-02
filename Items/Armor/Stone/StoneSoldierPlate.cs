using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic.Items.Armor.Stone
{
    [AutoloadEquip(EquipType.Body)]
	public class StoneSoldierPlate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stone Soldier Breastplate");
			// Tooltip.SetDefault(@"Increases mining speed by 15%");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
			Item.value = Item.sellPrice (0, 5, 0, 0);
			Item.rare = ItemRarityID.Yellow;
			Item.defense = 16;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.pickSpeed -= 0.15f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MiningShirt);
            recipe.AddIngredient(null, "StoneShell", 10);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}