using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories
{
    [AutoloadEquip(EquipType.Face)]
    public class SpaceStone : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            /*DisplayName.SetDefault("Space Stone");
            Tooltip.SetDefault(
@"Allows you to teleport with the hook funtion like with the rod of discord
You are immune to the Chaos State Debuff
'But this...Does put a smile on my face'");*/
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 24;
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
            base.ModifyTooltips(list);
            
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = Color.Cyan;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect<SpaceStoneEffect>();
            AddEffect(new BuffImmunityEffect(BuffID.ChaosState));
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return incomingItem.type != ModContent.ItemType<MindStone>() || incomingItem.type != ModContent.ItemType<PowerStone>() || incomingItem.type != ModContent.ItemType<RealityStone>() || incomingItem.type != ModContent.ItemType<SoulStone>() || incomingItem.type != ModContent.ItemType<SpaceStone>() || incomingItem.type != ModContent.ItemType<TimeStone>() || incomingItem.type != ModContent.ItemType<InfinityGauntlet>();
        }
    }
}