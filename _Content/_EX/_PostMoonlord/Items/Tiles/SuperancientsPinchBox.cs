using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Tiles
{
    public class SuperancientsPinchBox : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.MusicBoxes";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Music Box (Superancients Pinch)");
            // Tooltip.SetDefault(@"Plays 'Bygone Battles' by SpectralAves");

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
            Item.createTile = ModContent.TileType<SuperancientsPinchBox_Tile>();
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
            recipe.AddIngredient(ModContent.ItemType<EXSoul>(), 1);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
