using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity

{
    [AutoloadEquip(EquipType.Body)]
    public class PlutoChestplateS : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Vanity.Pluto.Shiny";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Outer God's Ancient Chestplate");
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
            Item.width = 26;
            Item.height = 20;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PlutoChestplate>(), 1);
            recipe.AddRecipeGroup("AAModClassic:ShinyCharm");
            recipe.Register();
        }
    }
}