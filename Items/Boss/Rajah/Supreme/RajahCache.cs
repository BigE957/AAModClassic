using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Rajah.Supreme
{
    public class RajahCache : BaseAAItem
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
            Item.width = 32;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;
        }

        public override int BossBagNPC => Mod.Find<ModNPC>("SupremeRajah").Type;

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("RajahMask").Type);
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.SADevArmor();
            }
            player.QuickSpawnItem(Terraria.ModLoader.ModContent.ItemType<ChampionPlate>(), Main.rand.Next(15, 31));
            player.QuickSpawnItem(Mod.Find<ModItem>("RajahCape").Type);
            string[] lootTable = { "Excalihare", "FluffyFury", "RabbitsWrath", "BaneOfTheBunnyEX", "CottonCaneEX", "PunisherEX", "RoyalScepterEX", "BunzookaEX"};
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(Mod.Find<ModItem>(lootTable[loot]).Type);
        }
    }
}