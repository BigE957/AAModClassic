using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.Accessories;
using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.Weapons;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.BossStandard
{
    public class GripsOfChaosTreasureBag : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.GrabBags.TreasureBags";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Treasure Bag (Grips of Chaos)");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

            Item.ResearchUnlockCount = 3;
            ItemID.Sets.BossBag[Type] = true;
            ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
        }

		public override void SetDefaults()
		{
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.width = 36;
			Item.height = 32;
			Item.rare = ItemRarityID.Cyan;
			Item.expert = true;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }
        //public override int BossBagNPC => ModContent.NPCType<GripOfChaosBlue>();

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
            var maskRule = ItemDropRule.Common(ModContent.ItemType<MireGripMask>(), 7);
            maskRule.OnFailedRoll(ItemDropRule.Common(ModContent.ItemType<InfernoGripMask>(), 6));
            itemLoot.Add(maskRule);

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ClawBaton>(), 3));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AbyssiumOre>(), 1, 25, 56));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<IncineriteOre>(), 1, 25, 56));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ClawOfChaos>()));
        }
	}
}