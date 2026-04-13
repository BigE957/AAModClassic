using AAModClassic.Tiles;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAModClassic.___Content.Void.___PreHardmode.Items.Materials
{
    public class DoomiteScrap : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomite Scrap");
            /* Tooltip.SetDefault(@"It's worthless
...or is it?"); */
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Gray;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<DoomitePlate_Tile>();
        }
    }
}