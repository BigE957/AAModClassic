using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.BossStandard
{
    public class SoulOfCthulhuTreasureBag : ModItem
	{
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Treasure Cache");
			// Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 36;
			Item.height = 32;
			Item.expert = true;
			//TODOSOC
            //bossBagNPC = ModContent.NPCType<SoC>();
		}
        
        public override bool CanRightClick()
		{
			return true;
		}

        //TODOSOC
        /*
		public override void OpenBossBag(Player player)
		{
            if (Main.rand.NextFloat() < 0.01f)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.SADevArmor();
            }
            player.QuickSpawnItem(Item.GetSource_FromThis(), ModContent.ItemType<EXSoul>());
            string[] lootTable = 
            {
                "CthulhuCannon"
            };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(Item.GetSource_FromThis(), Mod.Find<ModItem>(lootTable[loot]).Type);
        }
        */
	}
}