using AAModClassic.Items.Vanity.Mask;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Athena.Olympian
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
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;
            Item.rare = ItemRarityID.Red;
        }

        //public override int BossBagNPC => ModContent.NPCType<AthenaA>();

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (Main.rand.NextBool(7))
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<AthenaAMask>());
            }
            if (Main.rand.NextBool(10))
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PMLDevArmor();
            }
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<GoddessHarp>());
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<GoddessFeather>(), Main.rand.Next(25, 30));
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<SkyCrystal>(), Main.rand.Next(30, 50));
            string[] lootTable = { "HurricaneStone", "Olympia", "Windfury", "GaleForce" };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>(lootTable[loot]).Type);
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<StarChart>());
        }
    }
}