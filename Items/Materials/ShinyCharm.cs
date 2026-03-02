namespace AAMod.Items.Materials
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
            Item.maxStack = 99;
            Item.rare = 9;
        }
    }
}