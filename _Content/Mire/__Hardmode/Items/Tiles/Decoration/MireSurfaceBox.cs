using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Mire.__Hardmode.Items.Tiles.Decoration
{
    public class MireSurfaceBox : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Placeables.MusicBoxes";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Music Box (Mire)");
            // Tooltip.SetDefault(@"Plays 'Secluded Swamp' by ProduceVGM");

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
			Item.createTile = ModContent.TileType<MireSurfaceBox_Tile>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(ModContent.ItemType<Bogwood>(), 20);
			recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
