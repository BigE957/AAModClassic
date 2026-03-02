using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic;


namespace AAModClassic.Items.Armor.DoomiteU
{
    [AutoloadEquip(EquipType.Legs)]
	public class DoomiteUGreaves : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dark Doomite Greaves");
        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
			Item.value = Item.sellPrice(0, 0, 5, 0);
			Item.rare = ItemRarityID.Orange;
			Item.defense = 3;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "DoomiteScrap", 8);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}