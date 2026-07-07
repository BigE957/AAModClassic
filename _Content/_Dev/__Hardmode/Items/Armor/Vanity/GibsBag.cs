using AAModClassic._Content._Dev.__Hardmode.Items.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity
{
    public class GibsBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.Vanity";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Angry Revenant's Sarcophagus");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Raging Revenant!'");
        }

        public override void SetDefaults()
        {
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true;  
        }

        public override bool CanRightClick()
        {
            return true;
        }

 		public override void RightClick(Player player)
        {
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<GibsHelmet>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<GibsChestplate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<GibsLeggings>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<GibsWings>());
            }
        }
    }
}