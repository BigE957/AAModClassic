using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev
{
    public class GroxNote : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("GRealm Advertisement");
            /* Tooltip.SetDefault(@"'Want more AA content and more Grox-filled fun? Go play GRealm!'
-Grox The Great"); */
		}
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(89, 119, 71);
                }
            }
        }

        public override void SetDefaults()
		{
            Item.width = 22;
            Item.height = 22;
            Item.value = 0;
            Item.rare = ItemRarityID.White;
        }
	}
}
