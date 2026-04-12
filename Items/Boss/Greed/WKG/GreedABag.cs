using AAModClassic.Items.Vanity.Mask;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Greed.WKG
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
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.width = 32;
			Item.height = 36;
			Item.rare = ItemRarityID.Purple;
			Item.expert = true; Item.expertOnly = true;
        }
        //public override int BossBagNPC => ModContent.NPCType<GreedA>();

        public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
        {
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<StoneShell>(), Main.rand.Next(25, 30));
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<CovetiteOre>(), Main.rand.Next(30, 50));
            if (Main.rand.NextBool(7))
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<WKGreedMask>());
            }
            if (Main.rand.NextBool(10))
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PMLDevArmor();
            }
            string[] lootTable = { "OreCannon", "Unearther", "OreStaff", "Earthbreaker" };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>(lootTable[loot]).Type);
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<GravitySphere>());
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<DesireTalisman>());
        }
	}
}