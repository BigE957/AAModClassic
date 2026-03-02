using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Viking
{
    [AutoloadEquip(EquipType.Head)]
	public class VikingHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Viking Helmet");
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
            recipe.AddIngredient(null,"RelicBar", 14);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}