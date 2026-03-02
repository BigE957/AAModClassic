using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Radium
{
    [AutoloadEquip(EquipType.Legs)]
	public class RadiumCuisses : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Radium Cuisses");
			/* Tooltip.SetDefault(@"30% increased movement speed
Shines with the light of a starry night sky"); */
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 18;
			Item.value = 300000;
			Item.defense = 20;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.3f;
			player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .3f;
            Lighting.AddLight(player.Center, 1.0f, 1.0f, 1.0f);
        }

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "RadiumBar", 27);
            recipe.AddIngredient(null, "Stardust", 15);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
	}
}