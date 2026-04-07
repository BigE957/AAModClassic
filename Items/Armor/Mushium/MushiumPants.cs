using AAModClassic;
using AAModClassic.Items.Boss.MushroomMonarch;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Armor.Mushium
{
    [AutoloadEquip(EquipType.Legs)]
	public class MushiumPants : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mushium Pants");
            // Tooltip.SetDefault("1% Increased life regeneration");

        }

		public override void SetDefaults()
		{
            Item.width = 22;
			Item.height = 18;
			Item.value = 50;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 3;
            Item.value = Item.sellPrice(0, 0, 25, 0);
        }

		public override void UpdateEquip(Player player)
        {
            player.lifeRegen += 1;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MushiumBar>(), 5);
            recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}