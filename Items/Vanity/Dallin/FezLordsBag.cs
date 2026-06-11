using AAModClassic._Content._Dev.__Hardmode.Items.Pets;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Dallin
{
    public class FezLordsBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fez Lord's Bag");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Fez Lord!'");
        }

        public override void SetDefaults()
        {
            Item.maxStack = Item.CommonMaxStack;
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
			player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.Fez);	
			player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.TheDoctorsShirt);		
			player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.TheDoctorsPants);
			player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.ReflectiveDye, 3);
            if (Main.hardMode)
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.Hoverboard);
            }
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<K9Collar>());
        }
    }
}