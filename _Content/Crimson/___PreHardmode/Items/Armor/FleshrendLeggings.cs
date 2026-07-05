using AAModClassic._Content.Hell.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Crimson.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class FleshrendLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Fleshrend";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fleshrend Greaves");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 20;
			Item.value = 90000;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 8;
		}

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Melee) += .07f;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrimsonGreaves, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 6);
            recipe.AddIngredient(ItemID.Bone, 6);
            recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 6);
            recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}