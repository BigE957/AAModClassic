using AAModClassic;
using Terraria.ID;

namespace AAModClassic.Items.Materials
{
    public class Darkshroom : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lunar Mushroom");
            // Tooltip.SetDefault("Only grows at night");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Blue;
        }
    }
}
