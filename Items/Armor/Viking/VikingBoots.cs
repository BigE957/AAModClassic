using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic;
using AAModClassic.Items.Materials;

namespace AAModClassic.Items.Armor.Viking
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
			Item.rare = ItemRarityID.Orange;
			Item.defense = 8;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<RelicBar>(), 14);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}