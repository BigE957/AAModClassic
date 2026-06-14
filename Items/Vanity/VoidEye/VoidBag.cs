using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.VoidEye
{
    public class VoidBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.Vanity";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Gate");
            /* Tooltip.SetDefault(@"<right> to open
'All the essentials for impersonating the Dark Watcher!'"); */
        }

        public override void SetDefaults()
        {
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;  
        }

        public override bool CanRightClick()
        {
            return true;
        }

 		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<VoidEyeHelm>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<VoidEyePlate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<VoidEyeBoots>());
        }
    }
}