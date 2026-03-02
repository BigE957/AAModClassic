namespace AAMod.Items.Mushrooms
{
    public class Brown : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Brown Alchemical Mushroom");
            // Tooltip.SetDefault(@"It smells weird");
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 99;
            Item.rare = 2;
        }
    }
}