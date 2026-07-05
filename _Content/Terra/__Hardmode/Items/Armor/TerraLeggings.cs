using AAModClassic._Content.Terrarium.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terra.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class TerraLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Terra";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra Greaves");
        }

		public override void SetDefaults()
		{
            Item.width = 22;
            Item.height = 16;
            Item.defense = 22;
            Item.rare = ItemRarityID.Lime;
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Generic) += .05f;
            AddEffect(new MovementSpeedEffect(0.10f));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("AAModClassic:TerraLeggings");
            recipe.AddIngredient(ModContent.ItemType<TerraPrism>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}