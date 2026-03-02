using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Eliza.Dragon

{
    [AutoloadEquip(EquipType.Body)]
    public class LizRobes : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Dark Dragoness' Robes");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(121, 21, 214);
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 20;
            Item.rare = ItemRarityID.Purple;
            Item.vanity = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "LizShirt");
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }
    }
}