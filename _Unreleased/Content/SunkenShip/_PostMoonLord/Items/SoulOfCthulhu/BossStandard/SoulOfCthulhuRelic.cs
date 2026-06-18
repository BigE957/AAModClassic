using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.BossStandard
{
    internal class SoulOfCthulhuRelic : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.Boss.Relic";
        public override string Texture => ModContent.GetInstance<SoulOfCthulhuRelic_Tile>().Texture;

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<SoulOfCthulhuRelic_Tile>(), 0);

            Item.width = 30;
            Item.height = 40;
            Item.rare = ItemRarityID.Master;
            Item.master = true;
            Item.value = Item.buyPrice(0, 5);
        }
    }
}
