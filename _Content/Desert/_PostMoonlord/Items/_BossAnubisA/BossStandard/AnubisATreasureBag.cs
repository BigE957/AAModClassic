using AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.Accessories;
using AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.Weapons;
using AAModClassic._Content.Desert._PostMoonlord.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.BossStandard
{
    public class AnubisATreasureBag : BaseAAItem
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
            Item.width = 32;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;
            Item.rare = ItemRarityID.Red;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }

        //public override int BossBagNPC => ModContent.NPCType<ForsakenAnubis>();

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
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AnubisAMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ArtifactOfGuilt>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulFragment>(), 1, 10, 20));

            int[] lootTable = { ModContent.ItemType<Verdict>(), ModContent.ItemType<Lifeline>(), ModContent.ItemType<ForsakenStaff>(), ModContent.ItemType<Soulsplitter>(), ModContent.ItemType<CursedFlamefury>(), ModContent.ItemType<HorusCane>() };

            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable));
        }
    }
}