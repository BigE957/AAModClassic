using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.Hallam
{
    [AutoloadEquip(EquipType.Body)]
    public class HalTux : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Hallam's Fancy Tux");
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
            Item.rare = 9;
            Item.vanity = true;
        }
    }
}