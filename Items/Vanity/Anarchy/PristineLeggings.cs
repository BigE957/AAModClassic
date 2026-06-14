using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Anarchy
{
    [AutoloadEquip(EquipType.Legs)]
	public class PristineLeggings : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Vanity.Anarchy";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Pristine Leggings");
            /* Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Contributors!'
For the record, Anarchy sprited this himself."); */
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(200, 200, 200);
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