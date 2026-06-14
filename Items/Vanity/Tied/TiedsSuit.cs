using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic.Items.Vanity.Tied
{
    [AutoloadEquip(EquipType.Body)]
	class TiedsSuit : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Vanity.Tied";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spooky Suit");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
        }
        public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 22;
			Item.rare = ItemRarityID.Cyan;
			Item.vanity = true;
		}
        //public override void DrawHands(ref bool drawHands, ref bool drawArms)/* tModPorter Note: _Unreleased. In SetStaticDefaults, use ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false if you had drawHands set to true. If you had drawArms set to true, you don't need to do anything */
		//{
		//	drawHands = false;
		//}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(0, 105, 0);
                }
            }
        }
    }
}
