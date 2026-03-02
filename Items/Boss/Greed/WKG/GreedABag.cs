using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Greed.WKG
{
    public class GreedABag : BaseAAItem
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
        public override int BossBagNPC => Mod.Find<ModNPC>("GreedA").Type;

        public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
        {
            player.QuickSpawnItem(Mod.Find<ModItem>("StoneShell").Type, Main.rand.Next(25, 30));
            player.QuickSpawnItem(Mod.Find<ModItem>("CovetiteOre").Type, Main.rand.Next(30, 50));
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("WKGreedMask").Type);
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PMLDevArmor();
            }
            string[] lootTable = { "OreCannon", "Unearther", "OreStaff", "Earthbreaker" };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(Mod.Find<ModItem>(lootTable[loot]).Type);
            player.QuickSpawnItem(Mod.Find<ModItem>("GravitySphere").Type);
            player.QuickSpawnItem(Mod.Find<ModItem>("DesireTalisman").Type);
        }
	}
}