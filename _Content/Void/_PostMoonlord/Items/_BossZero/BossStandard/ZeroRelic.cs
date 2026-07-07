using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.BossStandard
{
    public class ZeroRelic : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.Boss.Relic";

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<ZeroRelic_Tile>(), 0);

            Item.width = 30;
            Item.height = 40;
            Item.rare = ItemRarityID.Master;
            Item.master = true;
            Item.value = Item.buyPrice(0, 5);
        }
    }
}
