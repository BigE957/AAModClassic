using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Grox
{
    [AutoloadEquip(EquipType.Head)]
	public class AngryPirateHood : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Angry Pirate's Hood");
            /* Tooltip.SetDefault(@"Hatred towards fish that can't code radiates from this hood.
'Great for impersonating Ancients Awakened Devs!'"); */
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(89, 119, 71);
                }
            }
        }
        
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 20;
            Item.rare = ItemRarityID.Lime;
            Item.vanity = true;
        }
	}
}