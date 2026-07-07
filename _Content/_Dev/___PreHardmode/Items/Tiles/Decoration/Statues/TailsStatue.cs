using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.___PreHardmode.Items.Tiles.Decoration.Statues
{
	public class TailsStatue : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fox Statue");
        }

        public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.maxStack = Item.CommonMaxStack;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 50000;
			Item.rare = ItemRarityID.Blue;
			Item.createTile = ModContent.TileType<DevStatue_Tile>();
			Item.placeStyle = 12;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.Register();
        }
    }
}