using Terraria.Enums;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Functional
{
    public class VoidPylon : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.Functional";

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<VoidPylon_Tile>());

            Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(gold: 10));
        }
    }
}
