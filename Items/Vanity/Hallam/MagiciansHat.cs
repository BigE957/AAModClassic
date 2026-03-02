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
            if (player.GetModPlayer<AAPlayer>().ShinyCheck())
            {
                player.QuickSpawnItem(ModContent.ItemType<ShinyHalHat>());
                player.QuickSpawnItem(ModContent.ItemType<ShinyHalTux>());
                player.QuickSpawnItem(ModContent.ItemType<ShinyHalTux>());
                if (Main.rand.Next(10) == 0)
                {
                    player.QuickSpawnItem(ItemID.GoldBunny);
                }
                return;
            }
			player.QuickSpawnItem(ModContent.ItemType<HalHat>());
            player.QuickSpawnItem(ModContent.ItemType<HalTux>());
            player.QuickSpawnItem(ModContent.ItemType<HalTrousers>());

            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(ItemID.Bunny);
            }
        }
    }
}