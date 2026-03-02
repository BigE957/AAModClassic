using AAModClassic;
using Terraria.ID;

namespace AAModClassic.Items.Mushrooms
{
    public class Orange : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Orange Alchemical Mushroom");
            // Tooltip.SetDefault(@"It smells weird");
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Green;
        }
    }
}