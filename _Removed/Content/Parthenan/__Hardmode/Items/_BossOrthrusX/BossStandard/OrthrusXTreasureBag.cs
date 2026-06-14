using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Accessories;
using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.BossStandard;
using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Weapons;
using AAModClassic._Content.Desert.__Hardmode.Items.Materials;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossOrthrusX.Accessories;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossOrthrusX.BossStandard
{
    public class OrthrusXTreasureBag : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.GrabBags.TreasureBags";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
			// Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			Item.maxStack = Item.CommonMaxStack;
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
        //public override int BossBagNPC => Mod.Find<ModNPC>("Orthrus").Type;

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
            int[] lootTable = { ModContent.ItemType<RedOrthrusXMask>(), ModContent.ItemType<BlueOrthrusXMask>() };

            itemLoot.Add(ItemDropRule.OneFromOptions(7, lootTable));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<FulguriteBar>(), 1, 40, 76));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<StormPendant>()));

            itemLoot.Add(ItemDropRule.Common(ItemID.SoulofMight, 1, 25, 40));
        }
    }
}