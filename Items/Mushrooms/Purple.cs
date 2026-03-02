using Terraria.ID;

namespace AAMod.Items.Mushrooms
{
    public class Purple : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Purple Alchemical Mushroom");
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