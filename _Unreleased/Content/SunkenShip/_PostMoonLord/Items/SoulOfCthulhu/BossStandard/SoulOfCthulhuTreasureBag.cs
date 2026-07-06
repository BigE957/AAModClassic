using AAModClassic._Unofficial.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.BossStandard;
using AAModClassic._Unofficial.Content.SunkenShip._PostMoonlord.Items._BossSoulOfCthulhu.BossStandard;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.BossStandard
{
    public class SoulOfCthulhuTreasureBag : ModItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.GrabBags.TreasureBags";
        
        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Treasure Cache (Soul of Cthulhu)");
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
            LeadingConditionRule unofficialRule = new(new AAConditions.Unofficial());

            unofficialRule.OnSuccess(ItemDropRule.OneFromOptions(7, ModContent.ItemType<SoulOfCthulhuMask>(), ModContent.ItemType<SoulOfCthulhuAMask>()));

            itemLoot.Add(unofficialRule);

            int[] lootTable =
            {
                ModContent.ItemType<RealityAnchor>(),
                ModContent.ItemType<SquidStorm>(),
                ModContent.ItemType<CthulhuCannon>(),
                ModContent.ItemType<GalacticStormspike>(),
            };

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RealityBar>(), 1, 35, 45));

            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable));
        }
    }
}