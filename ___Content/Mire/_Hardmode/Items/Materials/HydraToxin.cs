using Terraria.ID;

namespace AAModClassic.___Content.Mire._Hardmode.Items.Materials
{
    public class HydraToxin : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bogtoxin");
            // Tooltip.SetDefault("Exceedingly corrosive venom.");
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 22;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Orange;
            Item.value = 900;
        }
    }
}