using AAModClassic._Content.Inferno.___PreHardmode.Items._BossBroodmother.Accessories;
using AAModClassic._Content.Inferno.___PreHardmode.Items._BossBroodmother.Pets;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Pets;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Weapons;
using AAModClassic.CrossMod;
using AAModClassic.Items.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items._BossBroodmother.BossStandard
{
    public class BroodmotherTreasureBag : BaseAAItem
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
			Item.width = 36;
			Item.height = 32;
			Item.expert = true; Item.expertOnly = true;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }
        //public override int BossBagNPC => ModContent.NPCType<Broodmother>();

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
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BroodmotherMask>(), 7));

            if (ContentReplacementSystem.NeedToReplaceContent)
                itemLoot.Add(ItemDropRule.OneFromOptions(1, ModContent.ItemType<Pyrosphere>(), ModContent.ItemType<Firebuster>(), ModContent.ItemType<Volley>(), ModContent.ItemType<DragonSoul>(), ModContent.ItemType<DragonsGuard>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScorchedEgg>(), 7));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DragontamersCloak>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScorchedScale>(), 1, 50, 100));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<IncineriteOre>(), 1, 75, 125));
        }
	}
}