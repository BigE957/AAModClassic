using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Content.Acropolis.__Hardmode.Items.Materials;

namespace AAModClassic._Content.Acropolis.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class OlympianChestplate : BaseAAItem
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
			Item.rare = ItemRarityID.LightPurple;
			Item.defense = 8;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GladiatorBreastplate);
            recipe.AddIngredient(ModContent.ItemType<GoddessFeather>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}