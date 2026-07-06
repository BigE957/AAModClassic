using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class FulguriteLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Fulgurite";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fulgurite Pants");
        }

		public override void SetDefaults()
		{
            Item.width = 22;
			Item.height = 18;
			Item.value = 50000;
			Item.rare = ItemRarityID.Pink;
			Item.defense = 11;
		}

        public override void RegisterEquipEffects()
        {
			damageMap.GetCritChance(DamageClass.Generic) += 5;
			AddEffect(new MovementSpeedEffect(0.16f));
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FulguriteBar>(), 18);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}