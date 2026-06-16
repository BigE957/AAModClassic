using Terraria;
using Terraria.ModLoader;
using AAModClassic.Items.Vanity.Pluto.Shiny;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic.Items.Vanity.Pluto
{
    public class PlutoBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.Vanity";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Outer God's Bag");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Dwarf God!'");
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
            if (player.GetModPlayer<AAPlayer>().ShinyCheck())
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<PlutoHelmetS>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<PlutoChestplateS>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<PlutoLeggings>());
                if (Main.hardMode)
                {
                    //player.QuickSpawnItem(ModContent.ItemType<>());
                }
                return;
            }
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<PlutoHelmet>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<PlutoChestplate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<PlutoLeggings>());
            if (Main.hardMode)
            {
                //player.QuickSpawnItem(ModContent.ItemType<>());
            }
        }
    }
}