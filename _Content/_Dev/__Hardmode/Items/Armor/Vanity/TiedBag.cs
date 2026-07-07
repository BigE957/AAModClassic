using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity
{
    public class TiedBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.Vanity";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Old Magician's Top Hat");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Dapper Bone Man!'");
        }

        public override void SetDefaults()
        {
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true;  
        }

        public override bool CanRightClick()
        {
            return true;
        }

 		public override void RightClick(Player player)
        {
            if (player.GetModPlayer<ZAAPlayer>().ShinyCheck())
            {
                if (Main.rand.NextBool(10))
                {
                    player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.GoldBunny);
                }
            }
            else
            {
                if (Main.rand.NextBool(10))
                {
                    player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.Bunny);
                }
            }
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<TiedHelmet>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<TiedChestplate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<TiedLeggings>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.BoneWings);
            }

        }
    }
}