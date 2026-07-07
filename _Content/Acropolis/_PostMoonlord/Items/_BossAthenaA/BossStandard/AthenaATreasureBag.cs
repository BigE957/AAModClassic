using AAModClassic._Content.Acropolis.__Hardmode.Items.Materials;
using AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.Accessories;
using AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.Weapons;
using AAModClassic._Content.Acropolis._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.BossStandard
{
    public class AthenaATreasureBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.TreasureBags";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag (Olympian Athena)");
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
        //public override int BossBagNPC => ModContent.NPCType<AthenaA>();

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (Main.rand.NextBool(10))
            {
                ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
                modPlayer.PMLDevArmor();
            }
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AthenaAMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<GoddessFeather>(), 1, 20, 30));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SkyCrystal>(), 1, 30, 50));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<GoddessHarp>()));

            int[] lootTable = { ModContent.ItemType<HurricaneStone>(), ModContent.ItemType<Olympia>(), ModContent.ItemType<Windfury>(), ModContent.ItemType<GaleForce>() };

            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable));
        }
    }
}