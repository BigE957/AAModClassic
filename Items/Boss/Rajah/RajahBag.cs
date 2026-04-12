using AAModClassic.CrossMod;
using AAModClassic.Items.Thorium.Healer;
using AAModClassic.Items.Vanity.Mask;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Rajah
{
    public class RajahBag : BaseAAItem
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
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;
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
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RajahMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RajahPelt>(), 1, 15, 31));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RajahSash>()));

            List<int> lootTable = [ModContent.ItemType<BaneOfTheBunny>(), ModContent.ItemType<Bunzooka>(), ModContent.ItemType<RoyalScepter>(), ModContent.ItemType<Punisher>(), ModContent.ItemType<RabbitcopterEars>()];
            if (ModSupport.GetMod("ThoriumMod") != null)
                lootTable.Add(ModContent.ItemType<CarrotFarmer>());

            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable.ToArray()));
        }
    }
}