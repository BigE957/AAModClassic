using System.Collections.Generic;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Vanity.CC.Shiny
{
    [AutoloadEquip(EquipType.Head)]
	public class ShinyCCHood : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Draconian Cultist Mask");
			/* Tooltip.SetDefault(@"The mask of a crazy dragon enthusiast
'Great for impersonating Ancients Awakened Developers!'"); */
		}

		public override void SetDefaults() 
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = 2;
			Item.vanity = true;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.Mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.OverrideColor = new Color(92, 101, 150);
				}
			}
		}
	}
}