using AAModClassic._Content.Hallow.__Hardmode.Items.Tiles.Functional;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terrarium.World.Tiles
{
    public class TerraLeafWand : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Leaf Wand");
            ItemID.Sets.DisableAutomaticPlaceableDrop[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LivingWoodWand);
            Item.createTile = ModContent.TileType<TerraLeaves_Tile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TerraCrystal>(), 20);
            recipe.AddIngredient(ItemID.LeafWand);
            recipe.AddTile(ModContent.TileType<TruePaladinsSmeltery_Tile>());
            recipe.Register();
        }
    }
}
