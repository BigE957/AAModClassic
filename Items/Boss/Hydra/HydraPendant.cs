using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Hydra
{
    [AutoloadEquip(EquipType.Neck)]
    public class HydraPendant : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hydra Pendant");
            // Tooltip.SetDefault(@"7% Increased damage");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 50;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.accessory = true;
            Item.expert = true; Item.expertOnly = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += .07f;
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
                }
            }
            return true;
        }
    }
}