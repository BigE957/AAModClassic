using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAModClassic._Content.Inferno.World.Tiles
{
    public class Torchsand : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
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
            Item.rare = ItemRarityID.Green;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Torchsand_Tile>(); //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Torchsand");

            ItemTrader.ChlorophyteExtractinator.AddOption_OneWay(Type, 1, ItemID.SandBlock, 1);

            Item.ResearchUnlockCount = 100;
        }

    }
}
