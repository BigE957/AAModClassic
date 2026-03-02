using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Greed
{
    public class GreedBag : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
			// Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 32;
			Item.height = 36;
			Item.rare = 11;
			Item.expert = true; Item.expertOnly = true;
        }
        public override int BossBagNPC => Mod.Find<ModNPC>("Greed").Type;

        public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
        {
            player.QuickSpawnItem(Mod.Find<ModItem>("StoneShell").Type, Main.rand.Next(25, 30));
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("GreedMask").Type);
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PPDevArmor();
            }
            string[] lootTable = { "GildedGlock", "Miner", "StoneSlammer", "GoldDigger"};
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(Mod.Find<ModItem>(lootTable[loot]).Type);
            player.QuickSpawnItem(Mod.Find<ModItem>("CovetiteCoin").Type, Main.rand.Next(60, 150));
            player.QuickSpawnItem(Mod.Find<ModItem>("DesireCharm").Type);
        }
	}
}