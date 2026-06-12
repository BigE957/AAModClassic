using AAModClassic._Unofficial.Content.Void._PostMoonlord.Items._BossInfinityZero.BossStandard;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero.BossStandard
{
    public class InfinityZeroTreasureBag : ModItem
	{
        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Treasure Cache (Infinity Zero)");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

            Item.ResearchUnlockCount = 3;
            ItemID.Sets.BossBag[Type] = true;
        }

		public override void SetDefaults()
		{
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.width = 36;
			Item.height = 32;
            Item.expert = true;
		}

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }

        public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
            if (Main.rand.NextFloat() < 0.01f)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.SADevArmor();
            }
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            LeadingConditionRule unofficialRule = new(new AAConditions.UnofficialNotExpert());

            unofficialRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<InfinityZeroMask>(), 7));

            itemLoot.Add(unofficialRule);

            int[] lootTable =
            {
                ModContent.ItemType<Genocide>(),
                ModContent.ItemType<Nova>(),
                ModContent.ItemType<Sagittarius>(),
                ModContent.ItemType<TotalDestruction>(),
                ModContent.ItemType<Annihilator>(),
                ModContent.ItemType<InfinityBlade>()
            };

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Infinitium>(), 1, 35, 45));
            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable));
        }
    }
}