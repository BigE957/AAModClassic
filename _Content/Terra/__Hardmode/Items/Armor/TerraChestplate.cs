using AAModClassic._Content.Terrarium.__Hardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terra.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class TerraChestplate : BaseAAItem
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
            recipe.AddRecipeGroup("AAModClassic:TerraPlates");
            recipe.AddIngredient(ModContent.ItemType<TerraPrism>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}