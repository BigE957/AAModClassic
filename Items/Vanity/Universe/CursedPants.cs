using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Universe
{
    [AutoloadEquip(EquipType.Legs)]
	public class CursedPants : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Cursed Reaper Boots");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(29, 109, 124);
                }
            }
        }


        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }
    }
}