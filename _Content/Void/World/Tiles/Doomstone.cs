using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.World.Tiles
{
    public class Doomstone : BaseAAItem, ILocalizedModType
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
            Item.rare = ModContent.RarityType<AncientsRarity>();
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Doomstone_Tile>(); //put your CustomBlock Tile name
        }



        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Charged Doomstone");
            Item.ResearchUnlockCount = 100;

        }
    }
}
