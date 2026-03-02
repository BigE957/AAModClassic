using AAModClassic;
using Terraria.ID;

namespace AAModClassic.Items.Materials
{
    public class Dracokarp : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dracokarp");
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