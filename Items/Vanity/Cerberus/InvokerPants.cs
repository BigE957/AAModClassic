using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Cerberus
{
    [AutoloadEquip(EquipType.Legs)]
	public class InvokerPants : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Invoker Boots");
            /* Tooltip.SetDefault(@"The enchanted boots of Aleister the 'Mega Therion'
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
            Item.width = 22;
            Item.height = 18;
            Item.rare = 9;
            Item.vanity = true;
        }
    }
}