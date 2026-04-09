using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic.Items.Vanity.Tied
{
    public class OldMagiciansHat : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Old Magician's Top Hat");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Dapper Bone Man!'");
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
                if (Main.rand.Next(10) == 0)
                {
                    player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.GoldBunny);
                }
            }
            else
            {
                if (Main.rand.Next(10) == 0)
                {
                    player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.Bunny);
                }
            }
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<TiedsMask>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<TiedsSuit>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<TiedsLeggings>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.BoneWings);
            }

        }
    }
}