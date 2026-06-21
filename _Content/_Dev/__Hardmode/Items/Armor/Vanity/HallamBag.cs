using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity
{
    public class HallamBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.Vanity";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Magician's Top Hat");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Mad Cat!'");
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
            if (player.GetModPlayer<AAPlayer>().ShinyCheck())
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<HallamHelmetS>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<HallamChestplateS>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<HallamChestplateS>());
                if (Main.rand.NextBool(10))
                {
                    player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.GoldBunny);
                }
                return;
            }
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<HallamHelmet>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<HallamChestplate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<HallamLeggings>());

            if (Main.rand.NextBool(10))
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.Bunny);
            }
        }
    }
}