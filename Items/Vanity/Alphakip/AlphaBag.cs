using Terraria;
using Terraria.ModLoader;
using AAModClassic.Items.Pets;
using AAModClassic.Items.Vanity.Alphakip.Shiny;

namespace AAModClassic.Items.Vanity.Alphakip
{
    public class AlphaBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mud Fish's Bag");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Fish King!'");
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
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ShinyFishDiverMask>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ShinyFishDiverJacket>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ShinyFishDiverBoots>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MudkipBallS>());
                if (Main.hardMode)
                {
                    player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ShinyKipronWings>());
                }
                return;
            }
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<FishDiverMask>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<FishDiverJacket>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<FishDiverBoots>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MudkipBall>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<KipronWings>());
            }
        }
    }
}