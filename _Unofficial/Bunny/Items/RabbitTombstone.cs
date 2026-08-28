using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Bunny.Items
{
    public class RabbitTombstone : ModItem
    {
        public override void SetDefaults()
        {
            Item.autoReuse = false;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.rare = ItemRarityID.White;
            Item.useTime = 15;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 30;
            Item.height = 44;
            Item.value = 0;
            Item.createTile = ModContent.TileType<RabbitTombstone_Tile>();
            Item.placeStyle = 0;
        }
    }
}
