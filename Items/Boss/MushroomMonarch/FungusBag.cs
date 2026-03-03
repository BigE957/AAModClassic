using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.MushroomMonarch
{
    public class FungusBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 36;
            Item.rare = ItemRarityID.Purple;
            Item.expert = true; Item.expertOnly = true;
        }
        //public override int BossBagNPC => Mod.Find<ModNPC>("FeudalFungus").Type;

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Terraria.ModLoader.ModContent.ItemType<Vanity.Mask.FungusMask>());
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PHMDevArmor();
            }
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("GlowingMushium").Type, Main.rand.Next(30, 40));
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("MagicTruffle").Type);
        }
    }
}