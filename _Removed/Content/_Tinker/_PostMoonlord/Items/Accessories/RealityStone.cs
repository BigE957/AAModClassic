using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories
{
    [AutoloadEquip(EquipType.Face, EquipType.Wings)]
    public class RealityStone : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            /*DisplayName.SetDefault("Reality Stone");
            Tooltip.SetDefault(
@"Grants you control over reality around you allowing long flight, insane speed, and uninhibited movement
'Now...reality can be whatever I want it to be...'");*/

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 13));
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 36;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.rare = 11;
            Item.accessory = true;
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
                    line2.OverrideColor = Color.DarkRed;
                }
            }
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new WingTimeMaxEffect(500));
            AddEffect(new MaxRunSpeedEffect(10.00f)); // !!!
            AddEffect(new MovementSpeedEffect(1.00f));
            AddEffect(new FrostsparkBootsEffect(0, 0, true));
            bool lol = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) ? true : false;
            AddEffect(new LavaWadersWaterWalkingEffect(lol));
            AddEffect(new LavaWadersFireImmunityEffect(true, 0));
            AddEffect<FullLavaImmunityEffect>();
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 1f;
            ascentWhenRising = 0.4f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 4f;
            constantAscend = 0.3f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 20f;
            acceleration *= 3f;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return incomingItem.type != ModContent.ItemType<MindStone>() || incomingItem.type != ModContent.ItemType<PowerStone>() || incomingItem.type != ModContent.ItemType<RealityStone>() || incomingItem.type != ModContent.ItemType<SoulStone>() || incomingItem.type != ModContent.ItemType<SpaceStone>() || incomingItem.type != ModContent.ItemType<TimeStone>() || incomingItem.type != ModContent.ItemType<InfinityGauntlet>();
        }
    }
}