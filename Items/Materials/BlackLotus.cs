using Terraria;
using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class BlackLotus : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Black Lotus");
            // Tooltip.SetDefault("It's said that someone offered $160000 for this thing.");
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Yellow;
        }
    }
}
