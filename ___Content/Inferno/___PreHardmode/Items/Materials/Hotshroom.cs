using Terraria.ID;

namespace AAModClassic.___Content.Inferno._PreHardmode.Items.Materials
{
    public class Hotshroom : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Solar Mushroom");
            // Tooltip.SetDefault("Only grows during the day");
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
