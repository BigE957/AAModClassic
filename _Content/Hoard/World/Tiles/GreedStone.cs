using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard.World.Tiles
{
    public class GreedStone : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hoardstone");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<GreedStone_Tile>();
        }
    }
}
