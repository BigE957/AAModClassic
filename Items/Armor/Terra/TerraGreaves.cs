using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Terra
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
            Item.rare = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += .05f;
            player.moveSpeed += .1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("AAMod:TerraBoots");
            recipe.AddIngredient(null, "TerraCrystal");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}