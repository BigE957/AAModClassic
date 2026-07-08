using AAModClassic._Content._Dev.TEMPliz.Cat;
using AAModClassic._Content.Mire.___PreHardmode.Items.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.TEMPliz.Dragon

{
    [AutoloadEquip(EquipType.Body)]
    public class AquariumChestplate : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Vanity.Caligulas";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Dark Dragoness' Robes");
            // Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = true;
            ArmorIDs.Body.Sets.DisableHandOnAndOffAccDraw[Item.bodySlot] = true;
            ArmorIDs.Body.Sets.HidesArms[Item.bodySlot] = true;

            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<CaligulasChestplate>();
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
            Item.width = 26;
            Item.height = 20;
            Item.rare = ItemRarityID.Purple;
            Item.vanity = true;
        }
    }
}