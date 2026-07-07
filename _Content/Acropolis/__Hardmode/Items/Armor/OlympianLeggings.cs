using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Content.Acropolis.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Acropolis.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class OlympianLeggings : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Olympian";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Olympian Greaves");
        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.LightPurple;
			Item.defense = 8;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GladiatorLeggings);
            recipe.AddIngredient(ModContent.ItemType<GoddessFeather>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}