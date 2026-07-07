using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.World.Tiles
{
    class ScorchedDynastyWoodUnsafe : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        //TODO: add the unsafe marker
        public override string Texture => ModContent.GetInstance<ScorchedDynastyWood>().Texture;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Unsafe Scorched Dynasty Wood");
            // Tooltip.SetDefault("");

            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<ScorchedDynastyWood>();
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<ScorchedDynastyWood>()] = Type;
        }

        public override void SetDefaults()
        {

            Item.width = 24;
            Item.height = 22;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<ScorchedDynastyWoodUnsafe_Tile>(); //put your CustomBlock Tile name
        }
    }
}
