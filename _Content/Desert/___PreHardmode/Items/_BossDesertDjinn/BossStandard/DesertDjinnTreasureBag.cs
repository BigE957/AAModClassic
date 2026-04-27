using AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn.Accessories;
using AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn.Weapons;
using AAModClassic._Content.Desert.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn.BossStandard
{
    public class DesertDjinnTreasureBag : BaseAAItem
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
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
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
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DesertDjinnMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DesertMana>(), 1, 15, 20));

            int[] lootTable = { ModContent.ItemType<Djinnerang>(), ModContent.ItemType<SandLamp>(), ModContent.ItemType<SandScepter>(), ModContent.ItemType<SandstormCrossbow>(), ModContent.ItemType<SultansScimitar>() };

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Sandagger>(), 6, 100, 130).OnFailedRoll(ItemDropRule.OneFromOptions(1, lootTable)));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SandstormMedallion>()));
        }
    }
}