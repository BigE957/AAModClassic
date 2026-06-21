using AAModClassic._Content.Inferno.__Hardmode.Items.Accessories;
using AAModClassic._Content.Mire.__Hardmode.Items.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity
{
    public class CCBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.Vanity";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mire Manic's Cardboard Box");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Dread Devotee!'");
        }

        public override void SetDefaults()
        {
            Item.maxStack = Item.CommonMaxStack;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true;
            Item.createTile = ModContent.TileType<CCBag_Tile>(); 
        }

        public override bool CanRightClick()
        {
            return true;
        }

 		public override void RightClick(Player player)
        {
            if (player.GetModPlayer<AAPlayer>().ShinyCheck())
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CCHelmetS>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CCChestplateS>());
                if (Main.hardMode)
                {
                    player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MagmancerWings>());
                }
                return;
            }
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CCHelmet>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CCChestplate>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<AquamancerWings>());
            }
        }
    }
}