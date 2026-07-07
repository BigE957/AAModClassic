using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
    public class AvesHelmet : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Vanity.Aves";
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("DJ Duck Mask");
            /* Tooltip.SetDefault(@"'Quek'
'Great for impersonating Ancients Awakened Devs!'"); */

        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(158, 255, 61);
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