using Terraria.ID;

namespace AAMod.Items.Boss.MushroomMonarch
{
    public class Mushium : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Blue;
            Item.value = Terraria.Item.sellPrice(0, 0, 3, 0);
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mushium");
        }
    }
}
