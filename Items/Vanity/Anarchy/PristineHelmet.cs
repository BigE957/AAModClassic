using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.Anarchy
{
    [AutoloadEquip(EquipType.Head)]
	public class PristineHelmet : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Pristine Helmet");
            /* Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Contributors!'
For the record, Anarchy sprited this himself."); */
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(200, 200, 200);
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 30;
            Item.rare = 9;
            Item.vanity = true;
        }
	}
}