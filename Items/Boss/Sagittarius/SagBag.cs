using AAModClassic.___Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Items.Vanity.Mask;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Sagittarius
{
    public class SagBag : BaseAAItem
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

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }

        //public override int BossBagNPC => ModContent.NPCType<Sag>();

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
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SagMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SagShield>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DoomiteBar>(), 1, 35, 45));

            int[] lootTable = { ModContent.ItemType<SagCore>(), ModContent.ItemType<NeutronStaff>(), ModContent.ItemType<Legg>() };

            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable));
        }
    }
}