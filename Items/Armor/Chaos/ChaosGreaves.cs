using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Armor.Chaos
{
    [AutoloadEquip(EquipType.Legs)]
	public class ChaosGreaves : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Greaves");
            /* Tooltip.SetDefault(@"10% increased movement speed
7% increased damage"); */
        }

		public override void SetDefaults()
		{
            Item.width = 22;
            Item.height = 16;
            Item.defense = 20;
            Item.rare = ItemRarityID.Lime;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += .07f;
            player.moveSpeed += .1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("AAMod:ChaosBoots");
            recipe.AddIngredient(null, "ChaosCrystal");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}