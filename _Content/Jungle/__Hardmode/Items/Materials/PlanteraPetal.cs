using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;

namespace AAModClassic._Content.Jungle.__Hardmode.Items.Materials
{
    public class PlanteraPetal : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Plantera Petal");
            // Tooltip.SetDefault("It's very pink");
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Lime;
        }
    }
}