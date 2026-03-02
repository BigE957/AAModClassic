using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Grips
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class ClawOfChaos : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 30;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = -12;
            Item.expert = true; Item.expertOnly = true;
            Item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Claw of Chaos");
            // Tooltip.SetDefault("For every hit you land on an enemy, 5 true damage (damage unassigned to any class) is dealt");
        }
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<AAPlayer>().clawsOfChaos = true;
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ModContent.ItemType<BulwarkOfChaos>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}