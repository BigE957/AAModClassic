using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Djinn
{
    public class DjinnBag : BaseAAItem
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
        }
        //public override int BossBagNPC => ModContent.NPCType<Djinn>();

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<DjinnMask>());
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PHMDevArmor();
            }
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<DesertMana>(), Main.rand.Next(15, 20));
            string[] lootTable = { "Djinnerang", "SandLamp", "SandScepter", "SandstormCrossbow", "SultanScimitar" };
            int loot = Main.rand.Next(lootTable.Length);
            if (Main.rand.Next(9) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<Sandagger>(), Main.rand.Next(100, 130));
            }
            else
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>(lootTable[loot]).Type);
            }
			player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<SandstormMedallion>());		
        }
    }
}