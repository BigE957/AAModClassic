using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.World.Tiles
{
    public class Depthsand : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Depthsand");

            ItemTrader.ChlorophyteExtractinator.AddOption_OneWay(Type, 1, ItemID.SandBlock, 1);

            Item.ResearchUnlockCount = 100;
        }

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
            Item.consumable = true;
            Item.rare = ItemRarityID.Green;
            Item.createTile = ModContent.TileType<Depthsand_Tile>(); //put your CustomBlock Tile name
        }
    }
}
