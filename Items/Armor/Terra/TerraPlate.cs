using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Terra
{
    [AutoloadEquip(EquipType.Body)]
	public class TerraPlate : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Terra Chestplate");
            // Tooltip.SetDefault(@"5% increased damage");
        }


        public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
			Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 22;
		}

		public override void UpdateEquip(Player player)
		{
            player.GetDamage(DamageClass.Generic) += .05f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("AAMod:TerraPlates");
            recipe.AddIngredient(null, "TerraCrystal");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}