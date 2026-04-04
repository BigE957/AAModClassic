using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Toad
{
    public class ToadBag : BaseAAItem
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

        //public override int BossBagNPC => ModContent.NPCType<TruffleToad>();

        public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<ToadMask>());
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PHMDevArmor();
            }
            string[] lootTable = { "MushrockStaff", "ToadTongue", "Todegun" };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>(lootTable[loot]).Type);
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<ToadLeg>());
        }
	}
}