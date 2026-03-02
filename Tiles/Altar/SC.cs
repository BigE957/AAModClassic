using AAMod.Items;
using Terraria.ID;

namespace AAMod.Tiles.Altar
{
    public class SC : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Blue;
            Item.value = 1;
        }
    }
}
