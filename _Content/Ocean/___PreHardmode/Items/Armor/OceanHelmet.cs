using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Ocean.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class OceanHelmet : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Ocean";
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Ocean Helmet");
        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 2;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<OceanChestplate>() && legs.type == ModContent.ItemType<OceanLeggings>();
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Magic) += 0.05f;
            AddEffect(new MaxManaEffect(20));
            AddEffect<UnlimitedBreathingUnderWaterForeverAndEverEffect>();

            AddSetEffect<OceanHelmetSetEffect>();
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Coral, 3);
			recipe.AddIngredient(ItemID.Starfish, 2);
			recipe.AddIngredient(ItemID.Seashell);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}