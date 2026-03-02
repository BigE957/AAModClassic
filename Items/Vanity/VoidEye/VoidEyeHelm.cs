using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using AAModClassic;

namespace AAModClassic.Items.Vanity.VoidEye
{
    [AutoloadEquip(EquipType.Head)]
	public class VoidEyeHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Void Eye's Mask");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Contributors!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(148, 18, 142);
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 30;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }
	}
}