using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Tiles.Boxes;
using AAModClassic.Items.Materials;

namespace AAModClassic.Items.Blocks.Boxes
{
	public class SagittariusBox : BaseAAItem
	{
        
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sagittarius Music Box");

            // Tooltip.SetDefault(@"Plays 'Event Horizon' by SpectralAves");
        }

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<SagittariusBox_Tile>();
            Item.width = 72;
			Item.height = 36;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DoomiteScrap>(), 5);
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
