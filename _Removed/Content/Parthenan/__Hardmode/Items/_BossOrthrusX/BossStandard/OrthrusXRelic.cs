using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossOrthrusX.BossStandard
{
    internal class OrthrusXRelic : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.Boss.Relic";
        public override string Texture => ModContent.GetInstance<OrthrusXRelic_Tile>().Texture;

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<OrthrusXRelic_Tile>(), 0);

            Item.width = 30;
            Item.height = 40;
            Item.rare = ItemRarityID.Master;
            Item.master = true;
            Item.value = Item.buyPrice(0, 5);
        }
    }
}
