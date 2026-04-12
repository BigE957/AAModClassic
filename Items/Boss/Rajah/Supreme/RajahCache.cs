using AAModClassic.Items.Vanity.Mask;
using Terraria;
using Terraria.GameContent.ItemDropRules;
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
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;
        }

        //public override int BossBagNPC => ModContent.NPCType<SupremeRajah>();

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (Main.rand.NextBool(10))
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.SADevArmor();
            }
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RajahMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RajahCape>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChampionPlate>(), 1, 15, 31));

            int[] lootTable = { ModContent.ItemType<Excalihare>(), ModContent.ItemType<FluffyFury>(), ModContent.ItemType<RabbitsWrath>(), ModContent.ItemType<BaneOfTheBunnyEX>(), ModContent.ItemType<BunzookaEX>(), ModContent.ItemType<RoyalScepterEX>(), ModContent.ItemType<PunisherEX>() };

            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable));
        }
    }
}