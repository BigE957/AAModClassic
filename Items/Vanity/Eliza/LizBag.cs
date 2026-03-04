using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Eliza
{
    public class LizBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dark Dragon's Bag");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Dragon Queen!'");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;  
        }

        public override bool CanRightClick()
        {
            return true;
        }

 		public override void RightClick(Player player)
        {
            if (Main.rand.Next(2) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), Mod.Find<ModItem>("LizEars").Type);
                player.QuickSpawnItem(Item.GetSource_Loot(), Mod.Find<ModItem>("LizShirt").Type);
                player.QuickSpawnItem(Item.GetSource_Loot(), Mod.Find<ModItem>("LizBoots").Type);
                player.QuickSpawnItem(Item.GetSource_Loot(), Mod.Find<ModItem>("LizScarf").Type);
                if (Main.hardMode)
                {
                    player.QuickSpawnItem(Item.GetSource_Loot(), Mod.Find<ModItem>("NightingaleWings").Type);
                    player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.TwilightDye);
                }
            }
            else
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), Mod.Find<ModItem>("LizHood").Type);
                player.QuickSpawnItem(Item.GetSource_Loot(), Mod.Find<ModItem>("LizRobes").Type);
                player.QuickSpawnItem(Item.GetSource_Loot(), Mod.Find<ModItem>("LizSkirt").Type);
                if (Main.hardMode)
                {
                    player.QuickSpawnItem(Item.GetSource_Loot(), Mod.Find<ModItem>("DragonWings").Type);
                    player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.TwilightDye);
                }
            }
            player.QuickSpawnItem(Item.GetSource_Loot(), Mod.Find<ModItem>("RoyalStar").Type);
            player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.TwilightHairDye);
        }
    }
}