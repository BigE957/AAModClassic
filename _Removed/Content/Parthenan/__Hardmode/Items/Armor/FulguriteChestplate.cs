using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class FulguriteChestplate : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Fulgurite";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fulgurite Breastplate");
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 20;
			Item.value = 40000;
			Item.rare = ItemRarityID.Pink;
			Item.defense = 17;
		}

        public override void RegisterEquipStats()
        {
			damageMap.GetDamage(DamageClass.Generic) += 0.07f;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FulguriteBar>(), 24);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}