using AAModClassic._Content.Terrarium.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Terrarium.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class BiomiteChestplate : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Biomite";
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
			recipe.AddIngredient(ModContent.ItemType<TerraShard>(), 25);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}