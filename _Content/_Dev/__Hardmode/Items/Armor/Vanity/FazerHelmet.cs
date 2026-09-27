using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class FazerHelmet : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Vanity.Fazer";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fluffy Fox Wig");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Contributors!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(30, 70, 130);
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