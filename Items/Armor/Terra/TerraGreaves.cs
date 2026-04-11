using AAModClassic.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Armor.Terra
{
    [AutoloadEquip(EquipType.Legs)]
	public class TerraGreaves : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra Greaves");
            /* Tooltip.SetDefault(@"10% increased movement speed
5% increased damage"); */
        }

		public override void SetDefaults()
		{
            Item.width = 22;
            Item.height = 16;
            Item.defense = 22;
            Item.rare = ItemRarityID.Lime;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += .05f;
            player.moveSpeed += .1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("AAModClassic:TerraBoots");
            recipe.AddIngredient(ModContent.ItemType<TerraPrism>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}