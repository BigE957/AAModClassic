using AAModClassic.Items.Materials;
using AAModClassic.Items.Vanity.Mask;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Djinn
{
    public class DjinnBag : BaseAAItem
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
        }
        //public override int BossBagNPC => ModContent.NPCType<Djinn>();

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
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DjinnMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DesertMana>(), 1, 15, 20));

            int[] lootTable = { ModContent.ItemType<Djinnerang>(), ModContent.ItemType<SandLamp>(), ModContent.ItemType<SandScepter>(), ModContent.ItemType<SandstormCrossbow>(), ModContent.ItemType<SultanScimitar>() };

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Sandagger>(), 6, 100, 130).OnFailedRoll(ItemDropRule.OneFromOptions(1, lootTable)));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SandstormMedallion>()));
        }
    }
}