using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Anarchy
{
    public class AnarchyBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pristine Bag");
            /* Tooltip.SetDefault(@"<right> to open
'All the essentials for impersonating Anarchy! No really it's just Anarchy.'
For the record, Anarchy sprited this himself."); */
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
			player.QuickSpawnItem(ModContent.ItemType<PristineHelmet>());
            player.QuickSpawnItem(ModContent.ItemType<PristineChestplate>());
            player.QuickSpawnItem(ModContent.ItemType<PristineLeggings>());
        }
    }
}