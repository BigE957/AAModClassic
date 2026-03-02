using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Charlie

{
    [AutoloadEquip(EquipType.Body)]
    public class CharlieCloak : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Wraith Cloak");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(60, 12, 98);
                }
            }
        }


        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 20;
            Item.rare = ItemRarityID.Purple;
            Item.vanity = true;
        }
    }
}