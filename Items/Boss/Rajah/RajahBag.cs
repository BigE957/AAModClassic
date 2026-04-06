using AAModClassic.CrossMod;
using AAModClassic.Items.Thorium.Healer;
using AAModClassic.Items.Vanity.Mask;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Rajah
{
    public class RajahBag : BaseAAItem
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

        //public override int BossBagNPC => ModContent.NPCType<Rajah>();

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<RajahMask>());
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PMLDevArmor();
            }
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Terraria.ModLoader.ModContent.ItemType<RajahPelt>(), Main.rand.Next(15, 31));
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<RajahPelt>(), Main.rand.Next(20, 25));
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<RajahSash>());
            string[] lootTable = { "BaneOfTheBunny", "Bunzooka", "Punisher", "RabbitcopterEars", "RoyalScepter" };
            int loot = Main.rand.Next(lootTable.Length);
            if (Main.rand.Next(6) == 1 && ModSupport.GetMod("ThoriumMod") != null)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<CarrotFarmer>());
            }
            else
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>(lootTable[loot]).Type);
            }
        }
    }
}