using AAModClassic.Items.Accessories.Wings;
using AAModClassic.Items.Vanity.Mask;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Athena
{
    public class AthenaBag : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

            Item.ResearchUnlockCount = 3;
            ItemID.Sets.BossBag[Type] = true;
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

        //public override int BossBagNPC => ModContent.NPCType<Athena>();

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (Main.rand.NextBool(10))
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PPDevArmor();
            }
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AthenaMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SeraphHarp>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<GoddessFeather>(), 1, 25, 30));

            int[] lootTable = { ModContent.ItemType<DivineWindCharm>(), ModContent.ItemType<GaleOfWings>(), ModContent.ItemType<RazorwindLongbow>(), ModContent.ItemType<SkycutterKopis>(), ModContent.ItemType<OlympianWings>() };

            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable));
        }
    }
}