using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Cerberus
{
    [AutoloadEquip(EquipType.Head)]
	public class InvokerHood : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Invoker Hood");
            /* Tooltip.SetDefault(@"The enchanted hood of Aleister the 'Mega Therion'
Great for impersonating Awakened Devs!"); */
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = Color.Gold;
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