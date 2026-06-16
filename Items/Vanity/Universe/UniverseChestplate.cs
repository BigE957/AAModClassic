using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.Attributes;
using AAModClassic.Globals;

namespace AAModClassic.Items.Vanity.Universe

{
    [AutoloadEquip(EquipType.Body)]
    [AutoloadEquipGlow(EquipType.Body)]
    public class UniverseChestplate : BaseAAItem, ILocalizedModType, ICustomEquipGlow
    {
        public new string LocalizationCategory => "Items.Vanity.Universe";
        public Color Color => AAColor.COLOR_WHITEFADE1;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cursed Reaper Robe");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(29, 109, 124);
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