using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Accessories;
using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Weapons;
using AAModClassic._Content.Desert.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.BossStandard
{
    public class AnubisTreasureBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.TreasureBags";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag (Anubis)");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

            Item.ResearchUnlockCount = 3;
            ItemID.Sets.BossBag[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true;
            Item.rare = ItemRarityID.Red;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }

        //public override int BossBagNPC => ModContent.NPCType<Anubis>();

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
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AnubisMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ForsakenFragment>(), 1, 10, 20));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ArtifactOfJudgement>()));

            int[] lootTable = { ModContent.ItemType<Judgment>(), ModContent.ItemType<NeithsString>(), ModContent.ItemType<DesertStaff>(), ModContent.ItemType<JackalsWrath>(), ModContent.ItemType<Sandthrower>(), ModContent.ItemType<SentryOfTheEye>() };

            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable));
        }
    }
}