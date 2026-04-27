using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Items.Vanity.Hallam.Shiny;

namespace AAModClassic.Items.Vanity.Hallam
{
    public class MagiciansHat : BaseAAItem
    {
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
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ShinyHalHat>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ShinyHalTux>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ShinyHalTux>());
                if (Main.rand.NextBool(10))
                {
                    player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.GoldBunny);
                }
                return;
            }
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<HalHat>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<HalTux>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<HalTrousers>());

            if (Main.rand.NextBool(10))
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.Bunny);
            }
        }
    }
}