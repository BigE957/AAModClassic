using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Apawn

{
    [AutoloadEquip(EquipType.Body)]
    public class ApawnPlate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Apawn's Plate");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Testers!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(162, 116, 55);
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 20;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }
    }
}