using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Tiles
{
    public class EquinoxAltarBox : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.MusicBoxes";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Music Box (Equinox Altar)");
            // Tooltip.SetDefault(@"Plays 'Offerings' by ProduceVGM");

            ItemID.Sets.CanGetPrefixes[Type] = false;
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<EquinoxAltarBox_Tile>();
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.Yellow;
            Item.value = 10000;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
