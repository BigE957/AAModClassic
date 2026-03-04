using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Usable
{
    public class CodeMagnet : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Binary Code Magnet");
			/* Tooltip.SetDefault(@"Pulls items to you by moving its code closer to you
Right click the item to turn it off"); */
		}

        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 1;
			Item.value = 8000;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<CodeMagnetOff>());
        }
    }
}
