using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Alphakip

{
    [AutoloadEquip(EquipType.Body)]
    public class FishDiverJacket : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Alphakip's Diving Jacket");
            /* Tooltip.SetDefault(@"This jacket is so insulated, you could sit in the ocean and still come out dry
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
            Item.width = 26;
            Item.height = 20;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }
    }
}