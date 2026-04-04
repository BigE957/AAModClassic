using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Sagittarius
{
    public class SagBag : BaseAAItem
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

        //public override int BossBagNPC => ModContent.NPCType<Sag>();

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<SagMask>());
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PHMDevArmor();
            }
            string[] lootTable = { "SagCore", "NeutronStaff", "Legg" };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>(lootTable[loot]).Type);
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<Doomite>(), Main.rand.Next(35, 45));
			player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<SagShield>());			
        }
    }
}