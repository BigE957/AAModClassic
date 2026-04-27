using Terraria;
using Terraria.ID;

namespace AAModClassic.Tiles.Altar
{
    public class SC : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Blue;
            Item.value = 1;
        }
    }
}
