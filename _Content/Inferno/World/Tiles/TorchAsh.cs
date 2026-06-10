using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAModClassic._Content.Inferno.World.Tiles
{
    public class TorchAsh : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.White;
            Item.value = 0;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<TorchAsh_Tile>(); //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Volcanic Ash");

            ItemTrader.ChlorophyteExtractinator.AddOption_OneWay(Type, 1, ItemID.SnowBlock, 1);
        }
    }
}
