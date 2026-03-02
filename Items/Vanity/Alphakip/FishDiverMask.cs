using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using AAModClassic;

namespace AAModClassic.Items.Vanity.Alphakip
{
    [AutoloadEquip(EquipType.Head)]
	public class FishDiverMask : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Alphakip's Diving Mask");
            /* Tooltip.SetDefault(@"So I heard you like mudkips
'Great for impersonating Ancients Awakened Devs!'"); */

        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(39, 115, 189);
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