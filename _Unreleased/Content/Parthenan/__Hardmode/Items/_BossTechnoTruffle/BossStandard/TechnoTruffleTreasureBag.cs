using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.Accessories;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.Pets;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic._Unreleased.Content.Parthenan.__Hardmode.Items._BossTechnoTruffle.Accessories;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Parthenan.__Hardmode.Items._BossTechnoTruffle.BossStandard
{
    public class TechnoTruffleTreasureBag : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
			// Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 36;
			Item.height = 32;
			Item.rare = ItemRarityID.Cyan;
			Item.expert = true; Item.expertOnly = true;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }
        //public override int BossBagNPC => Mod.Find<ModNPC>("TechnoTruffle").Type;

        public override bool CanRightClick()
		{
			return true;
		}

        public override void RightClick(Player player)
        {
            if (Main.rand.NextBool(10))
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.HMDevArmor();
            }
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<TechnoTruffleMask>(), 7));

            itemLoot.Add(ItemDropRule.OneFromOptions(1, ItemID.SoulofFright, ItemID.SoulofSight, ItemID.SoulofMight));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<MetallicTruffle>()));
        }
    }
}