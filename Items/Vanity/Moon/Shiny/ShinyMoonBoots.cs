using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Moon.Shiny
{
    [AutoloadEquip(EquipType.Legs)]
	public class ShinyMoonBoots : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Lunar Boots");
            /* Tooltip.SetDefault(@"The boots of a legendary lunar mage
'Great for impersonating Ancients Awakened Devs!'"); */
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(159, 207, 190);
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.rare = 9;
            Item.vanity = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "MoonBoots", 1);
            recipe.AddRecipeGroup("AAMod:ShinyCharm");
            recipe.Register();
        }
    }
}