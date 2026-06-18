using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.BossStandard
{
    internal class RaiderUltimaRelic : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.Boss.Relic";
        public override string Texture => ModContent.GetInstance<RaiderUltimaRelic_Tile>().Texture;

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<RaiderUltimaRelic_Tile>(), 0);

            Item.width = 30;
            Item.height = 40;
            Item.rare = ItemRarityID.Master;
            Item.master = true;
            Item.value = Item.buyPrice(0, 5);
        }
    }
}
