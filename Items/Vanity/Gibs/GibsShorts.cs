using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Gibs
{
    [AutoloadEquip(EquipType.Legs)]
	public class GibsShorts : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Revenant Legs");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Developers!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(255, 128, 0);
                }
            }
        }

        public override bool DrawLegs()/* tModPorter Note: Removed. In SetStaticDefaults, use ArmorIDs.Legs.Sets.HidesBottomSkin[Item.legSlot] = true if you returned false for an accessory of EquipType.Legs, and ArmorIDs.Shoe.Sets.OverridesLegs[Item.shoeSlot] = true if you returned false for an accessory of EquipType.Shoes */
        {
            return false;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Red;
            Item.vanity = true;
        }
    }
}