using Terraria;
using Terraria.ID;

namespace AAModClassic._Content._Misc.__Hardmode.Items.Materials.Mushrooms
{
    public class BrownAlchemicalMushroom : BaseAAItem
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
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Green;
        }
    }
}