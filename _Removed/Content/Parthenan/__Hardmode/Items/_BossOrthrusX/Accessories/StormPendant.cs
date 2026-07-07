using AAModClassic._Content.Mire.___PreHardmode.Items._BossHydra.Accessories;
using AAModClassic._Removed.Content._Tinker.___PreHardmode.Items.Accessories;
using AAModClassic._Removed.Content._Tinker.__Hardmode.Items.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossOrthrusX.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class StormPendant : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Storm Pendant");
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 50;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.accessory = true;
            Item.expert = true;
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Generic) += .10f;
            damageMap.GetAttackSpeed(DamageClass.Melee) += .10f;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ModContent.ItemType<DragonSerpentNecklace>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == ModContent.ItemType<StormCharm>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == ModContent.ItemType<HydraPendant>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}