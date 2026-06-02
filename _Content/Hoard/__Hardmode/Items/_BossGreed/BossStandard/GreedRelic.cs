using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.BossStandard
{
    public class GreedRelic : ModItem
    {
        public override string Texture => ModContent.GetInstance<GreedRelic_Tile>().Texture;

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<GreedRelic_Tile>(), 0);

            Item.width = 30;
            Item.height = 40;
            Item.rare = ItemRarityID.Master;
            Item.master = true;
            Item.value = Item.buyPrice(0, 5);
        }
    }
}
