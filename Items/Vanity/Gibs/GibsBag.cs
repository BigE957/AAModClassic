using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Gibs
{
    public class GibsBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Angry Revenant's Sarcophagus");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Raging Revenant!'");
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
            player.QuickSpawnItem(Mod.Find<ModItem>("GibsSkull").Type);
            player.QuickSpawnItem(Mod.Find<ModItem>("GibsPlate").Type);
            player.QuickSpawnItem(Mod.Find<ModItem>("GibsShorts").Type);
            if (Main.hardMode)
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("GibsJet").Type);
            }
        }
    }
}