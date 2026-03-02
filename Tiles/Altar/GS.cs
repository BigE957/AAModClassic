using AAMod.Items;
using Terraria.ID;

namespace AAMod.Tiles.Altar
{
    public class GS : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Blue;
            Item.value = 1;
        }
    }
}
