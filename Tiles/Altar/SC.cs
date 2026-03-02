using AAMod.Items;

namespace AAMod.Tiles.Altar
{
    public class SC : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.maxStack = 1;
            Item.rare = 1;
            Item.value = 1;
        }
    }
}
