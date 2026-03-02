using Terraria.ID;

namespace AAMod.Items.Materials
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
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Lime;
        }
    }
}