using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Armor.Chaos
{
    [AutoloadEquip(EquipType.Body)]
	public class ChaosDou : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Chaos Dou");
            // Tooltip.SetDefault(@"7% increased damage");
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
            player.GetDamage(DamageClass.Generic) += .07f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("AAMod:ChaosPlates");
            recipe.AddIngredient(null, "ChaosCrystal");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}