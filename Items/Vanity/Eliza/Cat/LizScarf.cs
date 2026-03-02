using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Eliza.Cat
{
    [AutoloadEquip(EquipType.Neck)]
	public class LizScarf : BaseAAItem
	{
		public override void SetStaticDefaults()
       
		{
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Midnight Scarf");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
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
            Item.width = 22;
            Item.height = 18;
            Item.rare = ItemRarityID.Purple;
            Item.vanity = true;
            Item.accessory = true;
        }
    }
}