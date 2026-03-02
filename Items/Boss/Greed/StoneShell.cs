using Terraria.ID;

namespace AAModClassic.Items.Boss.Greed
{
    public class StoneShell : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Stone Shell");
            // Tooltip.SetDefault(@"Harder than bedrock but lighter than pumice");
        }
        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 24;
			Item.maxStack = 99;
            Item.rare = ItemRarityID.Yellow;
        }
    }
}
