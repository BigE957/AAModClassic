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
    public class PowerStone : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            /*DisplayName.SetDefault("Power Stone");
            Tooltip.SetDefault(
@"Multiplies your attack power by 40%
'Fun isnt something one considers when balancing the universe'");*/
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(4, 8));
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 78;
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
                    line2.OverrideColor = Color.Purple;
                }
            }
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Generic) += .40f;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return incomingItem.type != ModContent.ItemType<MindStone>() || incomingItem.type != ModContent.ItemType<PowerStone>() || incomingItem.type != ModContent.ItemType<RealityStone>() || incomingItem.type != ModContent.ItemType<SoulStone>() || incomingItem.type != ModContent.ItemType<SpaceStone>() || incomingItem.type != ModContent.ItemType<TimeStone>() || incomingItem.type != ModContent.ItemType<InfinityGauntlet>();
        }
    }
    
}