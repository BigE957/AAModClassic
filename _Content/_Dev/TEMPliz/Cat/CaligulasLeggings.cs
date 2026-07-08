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
    [AutoloadEquip(EquipType.Legs)]
	public class CaligulasLeggings : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Vanity.Caligulas";
        public override void SetStaticDefaults()
		{
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Midnight Cat Boots");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
            //ArmorIDs.Legs.Sets.HidesBottomSkin[Item.legSlot] = true;

            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<AquariumLeggings>();
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
            Item.width = 22;
            Item.height = 18;
            Item.rare = ItemRarityID.Purple;
            Item.vanity = true;
        }
    }
}