using AAModClassic;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Grox
{
    [AutoloadEquip(EquipType.Legs)]
	public class AngryPirateBoots : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Angry Pirate's Legguards");
            /* Tooltip.SetDefault(@"Hatred towards fish that can't code radiates from these boots.
'Great for impersonating Ancients Awakened Devs!'"); */
            ArmorIDs.Legs.Sets.HidesBottomSkin[Item.legSlot] = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(89, 119, 71);
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 20;
            Item.rare = ItemRarityID.Lime;
            Item.vanity = true;
        }
    }
}