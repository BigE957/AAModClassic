using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Apawn
{
    public class ApawnEgg : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Surprise Egg");
            // Tooltip.SetDefault("<right> to open \n'Its a plastic egg. A REEEEEEEALLY big one.!'");
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
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ApawnHelm>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ApawnPlate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ApawnBoots>());
        }
    }
}