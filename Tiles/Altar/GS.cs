using AAMod.Items;

namespace AAMod.Tiles.Altar
{
    public class GS : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.maxStack = 1;
            Item.rare = 1;
            Item.value = 1;
        }
    }
}
