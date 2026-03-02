using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Fazer
{
    public class WetFurrbag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wet Furrbag");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Funloving Fox!'");
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
            player.QuickSpawnItem(Mod.Find<ModItem>("SammyWig").Type);
            player.QuickSpawnItem(Mod.Find<ModItem>("SammySweater").Type);
            player.QuickSpawnItem(Mod.Find<ModItem>("SammyPants").Type);
        }
    }
}