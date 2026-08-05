using AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Inferno.World.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Inferno.___PreHardmode.Items
{
    public class LivingRazewoodWand : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Tools";
        public override void SetStaticDefaults() => ItemID.Sets.DisableAutomaticPlaceableDrop[Type] = true;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LivingWoodWand);

            Item.tileWand = ModContent.ItemType<Razewood>();
            Item.createTile = ModContent.TileType<LivingRazewood_Tile>();
        }
    }
}
