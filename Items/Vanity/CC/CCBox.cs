using AAModClassic.Items.Accessories.Wings;
using AAModClassic.Items.Dev.DevTile.Tiles;
using AAModClassic.Items.Vanity.CC.Shiny;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.CC
{
    public class CCBox : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mire Manic's Cardboard Box");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Dread Devotee!'");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;
            Item.createTile = ModContent.TileType<CCMireBox_Tile>(); 
        }

        public override bool CanRightClick()
        {
            return true;
        }

 		public override void RightClick(Player player)
        {
            if (player.GetModPlayer<AAPlayer>().ShinyCheck())
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<Shiny.ShinyCCHood>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ShinyCCRobe>());
                if (Main.hardMode)
                {
                    player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MagmancerWings>());
                }
                return;
            }
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CCHood>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CCRobe>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<AquamancerWings>());
            }
        }
    }
}