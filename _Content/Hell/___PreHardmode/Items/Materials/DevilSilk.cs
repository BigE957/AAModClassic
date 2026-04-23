using Terraria.ID;

namespace AAModClassic._Content.Hell.___PreHardmode.Items.Materials
{
    public class DevilSilk : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Devil Silk");
            // Tooltip.SetDefault("Physical Sin; feels good, but it isn't a good long-lasting material");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
			Item.maxStack = 99;
            Item.rare = ItemRarityID.Orange;
        }
    }
}
