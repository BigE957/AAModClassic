using System.Collections.Generic;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Tied   //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    [AutoloadEquip(EquipType.Head)]
    public class TiedHelmet : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Vanity.Tied";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spooky Skull");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }

        public override void SetDefaults()
        {
            Item.width = 24; //The size in width of the sprite in pixels.
            Item.height = 28;   //The size in height of the sprite in pixels.
            Item.rare = ItemRarityID.Cyan;    //The color the title of your item when hovering over it ingame
            Item.vanity = true; //this defines if this item is vanity or not.
        }

        //public override void DrawHair(ref bool drawHair, ref bool drawAltHair)/* tModPorter Note: _Unreleased. In SetStaticDefaults, use ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true if you had drawHair set to true, and ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true if you had drawAltHair set to true */
        //{
        //    drawHair = drawAltHair = false;  //this make so the player hair does not show when the vanity mask is equipped.  add true if you want to show the player hair.
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