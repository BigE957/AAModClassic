using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.Pluto.Shiny
{
    [AutoloadEquip(EquipType.Head)]
	public class ShinyPlutoMask : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Outer God's Ancient Mask");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");

        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(0, 190, 15);
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 20;
            Item.rare = 9;
            Item.vanity = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "PlutoMask", 1);
            recipe.AddRecipeGroup("AAMod:ShinyCharm");
            recipe.Register();
        }
    }
}