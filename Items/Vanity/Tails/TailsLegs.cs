using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Tails
{
    [AutoloadEquip(EquipType.Legs)]
    public class TailsLegs : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Tails Leggings");
            /* Tooltip.SetDefault(@"Leggings made to resemble a certain two-tailed fox.
'Great for impersonating Ancients Awakened Devs!'"); */
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(255, 216, 0);
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
