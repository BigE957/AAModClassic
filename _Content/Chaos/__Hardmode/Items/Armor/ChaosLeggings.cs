using AAModClassic._Content.Chaos.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class ChaosLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Chaos";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Greaves");
        }

		public override void SetDefaults()
		{
            Item.width = 22;
            Item.height = 16;
            Item.defense = 20;
            Item.rare = ItemRarityID.Lime;
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Generic) += .07f;
            AddEffect(new MovementSpeedEffect(0.10f));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("AAModClassic:ChaosLeggings");
            recipe.AddIngredient(ModContent.ItemType<ChaosPrism>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}