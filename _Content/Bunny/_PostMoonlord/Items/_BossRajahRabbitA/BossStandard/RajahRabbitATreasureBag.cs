using AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.BossStandard;
using AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Accessories;
using AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Weapons;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.BossStandard
{
    public class RajahRabbitATreasureBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Cache");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;
        }

        //public override int BossBagNPC => ModContent.NPCType<SupremeRajah>();

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (Main.rand.NextBool(10))
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.SADevArmor();
            }
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RajahRabbitMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RajahRabbitsCloakOfSupremacy>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChampionPlate>(), 1, 15, 31));

            int[] lootTable = { ModContent.ItemType<Excalihare>(), ModContent.ItemType<FluffyFury>(), ModContent.ItemType<RabbitsWrath>(), ModContent.ItemType<BaneOfTheSlaughterer>(), ModContent.ItemType<RPG>(), ModContent.ItemType<RoyalStaff>(), ModContent.ItemType<TheAvenger>() };

            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable));
        }
    }
}