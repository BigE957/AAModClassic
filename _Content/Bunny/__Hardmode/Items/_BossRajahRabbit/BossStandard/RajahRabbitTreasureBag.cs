using AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.Accessories;
using AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.Weapons;
using AAModClassic._Content.Bunny.__Hardmode.Items.Materials;
using AAModClassic._CrossMod.Thorium.Weapons.Healer;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.BossStandard
{
    public class RajahRabbitTreasureBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.TreasureBags";

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
            Item.expert = true;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }

        //public override int BossBagNPC => ModContent.NPCType<Rajah>();

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
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RajahRabbitMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RajahPelt>(), 1, 15, 31));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RajahRabbitsSashOfVengeance>()));

            List<int> lootTable = [ModContent.ItemType<BaneOfTheBunny>(), ModContent.ItemType<Bunzooka>(), ModContent.ItemType<RoyalScepter>(), ModContent.ItemType<ThePunisher>(), ModContent.ItemType<RabbitcopterWings>()];
            if (ModLoader.TryGetMod("ThoriumMod", out _))
                lootTable.Add(ModContent.ItemType<CarrotFarmer>());

            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable.ToArray()));
        }
    }
}