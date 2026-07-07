using AAModClassic._Content.Void.___PreHardmode.Items._BossSagittarius.Accessories;
using AAModClassic._Content.Void.___PreHardmode.Items._BossSagittarius.Weapons;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items._BossSagittarius.BossStandard
{
    public class SagittariusTreasureBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.TreasureBags";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag (Sagittarius)");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

            Item.ResearchUnlockCount = 3;
            ItemID.Sets.BossBag[Type] = true;
            ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true;
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
                ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
                modPlayer.PHMDevArmor();
            }		
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SagittariusMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SagittariusShield>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DoomiteBar>(), 1, 35, 45));

            int[] lootTable = { ModContent.ItemType<SagittariusCore>(), ModContent.ItemType<NeutronRod>(), ModContent.ItemType<SagittariusLeg>() };

            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable));
        }
    }
}