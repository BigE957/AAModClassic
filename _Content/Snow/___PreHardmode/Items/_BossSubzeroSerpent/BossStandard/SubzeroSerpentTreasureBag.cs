using AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Accessories;
using AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Tools;
using AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Weapons;
using AAModClassic._Content.Snow.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.BossStandard
{
    public class SubzeroSerpentTreasureBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.TreasureBags";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag (Subzero Serpent)");
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

        //public override int BossBagNPC => ModContent.NPCType<SerpentHead>();

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
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SubzeroSerpentMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SnowMana>(), 1, 15, 20));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ArcticMedallion>()));

            int[] lootTable = { ModContent.ItemType<BlizzardBuster>(), ModContent.ItemType<SerpentSpike>(), ModContent.ItemType<Icepick>(), ModContent.ItemType<SerpentsSting>(), ModContent.ItemType<Icicle>(), ModContent.ItemType<Sickleshot>(), ModContent.ItemType<SnakeStaff>(), ModContent.ItemType<SubzeroSlasher>() };

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SnowMana>(), 9, 100, 130).OnFailedRoll(ItemDropRule.OneFromOptions(1, lootTable)));
        }
    }
}