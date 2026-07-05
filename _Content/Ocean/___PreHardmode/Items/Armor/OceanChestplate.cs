using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Ocean.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class OceanChestplate : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Ocean";
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
			Item.rare = ItemRarityID.Orange;
			Item.defense = 4;
		}

        public override void RegisterEquipStats()
        {
			damageMap.GetDamage(DamageClass.Magic) += 0.05f;
			AddEffect(new MaxManaEffect(20));
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