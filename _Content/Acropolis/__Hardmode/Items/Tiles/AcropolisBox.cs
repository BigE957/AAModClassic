using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Content.Acropolis.__Hardmode.Items.Materials;

namespace AAModClassic._Content.Acropolis.__Hardmode.Items.Tiles
{
    public class AcropolisBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Music Box (Acropolis)");
            // Tooltip.SetDefault(@"Plays 'Palace in the Sky' by ENNWAY");
        }

        public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<AcropolisBox_Tile>();
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
            recipe.AddIngredient(ModContent.ItemType<SeraphFeather>(), 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
