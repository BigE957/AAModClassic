using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Alphakip.Shiny
{
    [AutoloadEquip(EquipType.Legs)]
	public class ShinyFishDiverBoots : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Vanity.Alpha.Shiny";
        public override void SetStaticDefaults()
       
		{
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Alphakip's Flippers");
            /* Tooltip.SetDefault(@"Not actually flippers
'Great for impersonating Ancients Awakened Devs!'"); */
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(39, 115, 189);
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FishDiverBoots>(), 1);
            recipe.AddRecipeGroup("AAModClassic:ShinyCharm");
            recipe.Register();
        }
    }
}