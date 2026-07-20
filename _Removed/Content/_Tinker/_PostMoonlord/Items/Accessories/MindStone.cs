using AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.Accessories;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories
{
    [AutoloadEquip(EquipType.Face)]
    public class MindStone : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            /*DisplayName.SetDefault("Mind Stone");
            Tooltip.SetDefault(
@"Reduces mana consumption by 50%
+200 Mana
'It's simple Calculus'");*/
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 24;
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
                    line2.OverrideColor = Color.Yellow;
                }
            }
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new MaxManaEffect(200));
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                AddEffect(new ManaCostMultiplierEffect(0.50f));
            else
                AddEffect(new ManaCostEffect(-0.50f));
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return incomingItem.type != ModContent.ItemType<MindStone>() || incomingItem.type != ModContent.ItemType<PowerStone>() || incomingItem.type != ModContent.ItemType<RealityStone>() || incomingItem.type != ModContent.ItemType <SoulStone>() || incomingItem.type != ModContent.ItemType<SpaceStone>() || incomingItem.type != ModContent.ItemType<TimeStone>() || incomingItem.type != ModContent.ItemType<InfinityGauntlet>();
        }
    }
}