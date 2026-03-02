using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace AAMod.Items.Blocks.Boxes
{
    public class YamataABox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Yamata Awakened Music Box");
            // Tooltip.SetDefault(@"Plays 'Abyssal Nightmare' by Universe");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.YamataA;
                }
            }
        }
        public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("YamataABox").Type;
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "YamataBox");
            recipe.AddIngredient(null, "Naitokurosu");
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
