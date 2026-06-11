using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic.Items.Vanity.Gibs
{
    [AutoloadEquip(EquipType.Head)]
	public class GibsSkull : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Revenant Skull");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Developers!'");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(255, 128, 0);
                }
            }
        }

        //public override void DrawHair(ref bool drawHair, ref bool drawAltHair)/* tModPorter Note: _Unreleased. In SetStaticDefaults, use ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true if you had drawHair set to true, and ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true if you had drawAltHair set to true */
        //{
        //    drawHair = false;
        //    drawAltHair = false;
        //}

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Red;
            Item.vanity = true;
        }
    }
}