using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Eliza.Cat
{
    [AutoloadEquip(EquipType.Head)]
	public class LizEars : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Midnight Cat Ears");
            /* Tooltip.SetDefault(@"As opposed to normal cat ears
'Great for impersonating Ancients Awakened Devs!'"); */

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
            Item.width = 18;
            Item.height = 20;
            Item.rare = ItemRarityID.Purple;
            Item.vanity = true;
        }
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)/* tModPorter Note: Removed. In SetStaticDefaults, use ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true if you had drawHair set to true, and ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true if you had drawAltHair set to true */
        {
            drawHair = drawAltHair = true;  //this make so the player hair does not show when the vanity mask is equipped.  add true if you want to show the player hair.
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "LizHood");
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }
    }
}