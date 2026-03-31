using Terraria;
using Microsoft.Xna.Framework; 
using Microsoft.Xna.Framework.Graphics; 
using Terraria.ModLoader;
using AAModClassic;

namespace AAModClassic._Unreleased.Items.Boss.SoC
{
    public class SoCCache : ModItem
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
            //bossBagNPC = Mod.Find<ModNPC>("SoC").Type;
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
            player.QuickSpawnItem(Item.GetSource_FromThis(), Mod.Find<ModItem>("EXSoul").Type);
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