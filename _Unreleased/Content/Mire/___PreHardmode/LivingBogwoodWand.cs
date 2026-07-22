using AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Mire.World.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Mire.___PreHardmode
{
    public class LivingBogwoodWand : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Tools";
        public override void SetStaticDefaults() => ItemID.Sets.DisableAutomaticPlaceableDrop[Type] = true;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LivingWoodWand);

            Item.tileWand = ModContent.ItemType<Bogwood>();
            Item.createTile = ModContent.TileType<LivingBogwood_Tile>();
        }
    }
}
