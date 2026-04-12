using AAModClassic.Items.Materials;
using AAModClassic.Items.Vanity.Mask;
using Terraria;
using Terraria.GameContent.ItemDropRules;
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
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;
        }

        //public override int BossBagNPC => ModContent.NPCType<SerpentHead>();

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (Main.rand.NextBool(10))
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PHMDevArmor();
            }
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SerpentMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SnowMana>(), 1, 15, 20));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ArcticMedallion>()));

            int[] lootTable = { ModContent.ItemType<BlizzardBuster>(), ModContent.ItemType<SerpentSpike>(), ModContent.ItemType<Icepick>(), ModContent.ItemType<SerpentSting>(), ModContent.ItemType<Sickle>(), ModContent.ItemType<SickleShot>(), ModContent.ItemType<SnakeStaff>(), ModContent.ItemType<SubzeroSlasher>() };

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SnowMana>(), 9, 100, 130).OnFailedRoll(ItemDropRule.OneFromOptions(1, lootTable)));
        }
    }
}