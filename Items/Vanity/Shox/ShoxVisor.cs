using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Shox
{
    [AutoloadEquip(EquipType.Head)]
	public class ShoxVisor : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Shock Lord's Visor");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Contributors!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(238, 168, 0);
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 20;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)/* tModPorter Note: Removed. In SetStaticDefaults, use ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true if you had drawHair set to true, and ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true if you had drawAltHair set to true */
        {
            drawHair = true;
        }
    }
}