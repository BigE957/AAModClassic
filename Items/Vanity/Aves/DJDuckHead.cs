using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Aves
{
    [AutoloadEquip(EquipType.Head)]
	public class DJDuckHead : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("DJ Duck Mask");
            /* Tooltip.SetDefault(@"'Quek'
'Great for impersonating Ancients Awakened Devs!'"); */

        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(158, 255, 61);
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
	}
}