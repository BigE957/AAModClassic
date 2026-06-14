using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic.Items.Vanity.Hallam
{
    [AutoloadEquip(EquipType.Legs)]
	public class HalTrousers : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Vanity.Hallam";
        public override void SetStaticDefaults()
       
		{
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Hallam's Fashionable Trousers");
            /* Tooltip.SetDefault(
@"These pants cost way more than you do
'Great for impersonating Ancients Awakened Devs!'"); */
		}
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(255, 8, 251);
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