using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.CC
{
    public class CCBox : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mire Manic's Cardboard Box");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Dread Devotee!'");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;
            Item.createTile = Mod.Find<ModTile>("CCMireBox").Type; 
        }

        public override bool CanRightClick()
        {
            return true;
        }

 		public override void RightClick(Player player)
        {
            if (player.GetModPlayer<AAPlayer>().ShinyCheck())
            {
                player.QuickSpawnItem(ModContent.ItemType<Shiny.ShinyCCHood>());
                player.QuickSpawnItem(ModContent.ItemType<Shiny.ShinyCCRobe>());
                if (Main.hardMode)
                {
                    player.QuickSpawnItem(ModContent.ItemType<Accessories.Wings.MagmancerWings>());
                }
                return;
            }
            player.QuickSpawnItem(ModContent.ItemType<CCHood>());
            player.QuickSpawnItem(ModContent.ItemType<CCRobe>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(ModContent.ItemType<Accessories.Wings.AquamancerWings>());
            }
        }
    }
}