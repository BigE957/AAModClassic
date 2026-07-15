using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.Accessories;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.Pets;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRetriever.Accessories;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRetriever.BossStandard
{
    public class RetrieverTreasureBag : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.GrabBags.TreasureBags";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag (Retriever)");
			// Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
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
        //public override int BossBagNPC => Mod.Find<ModNPC>("Retriever").Type;

        public override bool CanRightClick()
		{
			return true;
		}

        public override void RightClick(Player player)
        {
            if (Main.rand.NextBool(10))
            {
                ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
                modPlayer.HMDevArmor();
            }
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RetrieverMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ItemID.SoulofSight, 1, 25, 40));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<FulguriteBar>(), 1, 40, 76));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<StormClaw>()));
        }
    }
}