using Terraria.ID;

namespace AAModClassic.___Content.RedMushroom.___PreHardmode.Items.Materials
{
    public class Mushium : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mushium");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.rare = ItemRarityID.Blue;
            Item.value = Terraria.Item.sellPrice(0, 0, 3, 0);
        }
    }
}
