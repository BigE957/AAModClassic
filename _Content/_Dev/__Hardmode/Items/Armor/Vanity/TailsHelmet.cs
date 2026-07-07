using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class TailsHelmet : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Vanity.Tails";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Tails Helmet");
            /* Tooltip.SetDefault(@"A helmet made to resemble a certain two-tailed fox.
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
            Item.width = 18;
            Item.height = 20;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }
    }
}