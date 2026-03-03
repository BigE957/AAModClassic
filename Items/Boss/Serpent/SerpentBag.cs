using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Serpent
{
    public class SerpentBag : BaseAAItem
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
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;
        }

        //public override int BossBagNPC => Mod.Find<ModNPC>("SerpentHead").Type;

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("SerpentMask").Type);
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PHMDevArmor();
            }
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("SnowMana").Type, Main.rand.Next(15, 20));
            string[] lootTable = { "BlizardBuster", "SerpentSpike", "Icepick", "SerpentSting", "Sickle", "SickleShot", "SnakeStaff", "SubzeroSlasher" };
            int loot = Main.rand.Next(lootTable.Length);
            if (Main.rand.Next(9) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("SnowflakeShuriken").Type, Main.rand.Next(100, 130));
            }
            else
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>(lootTable[loot]).Type);
            }
			player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("ArcticMedallion").Type);			
        }
    }
}