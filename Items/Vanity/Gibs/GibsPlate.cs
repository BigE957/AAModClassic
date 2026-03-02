using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Gibs

{
    [AutoloadEquip(EquipType.Body)]
    public class GibsPlate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Revenant Plate");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Developers!'");
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

        public override bool DrawBody()/* tModPorter Note: Removed. In SetStaticDefaults, use ArmorIDs.Body.Sets.HidesTopSkin[Item.bodySlot] = true if you returned false */
        {
            return false;
        }

        public override void DrawHands(ref bool drawHands, ref bool drawArms)/* tModPorter Note: Removed. In SetStaticDefaults, use ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false if you had drawHands set to true. If you had drawArms set to true, you don't need to do anything */
        {
            drawHands = false;
            drawArms = false;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Red;
            Item.vanity = true;
        }
    }
}