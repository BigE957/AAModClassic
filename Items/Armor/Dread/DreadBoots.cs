using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Dread
{
    [AutoloadEquip(EquipType.Legs)]
	public class DreadBoots : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dread Moon Hakama");
			/* Tooltip.SetDefault(@"50% increased movement speed
25% decreased ammo consumption
The abyssal wrath of the Mire rests in this armor"); */
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 16;
			Item.value = 3000000;
			Item.defense = 34;
            Item.rare = 9;
            AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void UpdateEquip(Player player)
		{
			player.moveSpeed += .5f;
            player.ammoCost75 = true;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .5f;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "EventideAbyssium", 18);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(null, "DepthHakama", 1);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}