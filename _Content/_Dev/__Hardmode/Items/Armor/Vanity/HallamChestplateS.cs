using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity
{
    [AutoloadEquip(EquipType.Body)]
    public class HallamChestplateS : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Vanity.Hallam.Shiny";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Hallam's Velvet Tux");
            /* Tooltip.SetDefault(
@"This tux was woven with pure class
'Great for impersonating Ancients Awakened Devs!'"); */
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(255, 8, 251);
                }
            }
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HallamChestplate>(), 1);
            recipe.AddRecipeGroup("AAModClassic:ShinyCharm");
            recipe.Register();
        }
    }
}