using AAModClassic;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories
{
    [AutoloadEquip(EquipType.Face)]
    public class TimeStone : EquipAbstract, ILocalizedModType
    {
        

        public override void SetStaticDefaults()
        {
            /*DisplayName.SetDefault("Time Stone");
            Tooltip.SetDefault(
@"Respawn time cut by 80%
Pressing the Time Stone hotkey will allow you to speed up and resume time.
Using the Time stone like an item stops/resumes time.
'Dread it. Run from it. Destiny still arives.'");*/

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 16));
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 52;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.rare = 11;
            Item.accessory = true;
            Item.consumable = false;
            Item.prefix = 0;
        }

        public override void PostReforge()
        {
            Item.prefix = 0;
        }

        public override void UpdateInventory(Player player)
        {
            Item.prefix = 0;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            base.ModifyTooltips(list);
            
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = Color.Green;
                }
            }
        }

        public override bool? UseItem(Player player)
        {
            Main.fastForwardTimeToDawn = false;
            Main.fastForwardTimeToDusk = false;

            if (!AAWorld.TimeStopped)
            {
                AAWorld.PausedTime = Main.time;
                AAWorld.TimeStopped = true;
            }
            else
            {
                AAWorld.TimeStopped = false;
            }

            return true;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new AttacksInflictBuffEffect(null, (BuffID.Chilled, 1200))); // real effect of hte item just undocumented
            AddEffect<TimeStoneRespawnEffect>();
            AddEffect<TimeStoneTimeStopEffect>();
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return incomingItem.type != ModContent.ItemType<MindStone>() || incomingItem.type != ModContent.ItemType<PowerStone>() || incomingItem.type != ModContent.ItemType<RealityStone>() || incomingItem.type != ModContent.ItemType<SoulStone>() || incomingItem.type != ModContent.ItemType<SpaceStone>() || incomingItem.type != ModContent.ItemType<TimeStone>() || incomingItem.type != ModContent.ItemType<InfinityGauntlet>();
        }
    }
}