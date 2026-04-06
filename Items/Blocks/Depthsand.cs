using AAModClassic.Tiles;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAModClassic.Items.Blocks
{
    public class Depthsand : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.rare = ItemRarityID.Green;
            Item.createTile = ModContent.TileType<Depthsand_Tile>(); //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Depthsand");
        }

    }
}
