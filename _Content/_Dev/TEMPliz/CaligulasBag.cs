using AAModClassic._Content._Dev.__Hardmode.Items.Pets;
using AAModClassic._Content._Dev.TEMPliz.Cat;
using AAModClassic._Content._Dev.TEMPliz.Dragon;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.TEMPliz
{
    public class CaligulasBag : BaseAAItem
    {
        public new string LocalizationCategory => "Items.GrabBags.Vanity";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dark Dragon's Bag");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Dragon Queen!'");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 1;
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
            if (Main.rand.Next(2) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CaligulasHelmet>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CaligulasChestplate>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CaligulasLeggings>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<TangentiallyRelatedScarf>());
                if (Main.hardMode)
                {
                    player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CaligulasWings>());
                    player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.TwilightDye);
                }
            }
            else
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<AquariumHelmet>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<AquariumChestplate>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<AquariumLeggings>());
                if (Main.hardMode)
                {
                    player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<AquariumWings>());
                    player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.TwilightDye);
                }
            }
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<RoyalStar>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.TwilightHairDye);
        }
    }
}