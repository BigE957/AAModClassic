using Terraria;
using Terraria.ID;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Materials
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
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Blue;
        }
    }
}
