using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Alphakip
{
    [AutoloadEquip(EquipType.Legs)]
	public class FishDiverBoots : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Vanity.Alphakip";
		public override void SetStaticDefaults()
       
		{
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Alphakip's Flippers");
            /* Tooltip.SetDefault(@"Not actually flippers
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
            Item.width = 22;
            Item.height = 18;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }
    }
}