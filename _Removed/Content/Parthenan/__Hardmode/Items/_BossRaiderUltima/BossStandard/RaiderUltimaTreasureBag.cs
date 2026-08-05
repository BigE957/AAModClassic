using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Accessories;
using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.BossStandard;
using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Weapons;
using AAModClassic._Content.Desert.__Hardmode.Items.Materials;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.Accessories;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.Pets;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic._Unofficial.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.BossStandard;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.BossStandard
{
    public class RaiderUltimaTreasureBag : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.GrabBags.TreasureBags";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag (Raider Ultima)");
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
        //public override int BossBagNPC => Mod.Find<ModNPC>("Raider").Type;

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
            LeadingConditionRule unofficialRule = new(new ItemDropRuleConditionUtils.Unofficial());

            unofficialRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RaiderUltimaMask>(), 7));

            itemLoot.Add(unofficialRule);

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CyberneticEgg>(), 7));

            itemLoot.Add(ItemDropRule.Common(ItemID.SoulofFright, 1, 25, 40));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<FulguriteBar>(), 1, 40, 76));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<HoloCape>()));
        }
    }
}