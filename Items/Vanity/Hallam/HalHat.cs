using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Hallam
{
    [AutoloadEquip(EquipType.Head)]
	public class HalHat : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Hallam's Dapper Top Hat");
            /* Tooltip.SetDefault(
@"You can't help but feel fancy just wearing this
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
            Item.width = 20;
            Item.height = 14;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }
	}
}