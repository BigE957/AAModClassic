using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Alphakip
{

    [AutoloadEquip(EquipType.Wings)]
    public class KipronWings : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Kipron Wings");
            /* Tooltip.SetDefault(@"Allows flight and slow fall
Hold down and jump to hover for an extended period of time
'Great for impersonating Ancients Awakened Devs!'"); */

            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(300, 10, 6.25f, true, 15, 10);
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;
            Item.value = 500000;
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(39, 115, 189);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 300;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }

        public override bool WingUpdate(Player player, bool inUse)
        {
            int fspeed = 6;

            if (player.controlDown && player.controlJump && player.wingTime > 0f && !player.merman)
            {
                player.velocity.Y *= 0.01f;
                if (player.velocity.Y > -2f && player.velocity.Y < 1f)
                {
                    player.velocity.Y = 1E-05f;
                }
                fspeed = 4;
            }

            if (inUse)
            {
                if (player.controlJump && player.wingTime <= 0)
                {
                    player.wingFrame = 2;
                }
                player.wingFrameCounter++;
                if (player.wingFrameCounter > fspeed)
                {
                    player.wingFrame++;
                    player.wingFrameCounter = 0;
                }
            }
            else
            {
                player.wingFrame = 0;
                if (player.velocity.Y != 0)
                {
                    player.wingFrame = 1;
                }
            }
            if (player.wingFrame > 3)
            {
                player.wingFrame = 0;
            }
            return true;
        }
    }
}
