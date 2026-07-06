using AAModClassic._Content.Hoard.__Hardmode.Items.Materials;
using AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.Accessories;
using AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.Tools;
using AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.Weapons;
using AAModClassic._Content.Hoard._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.BossStandard
{
    public class GreedATreasureBag : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.GrabBags.TreasureBags";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag (Worm King, Greed)");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

            Item.ResearchUnlockCount = 3;
            ItemID.Sets.BossBag[Type] = true;
        }

		public override void SetDefaults()
		{
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.width = 32;
			Item.height = 36;
			Item.rare = ItemRarityID.Purple;
			Item.expert = true;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }
        //public override int BossBagNPC => ModContent.NPCType<GreedA>();

        public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
        {
            if (Main.rand.NextBool(10))
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PMLDevArmor();
            }
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<GreedAMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<TalismanOfDesire>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<StoneShell>(), 1, 25, 30));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CovetiteOre>(), 1, 30, 50));

            int[] lootTable = { ModContent.ItemType<OreCannon>(), ModContent.ItemType<Unearther>(), ModContent.ItemType<Earthbreaker>(), ModContent.ItemType<OreStaff>() };

            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable));
        }
	}
}