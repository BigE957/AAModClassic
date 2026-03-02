using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Dallin
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
			player.QuickSpawnItem(ItemID.Fez);	
			player.QuickSpawnItem(ItemID.TheDoctorsShirt);		
			player.QuickSpawnItem(ItemID.TheDoctorsPants);
			player.QuickSpawnItem(ItemID.ReflectiveDye, 3);
            if (Main.hardMode)
            {
                player.QuickSpawnItem(ItemID.Hoverboard);
            }
            player.QuickSpawnItem(Mod.Find<ModItem>("K9Collar").Type);
        }
    }
}