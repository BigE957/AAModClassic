using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories
{
    [AutoloadEquip(EquipType.Face)]
    public class SoulStone : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            /*DisplayName.SetDefault("Soul Stone");
            Tooltip.SetDefault(
@"+6 max minions
Allows you to detect the souls of creatures, detect valuable resources, and see traps
'I have lost more than you can imagine'");*/
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.rare = ItemRarityID.Purple;
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

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = Color.Orange;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect<SpelunkerEffect>();
            AddEffect<HunterEffect>();
            AddEffect<DangersenseEffect>();
            AddEffect(new MaxMinionSlotEffect(6));
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return incomingItem.type != ModContent.ItemType<MindStone>() || incomingItem.type != ModContent.ItemType<PowerStone>() || incomingItem.type != ModContent.ItemType<RealityStone>() || incomingItem.type != ModContent.ItemType<SoulStone>() || incomingItem.type != ModContent.ItemType<SpaceStone>() || incomingItem.type != ModContent.ItemType<TimeStone>() || incomingItem.type != ModContent.ItemType<InfinityGauntlet>();
        }
    }
}