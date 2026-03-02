using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Athena
{
    public class AthenaBag : BaseAAItem
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
            Item.rare = 10;
        }

        public override int BossBagNPC => Mod.Find<ModNPC>("Athena").Type;

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("AthenaMask").Type);
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PPDevArmor();
            }
            player.QuickSpawnItem(Mod.Find<ModItem>("SeraphHarp").Type);
            player.QuickSpawnItem(Mod.Find<ModItem>("GoddessFeather").Type, Main.rand.Next(25, 30));
            string[] lootTable = { "DivineWindCharm", "GaleOfWings", "RazorwindLongbow", "SkycutterKopis", "OlympianWings" };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(Mod.Find<ModItem>(lootTable[loot]).Type);
        }
    }
}