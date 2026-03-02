using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Athena.Olympian
{
    public class AthenaABag : BaseAAItem
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
            Item.rare = ItemRarityID.Red;
        }

        public override int BossBagNPC => Mod.Find<ModNPC>("AthenaA").Type;

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("AthenaAMask").Type);
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PMLDevArmor();
            }
            player.QuickSpawnItem(Mod.Find<ModItem>("GoddessHarp").Type);
            player.QuickSpawnItem(Mod.Find<ModItem>("GoddessFeather").Type, Main.rand.Next(25, 30));
            player.QuickSpawnItem(Mod.Find<ModItem>("SkyCrystal").Type, Main.rand.Next(30, 50));
            string[] lootTable = { "HurricaneStone", "Olympia", "Windfury", "GaleForce" };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(Mod.Find<ModItem>(lootTable[loot]).Type);
            player.QuickSpawnItem(Mod.Find<ModItem>("StarChart").Type);
        }
    }
}