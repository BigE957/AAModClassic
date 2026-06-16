using Terraria;
using Terraria.ModLoader;
using AAModClassic.Items.Vanity.Moon.Shiny;
using AAModClassic._Content._Dev.__Hardmode.Items.Pets;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic.Items.Vanity.Moon
{
    public class MoonBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.Vanity";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lunar Insect's Bag");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Moon Bee!'");
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
            if (Main.hardMode)
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MoonWings>());
            }
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MoonBeeInAJar>());
            if (player.GetModPlayer<AAPlayer>().ShinyCheck())
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MoonHelmetS>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MoonChestplateS>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MoonLeggingsS>());
                return;
            }
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MoonHelmet>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MoonChestplate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MoonLeggings>());
        }
    }
}