using Terraria;
using Terraria.ModLoader;
using AAMod.Items.Vanity.Alphakip.Shiny;

namespace AAMod.Items.Vanity.Alphakip
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
                player.QuickSpawnItem(ModContent.ItemType<ShinyFishDiverMask>());
                player.QuickSpawnItem(ModContent.ItemType<ShinyFishDiverJacket>());
                player.QuickSpawnItem(ModContent.ItemType<ShinyFishDiverBoots>());
                player.QuickSpawnItem(ModContent.ItemType<Pets.MudkipBallS>());
                if (Main.hardMode)
                {
                    player.QuickSpawnItem(ModContent.ItemType<ShinyKipronWings>());
                }
                return;
            }
			player.QuickSpawnItem(ModContent.ItemType<FishDiverMask>());
            player.QuickSpawnItem(ModContent.ItemType<FishDiverJacket>());
            player.QuickSpawnItem(ModContent.ItemType<FishDiverBoots>());
            player.QuickSpawnItem(ModContent.ItemType<Pets.MudkipBall>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(ModContent.ItemType<KipronWings>());
            }
        }
    }
}