using AAModClassic._Content._Dev.TEMPliz.Dragon;
using AAModClassic._Content.Mire.___PreHardmode.Items.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.TEMPliz.Cat
{
    [AutoloadEquip(EquipType.Head)]
	public class CaligulasHelmet : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Vanity.Caligulas";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Midnight Cat Ears");
            /* Tooltip.SetDefault(@"As opposed to normal cat ears
'Great for impersonating Ancients Awakened Devs!'"); */
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;

            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<AquariumHelmet>();
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(121, 21, 214);
                }
            }
        }


        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 20;
            Item.rare = ItemRarityID.Purple;
            Item.vanity = true;
        }
    }
}