using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.VoidEye
{
    [AutoloadEquip(EquipType.Legs)]
	public class VoidEyeLeggings : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Vanity.VoidEye";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Void Eye's Boots");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Contributors!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(148, 18, 142);
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 22;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }
    }
}