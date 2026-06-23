using AAModClassic._Content.RedMushroom.___PreHardmode.Items._BossMushroomMonarch.Accessories;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items._BossMushroomMonarch.BossStandard
{
    public class MushroomMonarchTreasureBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.TreasureBags";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
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
            Item.height = 36;
            Item.rare = ItemRarityID.Purple;
            Item.expert = true;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }
        //public override int BossBagNPC => ModContent.NPCType<MushroomMonarch>();

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
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<MushroomMonarchMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<HeartyTruffle>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Mushium>(), 1, 30, 40));
        }
    }
}