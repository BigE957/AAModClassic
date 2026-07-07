using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.__Hardmode.Items.Tiles.Functional
{
    public class ToxinMonkfish : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.Functional";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Toxin Monkfish");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 128000;
            Item.createTile = ModContent.TileType<ToxinMonkfish_Tile>();
        }
    }
}
