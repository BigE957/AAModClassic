using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Moon.Shiny
{
    [AutoloadEquip(EquipType.Body)]
    public class ShinyMoonRobe : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Lunar Robe");
            /* Tooltip.SetDefault(@"The Robe of a legendary lunar mage
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
            Item.width = 26;
            Item.height = 20;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "MoonRobe", 1);
            recipe.AddRecipeGroup("AAMod:ShinyCharm");
            recipe.Register();
        }
    }
}