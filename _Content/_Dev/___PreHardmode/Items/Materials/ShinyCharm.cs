using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;

namespace AAModClassic._Content._Dev.___PreHardmode.Items.Materials
{
    public class ShinyCharm : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shiny Charm");
            // Tooltip.SetDefault("A rare charm that allows you to make certain weapons shiny");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 36;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Cyan;
        }
    }
}