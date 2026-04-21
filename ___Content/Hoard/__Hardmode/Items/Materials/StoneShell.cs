using Terraria.ID;

namespace AAModClassic.___Content.Hoard.__Hardmode.Items.Materials
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
