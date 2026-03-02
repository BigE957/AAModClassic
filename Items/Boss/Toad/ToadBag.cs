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
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 32;
			Item.height = 36;
			Item.rare = ItemRarityID.Purple;
			Item.expert = true; Item.expertOnly = true;
		}

        public override int BossBagNPC => Mod.Find<ModNPC>("TruffleToad").Type;

        public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("ToadMask").Type);
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PHMDevArmor();
            }
            string[] lootTable = { "MushrockStaff", "ToadTongue", "Todegun" };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(Mod.Find<ModItem>(lootTable[loot]).Type);
            player.QuickSpawnItem(Mod.Find<ModItem>("ToadLeg").Type);
        }
	}
}