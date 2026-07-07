using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items._BossHydra.BossStandard
{
    public class HydraRelic : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.Boss.Relic";

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<HydraRelic_Tile>(), 0);

            Item.width = 30;
            Item.height = 40;
            Item.rare = ItemRarityID.Master;
            Item.master = true;
            Item.value = Item.buyPrice(0, 5);
        }
    }
}
