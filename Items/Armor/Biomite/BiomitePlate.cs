using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Biomite
{
    [AutoloadEquip(EquipType.Body)]
	public class BiomitePlate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Biomite Crystalmail");
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.value = 6000;
			Item.rare = ItemRarityID.Green;
			Item.defense = 5;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "TerraShard", 25);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}